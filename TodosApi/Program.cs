using Serilog;
using Microsoft.EntityFrameworkCore;
using TodosApi.Repository;
using TodosApi.Data;
using TodosApi.Service;
using TodosApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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

//Configure Authentication

var key = Encoding.ASCII.GetBytes(AuthConfig.SigningKey);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();

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

app.UseHttpsRedirection();

// Log all incoming requests using Serilog
app.UseSerilogRequestLogging();

// Middlewares: Exception Handling
app.UseMiddleware<ExceptionHandlingMiddleware>();
//Authentication
app.UseAuthentication();
app.UseAuthorization();
// Map controllers for endpoints
app.MapControllers();

app.Run();
