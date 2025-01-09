using System.Net.Http;
using System.Net.Http.Json;
using TodoApp.Classes;
using System.Collections.Generic;
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
        public async Task<TodoItem> GetTodoByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<TodoItem>($"todos/{id}");
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

        // Categories methods
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Category>>("categories") ?? new List<Category>();
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _httpClient.PostAsJsonAsync("categories", category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _httpClient.PutAsJsonAsync($"categories/{category.Id}", category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _httpClient.DeleteAsync($"categories/{id}");
        }

        //others

        // GET: api/categories/{id}/name
        public async Task<string> GetCategoryName(int id)
        {
            return await _httpClient.GetFromJsonAsync<string>($"categories/{id}/name");
        }

        
        // GET: api/todos/category/{categoryId}
        public async Task<List<TodoItem>> GetTodosByCategoryAsync(int categoryId)
        {
            return await _httpClient.GetFromJsonAsync<List<TodoItem>>($"todos/category/{categoryId}");
        }

        // GET: api/todos/category/{categoryId}/count
        public async Task<int> GetTodoCountByCategoryAsync(int categoryId)
        {
            return await _httpClient.GetFromJsonAsync<int>($"todos/category/{categoryId}/count");
        }

        // GET: api/todos/completed
        public async Task<List<TodoItem>> GetCompletedTodosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TodoItem>>("todos/completed");
        }
    }
}

