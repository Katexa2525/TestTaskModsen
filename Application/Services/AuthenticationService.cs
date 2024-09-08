﻿using AutoMapper;
using Domain.Entities.DTO;
using Domain.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Application.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Application.Mapping;


namespace Application.Services
{
  internal sealed class AuthenticationService : IAuthenticationService
  {
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    private User? _user;

    public AuthenticationService(ILoggerManager logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
    {
      _logger = logger;
      _mapper = mapper;
      _userManager = userManager;
      _configuration = configuration;
    }

    public async Task<IdentityResult> RegisterUser(UserForRegistrationDTO userForRegistration)
    {
      User user = UserMapping.ToUser(userForRegistration);
      var result = await _userManager.CreateAsync(user, userForRegistration.PasswordHash);
      if (result.Succeeded)
        await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
      return result;
    }

    public async Task<bool> ValidateUser(UserForAuthenticationDTO userForAuth)
    {
      _user = await _userManager.FindByNameAsync(userForAuth.UserName);
      var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));
      if (!result)
        _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong user name or password.");
      return result;
    }
    public async Task<string> CreateToken()
    {
      var signingCredentials = GetSigningCredentials();
      var claims = await GetClaims();
      var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
      return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
    private SigningCredentials GetSigningCredentials()
    {
      var key = Encoding.UTF8.GetBytes(_configuration.GetSection("SECRET").ToString());
      var secret = new SymmetricSecurityKey(key);
      return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }
    private async Task<List<Claim>> GetClaims()
    {
      var claims = new List<Claim>
      {
        new Claim(ClaimTypes.Name, _user.UserName)
      };
      var roles = await _userManager.GetRolesAsync(_user);
      foreach (var role in roles)
      {
        claims.Add(new Claim(ClaimTypes.Role, role));
      }
      return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
      var jwtSettings = _configuration.GetSection("JwtSettings");
      var tokenOptions = new JwtSecurityToken
      (
      issuer: jwtSettings["validIssuer"],
      audience: jwtSettings["validAudience"],
      claims: claims,
      expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
      signingCredentials: signingCredentials
      );
      return tokenOptions;
    }
  }
}
