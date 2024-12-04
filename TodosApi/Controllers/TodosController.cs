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

        private readonly ITodosService _todosService;

        public TodosController(ITodosService todosService)
        {
            _todosService = todosService;
        }


        // GET: api/todos
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTodos()
        {
            var todos = _todosService.GetTodos();
            Log.Information("Fetching all todos");
            return Ok(todos);
            if (todos == null)
            {
                Log.Error($"There is any todo to be fetched.");
                throw new NotFoundException($"No todos");
            }
        }

        // GET: api/todos/{id}
        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTodo(int id)
        {
            var todo = _todosService.GetTodo(id);
            Log.Information($"Fetching todo with id: {id}");
            if (todo == null)
            {
                Log.Error($"Todo with id {id} doesn't exist.");
                 throw new NotFoundException($"Todo item with id {id} was not found.");

            }
            return Ok(todo);
        }

        //POST: api/todos/

        [HttpPost]
        public ActionResult<TodoItem> CreateTodo(TodoItem newTodo)
        {   
            _todosService.AddTodo(newTodo);
            return CreatedAtAction(nameof(GetTodo), new { id = newTodo.Id }, newTodo);
        }

        // PUT: api/todos/{id}

        [HttpPut("{id}")]
        public ActionResult UpdateTodo(int id, TodoItem updatedTodo)
        {
            var existingTodo = _todosService.GetTodo(id);
            if (existingTodo == null)
            {
                Log.Error($"No such Todo.");

                throw new NotFoundException($"Todo item with id {id} was not found.");
            }

            _todosService.UpdateTodo(existingTodo, updatedTodo);

            return NoContent();
        }

        //DELETE: api/todos/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteTodo(int id)
        {
           

                var todo = _todosService.GetTodo(id);
                if (todo == null)
                {
                    Log.Error($"No such Todo.");
                    throw new NotFoundException($"Todo item with id {id} was not found.");
                }
                _todosService.DeleteTodo(id);
                return NoContent();

           
        }
    }
}