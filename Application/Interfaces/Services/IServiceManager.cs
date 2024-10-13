namespace Application.Interfaces.Services
{
  public interface IServiceManager
  {
    IAuthenticationService AuthenticationService { get; }
  }
}
