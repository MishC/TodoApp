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
            _todosRepository = todosRepository; 
        }

        public IQueryable<TodoItem> GetTodos()
        {
            Log.Information("Fetching all todos.");

            var todos = _todosRepository.GetTodos();

            if (!todos.Any())
            {
                Log.Warning("No todos available to fetch.");
            }

            return todos;
        }


        public TodoItem GetTodoById(int id)
        {
            Log.Information($"Fetching todo with id {id}.");

            var todo = _todosRepository.GetTodoById(id);
            if (todo == null)
            {
                Log.Warning($"Todo with id {id} does not exist.");
                throw new NotFoundException($"Todo item with id {id} was not found.");
            }

            return todo;
        }

      
        public void AddTodo(TodoItem todo)
        {
            if (todo == null) return;

            todo.CurrentDate = DateTime.Now;
            if (todo.IsCompleted == true && todo.TimeCompleted == null)
            {
                todo.TimeCompleted = DateTime.Now;
            }
            _todosRepository.AddTodo(todo);
            Log.Information($"Todo {todo.Title} added");
        }

        public void ToggleComplete(int id)
        {
            var todo = _todosRepository.GetTodoById(id);
            if (todo == null)
            {
                Log.Warning($"Todo with id {id} doesn't exist.");
                return;
            }

            todo.IsCompleted = !todo.IsCompleted;

            _todosRepository.UpdateTodo(todo);
            Log.Information($"Todo with id {id} marked as {(todo.IsCompleted ? "completed" : "incomplete")}.");
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
            if (newTodo.CategoryId!=0) existingTodo.CategoryId = newTodo.CategoryId;
            if (newTodo.Priority != null) existingTodo.Priority = newTodo.Priority;
            if (newTodo.DueDate != null) existingTodo.DueDate = newTodo.DueDate;


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

        public string GetCategoryName(int categoryId)
        {
            return _todosRepository.GetTodos().FirstOrDefault(todo => todo.CategoryId == categoryId)?.Category.Name;
        }

        public IEnumerable<TodoItem> GetTodosByPriority(string priority)
        {
            return _todosRepository.GetTodos().Where(todo => todo.Priority == priority).ToList();
        }

        public IEnumerable<TodoItem> GetTodosByDueDate(DateTime dueDate)
        {
            return _todosRepository.GetTodos().Where(todo => todo.DueDate == dueDate).ToList();
        }
    }
}