public class TodoService
{
    private List<TodoItem> todos = new();

    public List<TodoItem> GetTodos() => todos;

    public void AddTodo(TodoItem todo) => todos.Add(todo);

    public void RemoveTodo(TodoItem todo) => todos.Remove(todo);
}
