using Entities.Models;
using FluentValidation;

namespace Entities.Validation
{
  public class BookValidator: AbstractValidator<Book>
  {
    public BookValidator() 
    {
      RuleFor(book => book.ISBN).NotNull().Length(1,50);
      RuleFor(book => book.Name).NotNull().Length(1,50);
      RuleFor(book => book.Jenre).NotNull().Length(1,30);
      RuleFor(book => book.TakeTime).NotNull();
      RuleFor(book => book.ReturnTime).NotNull();
    }
  }
}
