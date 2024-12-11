using TodosApi.Models;
namespace TodosApi.Service
{

    public interface ITodosService
    {
        IQueryable<TodoItem> GetTodos();
        TodoItem? GetTodoById(int id);
        void AddTodo(TodoItem todo);
        void UpdateTodo(int id, TodoItem newTodo);
        void DeleteTodo(int id);

        IEnumerable<TodoItem> GetTodosByCategoryId(int categoryId);
        int GetTodoCountPerCategory(int categoryId);
        IEnumerable<TodoItem> GetCompletedTodosWithCategoryInfo();
    }

}