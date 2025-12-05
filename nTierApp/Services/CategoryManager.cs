using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services;

public class CategoryManager : ICategoryService
{
    private readonly IRepositoryManager _manager;

    public CategoryManager(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public Category CreateCategory(Category category)
    {
        if (category is null)
            throw new ArgumentNullException(nameof(category));

        _manager.CategoryRepository.Create(category);
        _manager.SaveChanges();
        return category;
    }

    public void DeleteCategory(int id, bool trackChanges = false)
    {
        var category = GetCategoryById(id, trackChanges);
        _manager.CategoryRepository.Delete(category);
        _manager.SaveChanges();
    }

    public IEnumerable<Category> GetAllCategories(bool trackChanges = false)
    {
        return _manager.CategoryRepository.GetAll(null, trackChanges);
    }

    public Category GetCategoryById(int categoryId, bool trackChanges = false)
    {
        var category = _manager
            .CategoryRepository
            .GetOne(c => c.CategoryId.Equals(categoryId));

        if (category is null)
            throw new ArgumentNullException(nameof(category));

        return category;
    }

    public void UpdateCategory(int id, Category category, bool trackChanges = false)
    {
        var entity = GetCategoryById(id, trackChanges);
        entity.CategoryName = category.CategoryName; // güncelleme ifadesi
        _manager.CategoryRepository.Update(entity);
        _manager.SaveChanges();
    }
}