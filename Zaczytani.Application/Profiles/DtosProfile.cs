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

        CreateMap<Book, ReadingBookDto>();
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

        #region Report
        CreateMap<Report, ReportDto>()
            .ForMember(desc => desc.Review, opt => opt.MapFrom(src => src.Review.Content));
        #endregion

        #region ReadingBook
        CreateMap<Review, CurrentlyReadingBookDto>()
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Book.Title))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Book.Authors))
            .ForMember(dest => dest.Progress, opt => opt.MapFrom(src => src.Progress))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Book.Image));
        #endregion
    }
}
