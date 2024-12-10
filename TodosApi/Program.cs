using Serilog;
using Microsoft.EntityFrameworkCore;
using TodosApi.Repository;
using TodosApi.Data;
using TodosApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog (Place it before builder creation for early configuration)
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Use Serilog for the host
builder.Host.UseSerilog();

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITodosService, TodosService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();

//Add repositories 
builder.Services.AddScoped<ITodosRepository, TodosRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();


// Configure SQLite (todos.db)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

// Enable Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middlewares - Order is Important
app.UseHttpsRedirection();

// Log all incoming requests using Serilog
app.UseSerilogRequestLogging();

// Exception handling middleware should come first to catch exceptions in subsequent middlewares
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Map controllers for endpoints
app.MapControllers();

app.Run();
