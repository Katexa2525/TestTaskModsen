using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public sealed class UserBookNotFoundException : NotFoundException
    {
        public UserBookNotFoundException(Guid id) : base($"The userbook with bookid: {id} doesn't exist in the database.")
        {
        }
    }
}
