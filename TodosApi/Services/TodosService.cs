using TodosApi.Models;
using TodosApi.Repository;
using TodosApi.Data;
using Serilog;

namespace TodosApi.Service
{
    public class TodosService : ITodosService
    {
       
        private readonly ITodosRepository _todosRepository;

        public TodosService(ITodosRepository todosRepository)
        {
            _todosRepository = todosRepository; //through repository to the context
        }

        public IEnumerable<TodoItem> GetTodos() => _todosRepository.GetTodos().ToList();


        public TodoItem? GetTodoById(int id) => _todosRepository.GetTodoById(id);

        public void AddTodo(TodoItem todo)
        {
            if (todo == null) return;
            
            


            if (todo.IsCompleted == true && todo.TimeCompleted == null)
            {
                todo.TimeCompleted = DateTime.Now;
            }
            _todosRepository.AddTodo(todo);
            Log.Information($"Todo {todo.Title} added");
        }

        public void UpdateTodo(int id, TodoItem newTodo)

        {

            var existingTodo = _todosRepository.GetTodoById(id);
            if (existingTodo == null)
            {
                Log.Warning($"Todo with id {id} doesn't exist.");

            }
            if (newTodo.Title != null) existingTodo.Title = newTodo.Title;
            if (newTodo.Description != null) existingTodo.Description = newTodo.Description;
            if (newTodo.IsCompleted == true && newTodo.TimeCompleted == null)
            {
                existingTodo.IsCompleted = newTodo.IsCompleted;
                existingTodo.TimeCompleted = DateTime.Now;
            }
            if (newTodo.IsCompleted == false) existingTodo.TimeCompleted = null;

            if (newTodo.TimeCompleted != null) existingTodo.TimeCompleted = newTodo.TimeCompleted;
            if (newTodo.TimeCompleted != null) existingTodo.TimeCompleted = newTodo.TimeCompleted;
            if (newTodo.CategoryId != null) existingTodo.CategoryId = newTodo.CategoryId;


                _todosRepository.UpdateTodo(existingTodo);
            Log.Information($"Todo with id {id} was updated.");


        }

        public void DeleteTodo(int id)
        {
            var todo = _todosRepository.GetTodoById(id);
            if (todo == null)
            {
                Log.Error($"Todo with id {id} doesn't exist.");

                return;
            }
            if (todo != null)
            {
                _todosRepository.DeleteTodo(id);
                Log.Information($"Success: Todo with id {todo.Id} was deleted.");

            }

        }


        


        public IEnumerable<TodoItem> GetTodosByCategoryId(int categoryId)
        {
            return _todosRepository.GetTodos().Where(todo => todo.CategoryId == categoryId).ToList();
        }

        public int GetTodoCountPerCategory(int categoryId)
        {
            return _todosRepository.GetTodos().Count(todo => todo.CategoryId == categoryId);
        }

        public IEnumerable<TodoItem> GetCompletedTodosWithCategoryInfo()
        {
            return _todosRepository.GetTodos().Where(todo => todo.IsCompleted).ToList();
        }
    }
}