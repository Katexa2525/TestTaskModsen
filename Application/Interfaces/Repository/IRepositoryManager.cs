namespace Application.Interfaces.Repository
{
    public interface IRepositoryManager
    {
        IAuthorRepository Author { get; }
        IBookRepository Book { get; }
        IUserRepository User { get; }
        IUserBookRepository UserBook { get; }
        Task SaveAsync();
    }
}
