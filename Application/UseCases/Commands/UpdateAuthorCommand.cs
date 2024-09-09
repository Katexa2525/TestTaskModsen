using Domain.Entities.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Commands
{
    public sealed record UpdateAuthorCommand(Guid authorId, UpdateAuthorDTO UpdateAuthor, bool trackChanges) : IRequest<Unit>;
}
