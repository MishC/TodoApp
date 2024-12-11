using Microsoft.AspNetCore.Mvc;
using TodosApi.Models;
using TodosApi.Data;
using TodosApi.Service;

using Serilog;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq.Expressions;


namespace TodosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
                if (_context.Todos.ToList().Count == 0)
            {
                Log.Error($"There is any todo to be fetched.");
                throw new NotFoundException($"Todos are empty"); 

            }

            return Ok(_context.Todos.ToList());

        }


        // GET: api/todos/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var todo = _context.Todos.Find(id);
            if (todo == null)
            {
                Log.Error($"Todo with id {id} doesn't exist.");
                throw new NotFoundException($"Todo item with id {id} was not found.");

            }
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

                return Ok();
            }
            else { return BadRequest(new { message = $"Category with ID {todo.CategoryId} does not exist." }); };
        }


        // PUT: api/todos/{id}
        public IActionResult Update(int id, TodoItem newTodo)
        {
            _todosService.UpdateTodo(id, newTodo);
            return NoContent();
        }


        //DELETE: api/todos/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {  

            _todosService.DeleteTodo(id);
            Log.Information($"Todo with id: {id} has been removed.");
            return Ok();
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
    }
}