using Entities.Exceptions;
using Entities.Models;
using Microsoft.Extensions.Logging;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services;

public class CategoryManager : ICategoryService
{
    private readonly IRepositoryManager _manager;
    private readonly ILogger<CategoryManager> _logger;

    public CategoryManager(IRepositoryManager manager, 
        ILogger<CategoryManager> logger)
    {
        _manager = manager;
        _logger = logger;
    }

    public Category CreateCategory(Category category)
    {
        if (category is null)
        {
            _logger.LogError("Category object sent from client is null.");
            throw new ArgumentNullException(nameof(category));
        }

        _manager.CategoryRepository.Create(category);
        _manager.SaveChanges();
        _logger.LogInformation($"Kategory id: {category.CategoryId} oluşturuldu.");
        return category;
    }

    public void DeleteCategory(int id, bool trackChanges = false)
    {
        var category = GetCategoryById(id, trackChanges);
        _manager.CategoryRepository.Delete(category);
        _manager.SaveChanges();
        _logger.LogInformation($"Kategory id: {id} silindi.");
    }

    public IEnumerable<Category> GetAllCategories(bool trackChanges = false)
    {
        _logger.LogInformation("Tüm kategoriler getirildi.");
        return _manager.CategoryRepository.GetAll(null, trackChanges);
    }

    public Category GetCategoryById(int categoryId, bool trackChanges = false)
    {
        var category = _manager
            .CategoryRepository
            .GetOne(c => c.CategoryId.Equals(categoryId));

        if (category is null)
        {
            //_logger.LogError($"Kategory id: {categoryId} bulunamadı.");
            throw new CategoryNotFoundException(categoryId);
        }

        return category;
    }

    public void UpdateCategory(int id, Category category, bool trackChanges = false)
    {
        var entity = GetCategoryById(id, trackChanges);
        entity.CategoryName = category.CategoryName; // güncelleme ifadesi
        _manager.CategoryRepository.Update(entity);
        _manager.SaveChanges();
        _logger.LogInformation($"Kategory id: {id} güncellendi.");
    }
}