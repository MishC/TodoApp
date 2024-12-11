using TodosApi.Models;
namespace TodosApi.Repository
{
    public interface ICategoriesRepository
    {
        IQueryable<Category> GetCategories();
        Category? GetCategoryById(int id);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);
    }
}
