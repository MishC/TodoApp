using TodosApi.Data;
using TodoApp.Shared.Models;
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


        public IQueryable<TodoItem> GetTodos() => _context.Todos.ToList();


        public TodoItem GetTodoById(int id) => _context.Todos
                   .FirstOrDefault(b => b.Id == id) ?? new TodoItem();

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
    }
}