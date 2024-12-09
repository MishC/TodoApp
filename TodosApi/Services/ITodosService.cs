using TodoApp.Classes;

public interface ITodosService
{
    IEnumerable<TodoItem> GetTodos();           
    TodoItem? GetTodoById(int id);              
    void AddTodo(TodoItem todo);               
    void UpdateTodo(int id, TodoItem newTodo);     
    void DeleteTodo(int id);                
}