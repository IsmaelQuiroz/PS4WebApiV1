using BusinessLogic.Data;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic;
public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    private readonly PS4DbContext _context;

    public CategoryRepository(PS4DbContext pS4DbContext) : base(pS4DbContext)
    {
        _context = pS4DbContext;
    }

    public async Task<(int statusCode, string message)> addCategory(Category cat)
    {
        var duplicatedName = await existNameAsync(cat);

        if (duplicatedName  == 0)
        {
            if (cat.Name.ToUpper() == "GENERAL" || cat.Name.ToUpper() == "GENERALES")
            {
                return (409, "El Nombre General es reservado para el sistema");
            }
            return (await Add(cat), "Categoría Registrada con Éxito!");
        }
        else if(duplicatedName > 0)
        {
            return (409, "La categoría ya existe en la Base de Datos");
        }
         
        //throw new InvalidOperationException("The Category already exists");
        return (0,"La Categoría NO pudo ser guardada");
    }

    private async Task<int> existNameAsync(Category cat)
    {
        var existName = await _context.Category.Where(c => c.Name.ToUpper() == cat.Name.ToUpper()).CountAsync();
        return existName;
    }


    public async Task<(int statusCode, string message)> deleteCategory(int id)
    {
        var categoryToDelete = await _context.Category.FindAsync(id);
        int catIdGeneral = await getIdByName("General".ToUpper());
        if(categoryToDelete != null)
        {
            if(categoryToDelete.Name.ToUpper() == "GENERAL" || categoryToDelete.Name.ToUpper() == "GENERALES")
            //if (categoryToDelete.Name.ToUpper() == "GENERAL")
            {
                return (409, "La Categoría General es reservada para el sistema");
            }
            else
            {
                 _context.Monograp.Where(m => m.CategoryId == categoryToDelete.Id)
                    .ExecuteUpdate(m => m.SetProperty(m => m.CategoryId, catIdGeneral));
                 _context.Remove(categoryToDelete);

            }
            //await _context.SaveChangesAsync();
            return (await _context.SaveChangesAsync() > 0 ? (1, "La Categoria fue Eliminada") : (0, "La acción fue Rechazada"));
        }
        else
        {
            return (404, "No se encontró la Categoría buscada");
        }
           
    }

    public async Task<int> getIdByName(string name)
    {
        var category = await _context.Category.Where(c => c.Name.ToUpper() == name).FirstOrDefaultAsync();
        if(category == null)
        {
            return 0;
        }

        return category.Id;
    }
}
