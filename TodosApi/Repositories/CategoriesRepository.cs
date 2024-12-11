using TodosApi.Data;
using TodoApp.Classes;
using Microsoft.EntityFrameworkCore;


namespace TodosApi.Repository
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly AppDbContext _context;

        public CategoriesRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Category> GetCategories() => _context.Categories.ToList();

        public Category? GetCategoryById(int id) => _context.Categories.FirstOrDefault(c => c.Id == id) ?? new TodoItem();

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = GetCategoryById(id);

            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        
    }
}
