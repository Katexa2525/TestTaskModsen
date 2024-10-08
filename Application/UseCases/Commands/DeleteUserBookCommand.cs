﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Commands
{
    public record DeleteUserBookCommand(Guid bookId, string userName, bool trackChanges) : IRequest<Unit>;
}
