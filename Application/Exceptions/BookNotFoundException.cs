using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public sealed class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException(Guid id) : base($"The book with id: {id} doesn't exist in the database.")
        {
        }
    }
}
