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
        var duplicatedName = await existNameAsync(cat.Name);

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
            return (409, "Ya existe una Categoría con el mismo nombre");
        }
         
        //throw new InvalidOperationException("The Category already exists");
        return (0,"La Categoría NO pudo ser guardada");
    }

    private async Task<int> existNameAsync(string name)
    {
        var existName = await _context.Category.Where(c => c.Name.ToUpper() == name.ToUpper()).CountAsync();
        return existName;
    }


    public async Task<(int statusCode, string message)> deleteCategory(int id)
    {
        var categoryToDelete = await _context.Category.FindAsync(id);
        int catIdGeneral = await getIdByName("GENERAL");
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

    public async Task<(int statusCode, string message)> updateCategory(Category cat)
    {
        //get categoryToUpdate to validate its name
        //var categoryToUpdate = await _context.Category.FindAsync(cat.Id);
        // var catIdGeneral = await getIdByName("GENERAL");
        if (cat.Name.ToUpper() == "GENERAL")
        {
            return (409, "La Categoría General es reservada para el sistema");
        }
        else
        {
            //validate if the Name is duplicated
            var nameIsDuplicated = await existNameAsync(cat.Name);
            if (nameIsDuplicated > 0)
            {
                return (409, "Ya existe una Categoría con el mismo nombre");
            }
        }
        _context.Category.Attach(cat);
        _context.Entry(cat).State = EntityState.Modified;
        int res = await _context.SaveChangesAsync();
        if(res > 0)
        {
            return (res, "Categoría Actualizada con éxito!");
        }
        return (404, "No se pudo realizar la actualización");

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
