using TodosApi.Models;

namespace TodosApi.Repository
{
    public interface ITodosRepository
    {
        IQueryable<TodoItem> GetTodos();
        TodoItem GetTodoById(int id);
        void AddTodo(TodoItem todo);
        void UpdateTodo(TodoItem todo);
        void DeleteTodo(int id);

        IEnumerable<TodoItem> GetTodosByCategoryId(int categoryId);
        int GetTodoCountPerCategory(int categoryId);
        IEnumerable<TodoItem> GetCompletedTodosWithCategoryInfo();
    }
}