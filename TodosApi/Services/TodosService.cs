using TodoApp.Classes;

public class TodosService : ITodosService
{
	public static List<TodoItem> todos = new List<TodoItem>
		{
			new TodoItem {  Title = "Run for 30 min" },
			new TodoItem {  Title = "Take kids from school before 4 pm" }
		};

	public IEnumerable<TodoItem> GetTodos() => todos;

	public TodoItem? GetTodo(int id) => todos.FirstOrDefault(b => b.Id == id);

	public void AddTodo(TodoItem todo)
	{
		todo.Id = todos.Max(b => b.Id) + 1; 
		todos.Add(todo);
	}

	public void UpdateTodo(TodoItem existingTodo, TodoItem updatedTodo)
	{
		/*if (updatedTodo.Title == null)
		{ updatedTodo.Title = existingTodo.Title; } //This can we implemented if  title is not required*/
		
			existingTodo.Title = updatedTodo.Title;
		
		if (updatedTodo.IsCompleted==true)
		{
			existingTodo.IsCompleted = true;
		}
	}

	public void DeleteTodo(int id)
	{
		var todo = GetTodo(id);
		if (todo != null) todos.Remove(todo);
	}
}
