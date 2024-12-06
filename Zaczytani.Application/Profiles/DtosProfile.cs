using AutoMapper;
using Zaczytani.Application.Client.Commands;
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
        CreateMap<Book, SearchBookDto>()
            .ForMember(x => x.PublishingHouse, opt => opt.MapFrom(src => src.PublishingHouse.Name));
        #endregion

        #region Author
        CreateMap<Author, AuthorDto>();
        #endregion

        #region PublishingHouse
        CreateMap<PublishingHouse, PublishingHouseDto>();
        #endregion

        #region BookRequest
        CreateMap<BookRequest, BookRequestDto>()
            .ForMember(x => x.FileName, opt => opt.MapFrom(src => src.Image));
        CreateMap<BookRequest, UserBookRequestDto>();
        #endregion

        #region User
        CreateMap<User, UserDto>();
        #endregion

        #region Bookshelf
        CreateMap<BookShelf, BookShelfDto>();
        CreateMap<CreateBookShelfCommand, BookShelf>()
           .ForMember(dest => dest.Id, opt => opt.Ignore())
           .ForMember(dest => dest.IsDefault, opt => opt.MapFrom(_ => false));
        #endregion 
    }
}
