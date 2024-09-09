using Domain.Entities.DTO;
using MediatR;

namespace Application.UseCases.Quaries
{
    public sealed record GetUserBooksQuery(bool trackChanges) : IRequest<IEnumerable<UserBookDTO>>;
}
