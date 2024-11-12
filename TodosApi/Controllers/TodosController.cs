using Microsoft.AspNetCore.Mvc;
using TodoApp.Classes;

namespace TodosApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TodosController : ControllerBase
	{
		public static List<TodoItem> todos = new List<TodoItem>
		{
			new TodoItem {  Title = "Run for 30 min" },
			new TodoItem {  Title = "Take kids from school before 4 pm" }
		};

		// GET: api/todos
		[HttpGet]
		public ActionResult<IEnumerable<TodoItem>> GetTodos()
		{
			return Ok(todos);
		}

		// GET: api/todos/{id}
		[HttpGet("{id}")]
		public ActionResult<TodoItem> GetTodo(int id)
		{
			var todo = todos.FirstOrDefault(b => b.Id == id);
			if (todo == null)
			{
				NotFound();
			}
			return Ok(todo);
		}

		[HttpPost]
		public ActionResult<TodoItem> CreateTodo(TodoItem newTodo)
		{
			newTodo.Id = todos.Max(b => b.Id) + 1;
			todos.Add(newTodo);
			return CreatedAtAction(nameof(GetTodo), new { id = newTodo.Id }, newTodo);
		}

		// PUT: api/todos/{id}

		[HttpPut("{id}")]
		public ActionResult UpdateTodo(int id, TodoItem updatedTodo)
		{
			var existingTodo = todos.FirstOrDefault(b => b.Id == id);
			if (existingTodo == null)
			{
				return NotFound();
			}
			existingTodo.Title = updatedTodo.Title;

			if (updatedTodo.isCompleted)
			{
				existingTodo.isCompleted = updatedTodo.isCompleted;
  		    }


            return NoContent();
		}

		[HttpDelete("{id}")]
		public ActionResult DeleteTodo(int id)
		{
			var todo = todos.FirstOrDefault(b => b.Id == id);
			if (todo == null)
			{
				return NotFound();
			}

			todos.Remove(todo);
			return NoContent();
		}
	}
}