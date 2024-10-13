using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public sealed class AuthorNotFoundException : NotFoundException
    {
        public AuthorNotFoundException(Guid authorId) : base($"The author with id: {authorId} doesn't exist in the database.")
        {
        }
    }
}
