﻿using AutoMapper;
using Domain.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;

namespace Application.Services
{
  public sealed class ServiceManager : IServiceManager
  {
    private readonly Lazy<IAuthenticationService> _authenticationService;
    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, UserManager<User> userManager, 
                          IConfiguration configuration)
    {
      _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(logger, mapper, userManager, configuration));
    }
    public IAuthenticationService AuthenticationService => _authenticationService.Value;
  }
}
