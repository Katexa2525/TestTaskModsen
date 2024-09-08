using Domain.Entities.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
  public sealed record CreateBookCommand(Guid authorId, CreateUpdateBookDTO Book, bool trackChanges) : IRequest<BookDTO>;
}
