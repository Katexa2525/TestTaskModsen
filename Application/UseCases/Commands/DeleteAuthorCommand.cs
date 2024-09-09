using MediatR;

namespace Application.UseCases.Commands
{
    public record DeleteAuthorCommand(Guid Id, bool trackChanges) : IRequest<Unit>;
}
