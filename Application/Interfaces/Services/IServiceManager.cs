namespace Application.Interfaces.Services
{
  public interface IServiceManager
  {
    IBookService BookService { get; }
    IAuthorService AuthorService { get; }
    IAuthenticationService AuthenticationService { get; }
    IUserBookService UserBookService { get; }
  }
}
