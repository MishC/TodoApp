using TodosApi.Models;

namespace TodosApi.Service
{
    public interface ICategoriesService
    {
        IQueryable<Category> GetCategories();
        Category? GetCategoryById(int id);
        void AddCategory(Category category);
        void UpdateCategory(int id, Category updatedCategory);
        void DeleteCategory(int id);
        bool CategoryExists(int id);
    }
}
