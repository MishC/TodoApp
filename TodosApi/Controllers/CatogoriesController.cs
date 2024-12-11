using Microsoft.AspNetCore.Mvc;
using TodosApi.Models;
using TodosApi.Data;
using TodosApi.Service;

using Serilog;
using Microsoft.AspNetCore.Http.HttpResults;


namespace TodosApi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ICategoriesService _categoriesService;


        public CategoriesController(AppDbContext context, ICategoriesService categoriesService)
        {
            _context = context;
            _categoriesService = categoriesService;
        }
    
        

        //GET: api/categories/

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _categoriesService.GetCategories();
            return Ok(categories);
        }

        //GET: api/categories/{id}
        [HttpGet("{id}")]      
        public IActionResult GetCategoryById(int id)
        {
            _categoriesService.GetCategoryById(id);
            return Ok(category);
        }

        //POST:api/categories

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            
           _categoriesService.AddCategory(category);
            return Ok();
        }

       

        
        // PUT: api/categories/{id}

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, Category category)
        {
            _categoriesService.UpdateCategory(id, category);
            return Ok();
        }


        //DELETE: api/categories/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {             
            _categoriesService?.DeleteCategory(id);
            return Ok();
         }
    }


}



