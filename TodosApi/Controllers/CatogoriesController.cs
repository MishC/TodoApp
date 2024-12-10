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

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _context.Categories.ToList();
            return Ok(categories);
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCategories), new { id = category.Id }, category);
        }
    }


}



