using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseClass
    {
        Task<T> GetByIdAsync(int Id);

        // Permite pasar una expresión LINQ como filtro: x => x.Campo == valor
        //Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<IReadOnlyList<T>> GetByFilterAsync(Expression<Func<T, bool>> filter);      

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<int> Add(T entity);

        Task<int> Update(T entity);

        Task<bool> Delete(T entity);
    }
}
