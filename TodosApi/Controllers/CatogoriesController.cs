using Microsoft.AspNetCore.Mvc;
using TodoApp.Classes;
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
        
        public IActionResult GetById(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                Log.Error($"Category with id "{id}" doesn't exist.");
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
            return CreatedAtAction(nameof(GetCategories), new { id = category.Id }, category);
        }

        //DELETE: api/categories/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                Log.Error($"No such category.");
                throw new NotFoundException($"Category with id "{id}" was not found.");
            }
            Log.Information($"Category with id: {id} deleted successfully.");
            _context.Categories.Remove(category);
            Log.Information($"Category with id: {id} has been removed.")

            _context.SaveChanges();

            return Ok();
        }
    }


}


