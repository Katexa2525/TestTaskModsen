﻿using Domain.Entities.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Quaries
{
    public sealed record GetAuthorByIdQuery(Guid Id, bool trackChanges) : IRequest<AuthorDTO>;
}
