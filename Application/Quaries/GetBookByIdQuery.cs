using Domain.Entities.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Quaries
{
  public sealed record GetBookByIdQuery(Guid authorId,Guid Id, bool trackChanges) : IRequest<BookDTO>;
}
