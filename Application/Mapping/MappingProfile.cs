using AutoMapper;
using Domain.Entities.DTO;
using Domain.Entities.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dest => dest.BirthdayDate, opt => opt.MapFrom(src => src.BirthdayDate))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country));

            CreateMap<Book, BookDTO>();
            CreateMap<CreateAuthorDTO, Author>();
            CreateMap<UpdateAuthorDTO, Author>();
            CreateMap<CreateUpdateBookDTO, Book>();
            CreateMap<UserForRegistrationDTO, User>();
        }
    }
}
