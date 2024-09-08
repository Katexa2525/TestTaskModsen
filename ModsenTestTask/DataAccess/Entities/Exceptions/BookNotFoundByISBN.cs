using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Exceptions
{
    public sealed class BookNotFoundByISBN : NotFoundException
    {
        public BookNotFoundByISBN(string ISBN) : base($"The book with ISBN: {ISBN} doesn't exist in the database.")
        {
        }
    }
}
