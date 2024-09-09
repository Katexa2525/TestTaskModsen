using Domain.Entities.DTO;
using Domain.Entities.Models;

namespace Application.Mapping
{
  public static class BookMapping
  {
    public static BookDTO ToBookResponse(this Book book)
    {
      return new BookDTO{
          Id = book.Id,
          ISBN = book.ISBN,
          Name = book.Name,
          Jenre = book.Jenre,
          TakeTime = book.TakeTime,
          ReturnTime = book.ReturnTime
      };
    }

    public static Book ToBook(this CreateUpdateBookDTO request)
    {
      return new Book
      {
        ISBN = request.ISBN,
        Name = request.Name,
        Jenre = request.Jenre,
        TakeTime = request.TakeTime,
        ReturnTime = request.ReturnTime
      };
    }
    public static Book ToBook(this CreateUpdateBookDTO request, Book book)
    {
      book.ISBN = request.ISBN;
      book.Name = request.Name;
      book.Jenre = request.Jenre;
      book.TakeTime = request.TakeTime;
      book.ReturnTime = request.ReturnTime;
      return book;
    }
  }
}
