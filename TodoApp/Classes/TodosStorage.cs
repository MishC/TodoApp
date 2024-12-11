using Blazored.LocalStorage;
using System.Text.Json;
using TodoApp.Classes;
namespace TodoApp.Classes
{
    public class TodosStorage
    {
        private readonly ILocalStorageService _localStorage;

        private string? TodoStorageKey => "todos";
        public TodosStorage(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task SaveTodosAsync(List<TodoItem> todos)
        {
            var todosJson = JsonSerializer.Serialize(todos);
            await _localStorage.SetItemAsync(TodoStorageKey, todosJson);
        }

        public async Task<List<TodoItem>> LoadTodosAsync()
        {

            var todosJson = await _localStorage.GetItemAsync<string>(TodoStorageKey);
            return string.IsNullOrEmpty(todosJson) ? new List<TodoItem>() : JsonSerializer.Deserialize<List<TodoItem>>(todosJson);
        }

    }

}