using TodoApp.Classes;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<TodoItem> Todos { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.Entity<TodoItem>().HasData(
			new TodoItem { Id = 1, Title = "Run for 30 min" },
			new TodoItem { Id = 2, Title = "Take kids from the school before 4pm"  }
		);
	}

}