using Entities.Models;
using System.Linq.Expressions;

namespace Services.Contracts;

public interface ICategoryService
{
    IEnumerable<Category> GetAllCategories(bool trackChanges=false);
    Category GetCategoryById(int categoryId, bool trackChanges=false);
    Category CreateCategory(Category category);
    void DeleteCategory(int id, bool trackChanges=false);
    void UpdateCategory(int id, Category category, bool trackChanges=false);

}
