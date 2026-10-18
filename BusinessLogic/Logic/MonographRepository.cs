using BusinessLogic.Data;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic;

public class MonographRepository : GenericRepository<Monograph>, IMonographRepository
{
    private readonly PS4DbContext _context;
    
    public MonographRepository(PS4DbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistCodeAsync(int code, int id)
    {
        return await _context.Monograp
            .AnyAsync(x => x.Code == code && x.Id != id);

        //var result = await _context.Monograp.Where(x => x.Code == code).Where(x => x.Id != id).FirstOrDefaultAsync();
        //if(result == null)
        //{
        //    return false;
        //}
        //return true;
    }

    public async Task<bool> ExistTitleAsync(string title, int id)
    {       
        return await _context.Monograp
            .AnyAsync(x => EF.Functions.ILike(x.Title, title) && x.Id != id);

        //var result = await _context.Monograp.Where(x => EF.Functions.ILike(x.Title, title)).Where(x => x.Id != id).FirstOrDefaultAsync();
        //if (result == null)
        //{
        //    return false;
        //}

        //return true;

    }
}
