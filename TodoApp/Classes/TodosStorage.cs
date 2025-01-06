using System.Net.Http;
using System.Net.Http.Json;
using TodoApp.Classes;

namespace TodoApp.Classes
{
    public class TodosStorage
    {
        private readonly HttpClient _httpClient;

        public TodosStorage(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TodoItem>> GetTodosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TodoItem>>("todos") ?? new List<TodoItem>();
        }

        public async Task AddTodoAsync(TodoItem todo)
        {
            await _httpClient.PostAsJsonAsync("todos", todo);
        }

        public async Task UpdateTodoAsync(TodoItem todo)
        {
            await _httpClient.PutAsJsonAsync($"todos/{todo.Id}", todo);
        }

        public async Task DeleteTodoAsync(int id)
        {
            await _httpClient.DeleteAsync($"todos/{id}");
        }
    }
}

