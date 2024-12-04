using Microsoft.AspNetCore.Mvc;
using TodoApp.Classes;
using Serilog;
using Microsoft.AspNetCore.Http.HttpResults;


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

        //POST: api/todos/

        [HttpPost]
        public IActionResult Create(TodoItem todo)
        {
            
            _context.Todos.Add(todo);
            _context.SaveChanges();
            return Ok();
        }


        // PUT: api/todos/{id}

        [HttpPut("{id}")]
        public IActionResult Update(int id, TodoItem newTodo)
        {
            var existingTodo = _context.Todos.Find(id);
            if (existingTodo == null)
            {
                Log.Error($"No such Todo.");

                throw new NotFoundException($"Todo item with id {id} was not found.");
            }

            existingTodo.Title = newTodo.Title;
            if (newTodo.Description!=null) existingTodo.Description = newTodo.Description;
            if (newTodo.IsCompleted==true) existingTodo.IsCompleted = newTodo.IsCompleted;
            if (newTodo.TimeCompleted!=null) existingTodo.TimeCompleted = newTodo.TimeCompleted;

            _context.SaveChanges();

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
            Log.Information("Todo with id {TodoId} deleted successfully.", id);
            _context.Todos.Remove(todo);
            _context.SaveChanges();

            return Ok();
        }

    }
}