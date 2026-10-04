using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Errors;

namespace WebApi.Controllers;

public class CategoryController : BaseApiController
{
    //private readonly IGenericRepository<Category> _categoryRepository;
    private readonly ICategoryRepository _categoryRepository;

    //public CategoryController(IGenericRepository<Category> categoryRepository)
    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Category>>> GetCategoriesAsync()
    {
        //var data = await _categoryRepository.GetAllAsync();
        //return Ok(data);
        return Ok(await _categoryRepository.GetAllAsync());

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategoryById(int id)
    {
        var res = await _categoryRepository.GetByIdAsync(id);
        if (res == null)
        {
            return NotFound("El Producto no existe");
        }
        return res;
    }


    [HttpPut("update/{id}")]
    public async Task<ActionResult<Category>> Put(Category category, int id)
    {
        category.Id = id;
        var res = await _categoryRepository.updateCategory(category);
        if(res.statusCode == 1)
        {
            return Ok(category);
            
        }
        return BadRequest(new CodeErrorResponse(res.statusCode, res.message));
    }

    [HttpPost]
    public async Task<ActionResult<Category>> Post(CategoryDto categoryDto)
    {
        Category cat = new Category
        {
            Name = categoryDto.Name
        };
        //var res = await _categoryRepository.Add(cat);
        var res = await _categoryRepository.addCategory(cat);

        if (res.statusCode != 1)
        {
            return BadRequest(new CodeErrorResponse(res.statusCode, res.message));
        }       
        return Ok(cat);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> delete(int id)
    {
        //Category cat = await _categoryRepository.GetByIdAsync(id);
        //if (cat == null)
        //{
        //    return NotFound(new CodeErrorResponse(404,null));
        //}
        var res = await _categoryRepository.deleteCategory(id);
        if(res.statusCode == 1)
        {
            return Ok(res.message);
        }
        return BadRequest(new CodeErrorResponse(res.statusCode, res.message));


        //return await _categoryRepository.validateName(cat);


    }
}

