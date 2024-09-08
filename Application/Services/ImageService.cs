using Application.Interfaces;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
  public static class ImageService
  {
    public static byte[]? LoadImage(IFormFile Image)
    {
      if (Image != null)
      {
        byte[] imageData = null;
        // считываем переданный файл в массив байтов
        using (var binaryReader = new BinaryReader(Image.OpenReadStream()))
        {
          imageData = binaryReader.ReadBytes((int)Image.Length);
        }
        // установка массива байтов
        return imageData;
      }
      return null;
    }
  }
}
