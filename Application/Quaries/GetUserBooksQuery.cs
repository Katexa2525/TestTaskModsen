using Domain.Entities.DTO;
using MediatR;

namespace Application.Quaries
{
  public sealed record GetUserBooksQuery(bool trackChanges) : IRequest<IEnumerable<UserBookDTO>>;
}
