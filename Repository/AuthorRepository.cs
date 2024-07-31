﻿using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
  public class AuthorRepository: RepositoryBase<Author>, IAuthorRepository
  {
    public AuthorRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public IEnumerable<Author> GetAllAuthors(bool trackChanges) => FindAll(trackChanges).OrderBy(c => c.Name).ToList();

    public Author GetAuthorById(Guid authorId, bool trackChanges) => FindByCondition(c => c.Id.Equals(authorId), trackChanges).SingleOrDefault();
  }
}
