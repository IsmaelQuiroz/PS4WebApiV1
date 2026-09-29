using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces;

public interface ICategoryRepository : IGenericRepository<Category>
{
    //Task<bool> existNameAsync(Category cat);

    Task<(int statusCode, string message)> addCategory(Category cat);

    Task<(int statusCode, string message)> deleteCategory(int id);

    Task<int> getIdByName(string name); 
}
