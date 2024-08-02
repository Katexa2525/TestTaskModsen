using Contracts.Services;
using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Service
{
  public sealed class ServiceManager : IServiceManager
  {
    private readonly Lazy<IAuthorService> _authorService;
    private readonly Lazy<IBookService> _bookService;
    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
    {
      _authorService = new Lazy<IAuthorService>(() => new AuthorService(repositoryManager, logger, mapper));
      _bookService = new Lazy<IBookService>(() => new BookService(repositoryManager, logger, mapper));
    }
    public IAuthorService AuthorService => _authorService.Value;
    public IBookService BookService => _bookService.Value;
  }
}
