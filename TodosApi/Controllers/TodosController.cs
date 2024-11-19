using Microsoft.AspNetCore.Mvc;
using TodoApp.Classes;

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

            return Ok(todos);
		}

		// GET: api/todos/{id}
		[HttpGet("{id}")]
		public ActionResult<TodoItem> GetTodo(int id)
		{
			var todo = _todosService.GetTodo(id);
			if (todo == null)
			{
				NotFound();
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
                return NotFound();
            }

            _todosService.UpdateTodo(existingTodo, updatedTodo);

            return NoContent();
        }

        //DELETE: api/todos/{id}
        [HttpDelete("{id}")]
		public ActionResult DeleteTodo(int id)
		{

			_todosService.DeleteTodo(id);
			return NoContent();
		}
	}
}