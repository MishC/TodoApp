using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TodoApp;
using TodoApp.Classes;

using Blazored.LocalStorage;
using System.Net.Http.Headers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient with the backend API base address
builder.Services.AddScoped(sp =>
{
    var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5075/api/") }; 
    return httpClient;
});

// Add other services
builder.Services.AddScoped<EmojiService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TodosStorage>();

builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
