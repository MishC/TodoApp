using TodoApp.Classes;
using TodosApi.Repository;
using Serilog;

namespace TodosApi.Service
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public IEnumerable<Category> GetCategories() => _categoriesRepository.GetCategories();

        public Category? GetCategoryById(int id) => _categoriesRepository.GetCategoryById(id);

        public void AddCategory(Category category)
        {
            if (category == null)
            {
                Log.Warning("Category cannot be null.");
                return;
            }

            _categoriesRepository.AddCategory(category);
            Log.Information($"Category {category.Name} added.");
        }

        public void UpdateCategory(int id, Category updatedCategory)
        {
            var category = _categoriesRepository.GetCategoryById(id);
            if (category == null)
            {
                Log.Warning($"Category with id {id} doesn't exist.");
                return;
            }

            category.Name = updatedCategory.Name;
            category.Description = updatedCategory.Description;
            category.Priority = updatedCategory.Priority;

            _categoriesRepository.UpdateCategory(category);
            Log.Information($"Category with id {id} was updated.");
        }

        public void DeleteCategory(int id)
        {
            var category = _categoriesRepository.GetCategoryById(id);
            if (category == null)
            {
                Log.Warning($"Category with id:{id} doesn't exist.");
                return;
            }

            _categoriesRepository.DeleteCategory(id);
            Log.Information($"Category with id:{id} was deleted.");
        }

        public IEnumerable<Category> GetImportantCategories()
        {
            return _categoriesRepository.GetCategories()
                                         .Where(c => c.Priority)
                                         .ToList();
        }
    }
}
