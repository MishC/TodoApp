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

        public void ToggleTodoComplete(int id)
        {
            var todo = _todosRepository.GetTodoById(id);
            if (todo == null)
            {
                Log.Warning($"Todo with id {id} doesn't exist.");
                return;
            }

            

            _todosRepository.ToggleTodoComplete(id);
            Log.Information($"Todo with id {id} marked as {(todo.IsCompleted ? "completed" : "incomplete")}.");
        }




        public void UpdateTodo(int id, TodoItem newTodo)
        {
            var existingTodo = _todosRepository.GetTodoById(id);

            if (existingTodo == null)
            {
                Log.Warning($"Todo with id {id} doesn't exist.");
                return; 
            }

            if (!string.IsNullOrWhiteSpace(newTodo.Title))
                existingTodo.Title = newTodo.Title;

            if (!string.IsNullOrWhiteSpace(newTodo.Description))
                existingTodo.Description = newTodo.Description;

            if (newTodo.IsCompleted)
            {
                existingTodo.IsCompleted = true;
                existingTodo.TimeCompleted ??= DateTime.Now; 
            }
            else
            {
                existingTodo.IsCompleted = false;
                existingTodo.TimeCompleted = null; 
            }

            existingTodo.CategoryId = newTodo.CategoryId;
            existingTodo.Priority = newTodo.Priority;
            existingTodo.DueDate = newTodo.DueDate;

            _todosRepository.UpdateTodo(existingTodo);
            Log.Information($"Todo with id {id} was updated successfully.");
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

        //other methods
        //1
        public string GetCategoryName(int categoryId)
        {
            return _todosRepository.GetTodos().FirstOrDefault(todo => todo.CategoryId == categoryId)?.Category.Name;
        }
        //2
        public IEnumerable<TodoItem> GetInCompletedTodos()
        {
            return _todosRepository.GetTodos().Where(todo => !todo.IsCompleted).ToList();
        }
        //3
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

       
        public IEnumerable<TodoItem> GetTodosByCategoryName(string categoryName)
        {
            return _todosRepository.GetTodos().Where(todo => todo.Category.Name == categoryName).ToList();
        }
        public IEnumerable<TodoItem> GetTodosByPriority()
        {
            return _todosRepository.GetTodos().Where(todo => todo.Priority).ToList();
        }

        public IEnumerable<TodoItem> GetTodosByDueDate(DateTime dueDate)
        {
            return _todosRepository.GetTodos().Where(todo => todo.DueDate == dueDate).ToList();
        }
    }
}