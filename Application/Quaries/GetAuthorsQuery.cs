using Domain.Entities.DTO;
using MediatR;

namespace Application.Quaries
{
  public sealed record GetAuthorsQuery(bool TrackChanges) : IRequest<IEnumerable<AuthorDTO>>;
}
