using TodoApp.Classes;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace TodosApi.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<TodoItem> Todos { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Sport" },
                new Category { Id = 2, Name = "Family" }
            );

            
            builder.Entity<TodoItem>().HasData(
				new TodoItem { Id = 1, Title = "Run for 30 min", CategoryId = 1 },
				new TodoItem { Id = 2, Title = "Take kids from the school before 4pm", CategoryId = 2 }
			);
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.ConfigureWarnings(warnings =>
				warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
		}

	}
}