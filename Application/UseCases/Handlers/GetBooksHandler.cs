using Application.Interfaces.Repository;
using Application.Mapping;
using Application.UseCases.Quaries;
using Domain.Entities.DTO;
using Domain.RequestFeatures;
using Mapster;
using MediatR;

namespace Application.UseCases.Handlers
{
    internal sealed class GetBooksHandler : IRequestHandler<GetBooksQuery, (IEnumerable<BookDTO> books, MetaData metaData)>
    {
        private readonly IRepositoryManager _repository;
        public GetBooksHandler(IRepositoryManager repository) => _repository = repository;
        public async Task<(IEnumerable<BookDTO> books, MetaData metaData)> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var booksWithMetaData = await _repository.Book.GetAllBooksAsync(request.bookParameters, request.trackChanges);
            IEnumerable<BookDTO> booksDTO = booksWithMetaData.Select(booksWithMetaData => booksWithMetaData.Adapt<BookDTO>());
            return (books: booksDTO, metaData: booksWithMetaData.MetaData);
        }
    }
}
