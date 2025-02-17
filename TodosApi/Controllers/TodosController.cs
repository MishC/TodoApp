using Microsoft.AspNetCore.Mvc;
using TodosApi.Models;
using TodosApi.Data;
using TodosApi.Service;

using Serilog;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Principal;


namespace TodosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]

    public class TodosController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly ITodosService _todosService;
        private readonly ICategoriesService _categoriesService;



        public TodosController(AppDbContext context, ITodosService todosService, ICategoriesService categoriesService)
        {
            _context = context;
            _todosService = todosService;
            _categoriesService = categoriesService;
        }


        // GET: api/todos
        [HttpGet]
        public IActionResult GetTodos()
        {
            Log.Information("Fetching all todos");         
            
           var todos= _todosService.GetTodos();
            return Ok(todos);

        }


        // GET: api/todos/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var todo = _todosService.GetTodoById(id);
            
            return Ok(todo);
        }

        
        //POST: api/todos/

        [HttpPost]
        public IActionResult Create(TodoItem todo)
        {
            if (_categoriesService.CategoryExists(todo.CategoryId))
            {
                _todosService.AddTodo(todo);
                Log.Information($"New todo: {todo.Title} has been added.");

                return NoContent();
            }
            else { return BadRequest(new { message = $"Category with ID {todo.CategoryId} does not exist." }); };
        }


        // PUT: api/todos/{id}
        [HttpPut("{id}")]

        public IActionResult Update(int id, TodoItem newTodo)
        {
            _todosService.UpdateTodo(id, newTodo);
            return NoContent();
        }

        // PUT: api/todos/toggle/{id}
        [HttpPut("toggle/{id}")]
        public IActionResult ToggleTodoComplete(int id)
        {
            try
            {
                _todosService.ToggleTodoComplete(id);
                return Ok(new { message = "Todo completion status toggled successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error toggling todo completion status: {ex.Message}" });
            }
        }

        //DELETE: api/todos/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {  

            _todosService.DeleteTodo(id);
           return NoContent();
        }

        //Other methods

        //GET: api/todos/category/{categoryId}/name 
        [HttpGet("category/{categoryId}/name")]
        public IActionResult GetCategoryName(int categoryId)
        {
            var categoryName = _todosService.GetCategoryName(categoryId);
            if (categoryName == null)
                return NotFound("Category not found.");
            return Ok(categoryName);
        }

        // GET: api/todos/category/{categoryId}
        [HttpGet("category/{categoryId}")]
        public IActionResult GetTodosByCategoryId(int categoryId)
        {
            var todos = _todosService.GetTodosByCategoryId(categoryId);
            if (!todos.Any())
                return NotFound("No todos found for this category.");
            return Ok(todos);
        }

        // GET: api/todos/category/{categoryId}/count
        [HttpGet("category/{categoryId}/count")]
        public IActionResult GetTodoCountPerCategory(int categoryId)
        {
            var count = _todosService.GetTodoCountPerCategory(categoryId);
            return Ok(count);
        }

        // GET: api/todos/completed
        [HttpGet("completed")]
        public IActionResult GetCompletedTodosWithCategoryInfo()
        {
            var completedTodos = _todosService.GetCompletedTodosWithCategoryInfo();
            if (!completedTodos.Any())
                return NotFound("No completed todos found.");
            return Ok(completedTodos);
        }

        // GET: api/todos/incompleted
        [HttpGet("incompleted")]
        public IActionResult GetInCompletedTodosWithCategoryInfo()
        {
            var incompletedTodos = _todosService.GetInCompletedTodos();
            if (!incompletedTodos.Any())
                return NotFound("No incompleted todos found.");
            return Ok(incompletedTodos);
        }
    }

    
}