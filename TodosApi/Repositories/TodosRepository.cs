using TodosApi.Data;
using TodosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TodosApi.Repository
{
    public class TodosRepository : ITodosRepository
    {
        private readonly AppDbContext _context;

        public TodosRepository(AppDbContext context)
        {
            _context = context;
        }


        public IQueryable<TodoItem> GetTodos() => _context.Todos;

        public TodoItem? GetTodoById(int id) => _context.Todos
            .FirstOrDefault(b => b.Id == id);

        public void AddTodo(TodoItem todo)
        {
            _context.Todos.Add(todo);
            _context.SaveChanges();
        }

        public void UpdateTodo(TodoItem todo)
        {
            _context.Todos.Update(todo);
            _context.SaveChanges();
        }

        public void DeleteTodo(int id)
        {
            var todo = GetTodoById(id);
            if (todo != null)
            {
                _context.Todos.Remove(todo);
                _context.SaveChanges();
            }
        }

        // Get all todos by category ID
        public IEnumerable<TodoItem> GetTodosByCategoryId(int categoryId)
        {
            return _context.Todos.Where(todo => todo.CategoryId == categoryId).ToList();
        }

        // Get todo count per category
        public int GetTodoCountPerCategory(int categoryId)
        {
            
                return _context.Todos.Count(todo => todo.CategoryId == categoryId);
           
        }

        // Get all completed todos with category info
        public IEnumerable<TodoItem> GetCompletedTodosWithCategoryInfo()
        {
            return _context.Todos
                .Where(todo => todo.IsCompleted)
                .Include(todo => todo.CategoryId) 
                .ToList();
        }
    }
}