using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories;

public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
{
    public AuthorRepository(RepositoryContext context) 
        : base(context)
    {

    }
}
