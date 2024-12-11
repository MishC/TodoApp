using TodoApp.Shared.Models;

namespace TodosApi.Service
{
    public interface ICategoriesService
    {
        IEnumerable<Category> GetCategories();
        Category? GetCategoryById(int id);
        void AddCategory(Category category);
        void UpdateCategory(int id, Category updatedCategory);
        void DeleteCategory(int id);
        IEnumerable<Category> GetImportantCategories();
    }
}
