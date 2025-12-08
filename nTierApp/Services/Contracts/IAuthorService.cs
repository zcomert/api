using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contracts;

public interface IAuthorService 
{
    IEnumerable<Author> GetAllAuthors(bool trackChanges=false);
    Author GetAuthorById(int id, bool trackChanges=false);
    Author CreateAuthor(Author author);
    void DeleteAuthor(int id, bool trackChanges=false);
    void UpdateAuthor(int id, Author author, bool trackChanges=false);
}
