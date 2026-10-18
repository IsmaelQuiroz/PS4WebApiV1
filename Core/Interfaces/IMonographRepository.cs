using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces;
public interface IMonographRepository : IGenericRepository<Monograph>
{
    public Task<bool> ExistTitleAsync(string title, int id);
    public Task<bool> ExistCodeAsync(int code, int id);

}
