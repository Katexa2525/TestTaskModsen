using MediatR;

namespace Application.Commands
{
  public record DeleteAuthorCommand(Guid Id, bool trackChanges): IRequest<Unit>;
}
