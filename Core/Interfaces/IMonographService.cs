using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces;
public interface IMonographService
{
    public Task<(int statusCode, string message)> CreateMonographAsync(Monograph monograph);

    public Task<(int statusCode, string message)> UpdateMonographAsync(Monograph monograph);


}
