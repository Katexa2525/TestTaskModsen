using Domain.Entities.DTO;
using Application.RequestFeatures;
using MediatR;

namespace Application.UseCases.Quaries
{
    public sealed record GetBooksQuery(BookParameters bookParameters, bool trackChanges) : IRequest<(IEnumerable<BookDTO> books, MetaData metaData)>;
}
