using TodosApi.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace TodosApi.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<TodoItem> Todos { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<TodoItem>()
            .HasOne(t => t.Category)    // A Todo has ONE Category
            .WithMany(c => c.Todos)     // A Category has MANY Todos
            .HasForeignKey(t => t.CategoryId) // Foreign Key in TodoItem
            .OnDelete(DeleteBehavior.Cascade); // If a category is deleted, its todos are deleted too

            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Appointments", CategoryDescription = "Appointment at offices, government institutions, doctor's"},
                new Category { Id = 2, Name = "Family&Friends", CategoryDescription = "Todos related to family and friends"},
                new Category { Id = 3, Name = "Sport", CategoryDescription = "Sport activities" },
                new Category { Id = 4, Name = "Job", CategoryDescription = "Work-related tasks" },
                new Category { Id = 5, Name = "Shopping", CategoryDescription = "Buy things" },
                new Category { Id = 6, Name = "Beauty & Wellness", CategoryDescription = "Beauty procedures" },
                new Category { Id = 7, Name = "Home", CategoryDescription = "What to do at home" }
            );


            builder.Entity<TodoItem>().HasData(
                new TodoItem { Id = 1, Title = "Run for 30 min", CategoryId = 3, Priority = false },
                new TodoItem { Id = 2, Title = "Take kids from the school before 4.20pm", CategoryId = 2, Priority = true }
            );
		}

        //Inore warnings about pending model changes
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.ConfigureWarnings(warnings =>
				warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
		}

	}
}