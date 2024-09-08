using Domain.Entities.DTO;
using Domain.RequestFeatures;
using MediatR;

namespace Application.Quaries
{
  public sealed record GetBooksQuery(BookParameters bookParameters, bool trackChanges) : IRequest<(IEnumerable<BookDTO> books, MetaData metaData)>;
}
