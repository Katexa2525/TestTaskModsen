using Domain.Entities.DTO;
using MediatR;

namespace Application.UseCases.Quaries
{
    public sealed record GetUserBookByIdQuery(Guid bookId, string userName, bool trackChanges) : IRequest<UserBookDTO>;
}
