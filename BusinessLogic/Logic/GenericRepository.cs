using BusinessLogic.Data;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BusinessLogic.Logic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseClass
    {
        private readonly PS4DbContext _context;
        //protected readonly DbSet<T> _dbSet;
        public GenericRepository(PS4DbContext pS4DbContext)
        {
            //_dbSet = pS4DbContext.Set<T>();
            _context = pS4DbContext;
        }

        public async Task<int> Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(T entity)
        {
            if (_context.Set<T>().Entry(entity).State == EntityState.Detached)
            {
                _context.Set<T>().Attach(entity);
            }
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync() >0;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int Id)
        {
           return await _context.Set<T>().FindAsync(Id);
        }

        public async Task<int> Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        //prueba piloto
        public async Task<IReadOnlyList<T>> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            //IQueryable<T> query = _dbSet;
            // Aplicar el filtro si se proporciona
            //if (filter != null)
            //{
            //    query = query.Where(filter);
            //}
            //return await  query.ToListAsync();

            return await _context.Set<T>().Where(filter).ToListAsync();

        }


        //Methods with specification pattern
        //Applicator of Specification evaluator 
        public IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.MakeQuery(_context.Set<T>().AsQueryable(), spec);
        }


        public async Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }
    }
}
