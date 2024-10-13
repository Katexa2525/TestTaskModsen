using Domain.Entities.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation
{
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(author => author.Name).NotNull().Length(1, 30);
            RuleFor(author => author.Surname).NotNull().Length(1, 30);
        }
    }
}
