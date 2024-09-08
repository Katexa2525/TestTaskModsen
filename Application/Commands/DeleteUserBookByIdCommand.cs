using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
  public record DeleteUserBookByIdCommand(Guid IdUserBook, bool trackChanges) : IRequest<Unit>;
}
