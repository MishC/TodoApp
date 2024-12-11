using TodosApi.Data;
using TodosApi.Models;
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

        public IQueryable<Category> GetCategories() => _context.Categories;

        public Category? GetCategoryById(int id) => _context.Categories.FirstOrDefault(c => c.Id == id);

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
