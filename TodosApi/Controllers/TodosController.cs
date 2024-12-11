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


        public TodosController(AppDbContext context, ITodosService todosService)
        {
            _context = context;
            _todosService = todosService;
        }


        // GET: api/todos
        [HttpGet]
        public IActionResult GetTodos()
        {
            Log.Information("Fetching all todos");
            return Ok(_context.Todos.ToList());
            if (_context.Todos.ToList().Count == 0)
            {
                Log.Error($"There is any todo to be fetched.");
                throw new NotFoundException($"Todos are empty"); 



            }
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
            
            _context.Todos.Add(todo);
            _context.SaveChanges();
            Log.Information($"New todo: {todo.Title} has been added.");

            return Ok();
        }


        // PUT: api/todos/{id}

        [HttpPut("{id}")]
        public IActionResult Update(int id, TodoItem newTodo)
        {
            var existingTodo = _context.Todos.Find(id);
            if (existingTodo == null)
            {
                Log.Error($"No such todo with id: {id}. Cannot be updated.");

                throw new NotFoundException($"Todo item with id {id} was not found.");
            }

            if (newTodo.Title != null) existingTodo.Title = newTodo.Title;
            if (newTodo.Description != null) existingTodo.Description = newTodo.Description;
            if (newTodo.IsCompleted == true)
            { existingTodo.IsCompleted = newTodo.IsCompleted; 
              existingTodo.TimeCompleted = DateTime.Now;
            }
            if (newTodo.TimeCompleted != null) existingTodo.TimeCompleted = newTodo.TimeCompleted;
            if (newTodo.TimeCompleted != null) existingTodo.TimeCompleted = newTodo.TimeCompleted;


            _context.SaveChanges();
            Log.Information($"Todo with id: {id} has been updated.");
            return NoContent();
        }


        //DELETE: api/todos/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            var todo = _context.Todos.Find(id);
            if (todo == null)
            {
                Log.Error($"No such Todo.");
                throw new NotFoundException($"Todo item with id {id} was not found.");
            }
            _context.Todos.Remove(todo);
            Log.Information($"Todo with id: {id} has been removed.");

            _context.SaveChanges();

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