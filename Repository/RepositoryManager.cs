using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
  public class RepositoryManager : IRepositoryManager
  {
    private RepositoryContext _repositoryContext;
    private IBookRepository _bookRepository;
    private IAuthorRepository _authorRepository;
    private IUserBookRepository _userBookRepository;
    private IUserRepository _userRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
      _repositoryContext = repositoryContext;
    }

     public IAuthorRepository Author 
     {
      get 
      {
        if (_authorRepository == null)
          _authorRepository = new AuthorRepository(_repositoryContext);
        return _authorRepository;
      }
     }

    public IBookRepository Book
    {
      get
      {
        if (_bookRepository == null)
          _bookRepository = new BookRepository(_repositoryContext);
        return _bookRepository;
      }
    }

    public IUserRepository User
    {
      get
      {
        if (_userRepository == null)
          _userRepository = new UserRepository(_repositoryContext);
        return _userRepository;
      }
    }

    public IUserBookRepository UserBook
    {
      get
      {
        if (_userBookRepository == null)
          _userBookRepository = new UserBookRepository(_repositoryContext);
        return _userBookRepository;
      }
    }

    public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
  }
}
