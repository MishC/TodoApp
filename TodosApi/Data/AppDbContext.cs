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
                new Category { Id = 1, CategoryName = "Personal", CategoryDescription = "Practical things which need to be done as first", Priority = true },
                new Category { Id = 2, CategoryName = "Family", CategoryDescription = "Spend time with loved ones", Priority = false },
                new Category { Id = 3, CategoryName = "Sport", CategoryDescription = "Stay fit and healthy", Priority = false },
                new Category { Id = 4, CategoryName = "Job", CategoryDescription = "Work-related tasks", Priority = true },
                new Category { Id = 5, CategoryName = "Shopping", CategoryDescription = "Groceries and essentials", Priority = false },
                new Category { Id = 6, CategoryName = "Beauty & Wellness", CategoryDescription = "Take care of yourself", Priority = false },
                new Category { Id = 7, CategoryName = "Home", CategoryDescription = "What to do at home", Priority = false }
            );

            
            builder.Entity<TodoItem>().HasData(
				new TodoItem { Id = 1, Title = "Run for 30 min", CategoryId = 3 },
				new TodoItem { Id = 2, Title = "Take kids from the school before 4.20pm", CategoryId = 1 }
			);
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.ConfigureWarnings(warnings =>
				warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
		}

	}
}