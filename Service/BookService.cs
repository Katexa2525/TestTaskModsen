using AutoMapper;
using Contracts;
using Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
  internal sealed class BookService: IBookService
  {
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _maper;

    public BookService(IRepositoryManager repository, ILoggerManager logger, IMapper maper) 
    {
      _repository = repository;
      _logger = logger;
      _maper = maper;
    }
  }
}
