using Domain.Entities.DTO;
using Domain.Entities.Models;
using Mapster;

namespace Application.Mapping
{
  public class MapsterUserBookConfig
  {
    public MapsterUserBookConfig()
    {
      TypeAdapterConfig<CreateUserBookDTO, UserBook>.NewConfig()
          .Map(dest => dest.IdBook, src => src.IdBook)
          .Map(dest => dest.IdUser, src => src.IdUser);
    }
  }
}
