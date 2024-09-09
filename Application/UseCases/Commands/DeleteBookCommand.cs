using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Commands
{
    public record DeleteBookCommand(Guid authorId, Guid id, bool trackChanges) : IRequest<Unit>;
}
