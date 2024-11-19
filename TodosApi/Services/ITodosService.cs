using TodoApp.Classes;

public interface ITodosService
{
    IEnumerable<TodoItem> GetTodos();           
    TodoItem? GetTodo(int id);              
    void AddTodo(TodoItem todo);               
    void UpdateTodo(TodoItem oldTodo, TodoItem newTodo);     
    void DeleteTodo(int id);                
}