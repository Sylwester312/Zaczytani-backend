using AutoMapper;
using Zaczytani.Application.Dtos;
using Zaczytani.Domain.Entities;

namespace Zaczytani.Application.Profiles;

internal class DtosProfile : Profile
{
    public DtosProfile()
    {
        #region Book
        CreateMap<Book, BookDto>()
            .ForMember(x => x.PublishingHouse, opt => opt.MapFrom(src => src.PublishingHouse.Name));
        #endregion

        #region Author
        CreateMap<Author, AuthorDto>();
        #endregion

        #region PublishingHouse
        CreateMap<PublishingHouse, PublishingHouseDto>();
        #endregion

        #region BookRequest
        CreateMap<BookRequest, BookRequestDto>();
        #endregion

        #region User
        CreateMap<User, UserDto>();
        #endregion
    }
}
