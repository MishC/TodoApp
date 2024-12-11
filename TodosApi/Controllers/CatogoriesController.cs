using Microsoft.AspNetCore.Mvc;
using TodosApi.Models;
using TodosApi.Data;
using Serilog;
using Microsoft.AspNetCore.Http.HttpResults;


namespace TodosApi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }


        //GET: api/categories/

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _context.Categories.ToList();
            return Ok(categories);
        }

        //GET: api/categories/{id}
        [HttpGet("{id}")]
        
        public IActionResult GetCategoryById(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                Log.Error($"Category with id \"{id}\" doesn't exist.");
                throw new NotFoundException($"Category with id {id} was not found.");

            }
            return Ok(category);
        }

        //POST:api/categories

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            
                _context.Categories.Add(category);
                _context.SaveChanges();

            Log.Information($"New category {category.Name} has been added.");

            return Ok();
        }

        //GET: api/categories/important
        [HttpGet("important")]
        public IActionResult GetImportantCategories()
        {
            var importantCategories = _context.Categories.ToList().Where(c => c.Priority);
            return Ok(importantCategories);
        }


        // PUT: api/categories/{id}

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, Category category)
        {
            var oldCategory = _context.Categories.Find(id);
            if (oldCategory == null)
            {
                Log.Error($"No such category with id: {id}. Cannot be updated.");
                throw new NotFoundException($"Category with id {id} was not found.");
            }

            oldCategory.Name = category.Name ?? oldCategory.Name;
            if (!string.IsNullOrEmpty(category.CategoryDescription))
            {
                oldCategory.CategoryDescription = category.CategoryDescription;
            }

            _context.SaveChanges();
            Log.Information($"Category with id: {id} has been updated.");
            return NoContent();
        }


        //DELETE: api/categories/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                Log.Error($"No such category.");
                throw new NotFoundException($"Category with id \"{id}\" was not found.");
            }
            _context.Categories.Remove(category);
            Log.Information($"Category with id: {id} has been removed.");

            _context.SaveChanges();

            return Ok();
        }
    }


}



