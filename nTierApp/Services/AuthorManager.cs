using Entities.Models;
using Microsoft.Extensions.Logging;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services;

public class AuthorManager : IAuthorService
{
    private readonly IRepositoryManager _manager;
    private readonly ILogger<AuthorManager> _logger;

    public AuthorManager(IRepositoryManager manager, 
        ILogger<AuthorManager> logger)
    {
        _manager = manager;
        _logger = logger;
    }

    public Author CreateAuthor(Author author)
    {
        if(author is null)
        {
            _logger.LogError("Yazar boş olamaz!");
            throw new ArgumentNullException(nameof(author));
        }
        _manager.AuthorRepository.Create(author);
        _manager.SaveChanges();
        _logger.LogInformation($"Yeni yazar eklendi: {author.FirstName} {author.LastName}");
        return author;
    }

    public void DeleteAuthor(int id, bool trackChanges = false)
    {
        var author = GetAuthorById(id, trackChanges);
        if(author is null)
        {
            _logger.LogError($"Yazar bulunamadı. Id: {id}");
            throw new ArgumentNullException(nameof(author));
        }
        _manager.AuthorRepository.Delete(author);
        _manager.SaveChanges();
        _logger.LogInformation($"Yazar silindi. Id: {id}");
    }

    public IEnumerable<Author> GetAllAuthors(bool trackChanges = false)
    {
        _logger.LogInformation("Tüm yazarlar getirildi.");
        return _manager.AuthorRepository.GetAll(null,trackChanges);
    }

    public Author GetAuthorById(int id, bool trackChanges = false)
    {
        var author = _manager
            .AuthorRepository
            .GetOne(a => a.AuthorId.Equals(id), trackChanges);

        if(author is null)
        {
            _logger.LogError($"Yazar bulunamadı. Id: {id}");
            throw new ArgumentNullException(nameof(author));
        }
        _logger.LogInformation($"Yazar getirildi. Id: {id}");
        return author;
    }

    public void UpdateAuthor(int id, Author author, bool trackChanges = false)
    {
        var authorEntity = GetAuthorById(id, trackChanges);
        authorEntity.FirstName = author.FirstName;
        authorEntity.LastName = author.LastName;
        _manager.SaveChanges();
        _logger.LogInformation($"Yazar güncellendi. Id: {id}");
    }
}
