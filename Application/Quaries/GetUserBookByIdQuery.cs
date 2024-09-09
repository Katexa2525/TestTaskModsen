using Domain.Entities.DTO;
using MediatR;

namespace Application.Quaries
{
  public sealed record GetUserBookByIdQuery(Guid bookId, string userName, bool trackChanges) : IRequest<UserBookDTO>;
}
