using Application.Interfaces.Repository;
using Application.UseCases.Commands;
using Application.Exceptions;
using MediatR;

namespace Application.UseCases.Handlers
{
    internal sealed class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand, Unit>
    {
        private readonly IRepositoryManager _repository;
        public DeleteAuthorHandler(IRepositoryManager repository) => _repository = repository;
        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _repository.Author.GetAuthorByIdAsync(request.Id, request.trackChanges);
            if (author is null)
                throw new AuthorNotFoundException(request.Id);
            _repository.Author.DeleteAuthor(author);
            await _repository.SaveAsync();
            return Unit.Value;
        }
    }
}
