using Blazored.LocalStorage;
namespace TodoApp.Classes
{
    public class AuthService
    {
        private readonly ILocalStorageService _localStorage;
        private const string TokenKey = "authToken";
        private const string UsernameKey = "username";
        public event Action? OnAuthStateChanged;

        public string CurrentUsername { get; private set; } = string.Empty;

        public AuthService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<bool> Login(string username, string password)
        {
            if (username == "admin" && password == "password")
            {
                await _localStorage.SetItemAsync(TokenKey, "fake-jwt-token");
                await _localStorage.SetItemAsync(UsernameKey, username); // Save username
                CurrentUsername = username;
                NotifyAuthStateChanged();
                return true;
            }

            return false;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(TokenKey);
            await _localStorage.RemoveItemAsync(UsernameKey);
            CurrentUsername = string.Empty;
            NotifyAuthStateChanged();
        }

        public async Task InitializeUsernameAsync()
        {
            CurrentUsername = await _localStorage.GetItemAsync<string>(UsernameKey) ?? string.Empty;
            NotifyAuthStateChanged();

        }


        public async Task<bool> IsAuthenticated()
        {
            var token = await _localStorage.GetItemAsync<string>(TokenKey);
            return !string.IsNullOrEmpty(token);
        }

        private void NotifyAuthStateChanged()
        {
            OnAuthStateChanged?.Invoke();
        }
    }
}