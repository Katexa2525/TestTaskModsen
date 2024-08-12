using Contracts.Services;
using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Service
{
  public sealed class ServiceManager : IServiceManager
  {
    private readonly Lazy<IAuthorService> _authorService;
    private readonly Lazy<IBookService> _bookService;
    private readonly Lazy<IAuthenticationService> _authenticationService;
    private readonly Lazy<IUserBookService> _userBookService;
    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, UserManager<User> userManager,
    IConfiguration configuration)
    {
      _authorService = new Lazy<IAuthorService>(() => new AuthorService(repositoryManager, logger, mapper));
      _bookService = new Lazy<IBookService>(() => new BookService(repositoryManager, logger, mapper));
      _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(logger, mapper, userManager, configuration));
      _userBookService = new Lazy<IUserBookService>(() => new UserBookService(repositoryManager, logger, mapper, userManager));
    }
    public IAuthorService AuthorService => _authorService.Value;
    public IBookService BookService => _bookService.Value;
    public IAuthenticationService AuthenticationService => _authenticationService.Value;
    public IUserBookService UserBookService => _userBookService.Value;
  }
}
