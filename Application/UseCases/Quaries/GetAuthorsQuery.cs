using Domain.Entities.DTO;
using MediatR;

namespace Application.UseCases.Quaries
{
    public sealed record GetAuthorsQuery(bool TrackChanges) : IRequest<IEnumerable<AuthorDTO>>;
}
