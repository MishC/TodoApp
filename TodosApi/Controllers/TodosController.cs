using Microsoft.AspNetCore.Mvc;
using TodosApi.Models;
using TodosApi.Data;
using Serilog;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace TodosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {

        private readonly AppDbContext _context;

        public TodosController(AppDbContext context)
        {
            _context = context;
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

        //GET: api/todos/category/{id}
        [HttpGet("category/{id}")]
        public IActionResult GetByCategoryId(int id)
        {
            var todos = _context.Todos.Where(b => b.CategoryId == id).ToList();
            if (!todos.Any())
            {
                throw new NotFoundException($"Todo items in this category was not found.");
            }
            return Ok(todos);


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

            existingTodo.Title = newTodo.Title;
            if (newTodo.Description != null) existingTodo.Description = newTodo.Description;
            if (newTodo.IsCompleted == true) existingTodo.IsCompleted = newTodo.IsCompleted;
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
    }
}