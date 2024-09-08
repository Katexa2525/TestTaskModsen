using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
  public record DeleteBookCommand(Guid authorId, Guid id, bool trackChanges) : IRequest<Unit>;
}
