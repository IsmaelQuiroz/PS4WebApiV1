using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;

namespace WebApi.Controllers
{
    public class CategoryController : BaseApiController
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryController(IGenericRepository<Category> categoryRepository)
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
            if(res == null)
            {
                return NotFound("El Producto no existe");
            }
            return res;
        }


        [HttpPost]
        public async Task<ActionResult<Category>> Post(CategoryDto categoryDto)
        {
            Category cat = new Category
            {
                Name = categoryDto.Name
            };
            var res = await _categoryRepository.Add(cat);
            if(res == 0)
            {
                throw new Exception("No se insertó la categoría");
            }
            return Ok(cat);
        }


        [HttpPut("update/{id}")]
        public async Task<ActionResult<Category>> Put(Category category, int id)
        {
            category.Id = id;
            var res =  await _categoryRepository.Update(category);
            if(res == 0)
            {
                throw new Exception("No se pudo actualizar la categoria");
            }
            return Ok(category);
        }


    }
}
