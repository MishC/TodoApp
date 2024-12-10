using TodoApp.Classes;

namespace TodosApi.Repository
{
    public interface ITodosRepository
    {
        IQueryable<TodoItem> GetTodos();
        TodoItem GetTodoById(int id);
        void AddTodo(TodoItem todo);
        void UpdateTodo(TodoItem todo);
        void DeleteTodo(int id);
    }
}