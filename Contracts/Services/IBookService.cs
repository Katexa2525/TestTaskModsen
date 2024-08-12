﻿using Entities.DTO;
using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Services
{
  public interface IBookService
  {
    Task<(IEnumerable<BookDTO> books, MetaData metaData)> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges);
    Task<IEnumerable<BookDTO>> GetBookByAuthorAsync(Guid authorId, bool trackChanges);
    Task<BookDTO> GetBookByIdAsync(Guid authorId, Guid id, bool trackChanges);
    Task<BookDTO> GetBookByISBNAsync(string ISBN, bool trackChanges);
    Task DeleteBookAsync(Guid authorId, Guid id, bool trackChanges);
    Task<BookDTO> CreateBookAsync(Guid authorId, CreateUpdateBookDTO createBook, bool trackChanges);
    Task UpdateBookAsync(Guid authorId, Guid id, CreateUpdateBookDTO bookUpdate, bool authTrackChanges, bool bookTrackChanges);
  }
}
