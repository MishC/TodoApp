using TodoApp.Classes

public interface ITodosService
{
    IEnumerable<TodoItem> GetTodos();           
    Book? GetTodo(int id);              
    void AddTodo(TodoItem todo);               
    void UpdateTodo(int id, TodoItem todo);     
    void DeleteTodo(int id);                
}