using TodoApp.Classes;

namespace TodosApi.Repository
{
    public interface ICategoriesRepository
    {
        IEnumerable<Category> GetCategories();
        Category? GetCategoryById(int id);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);
    }
}
