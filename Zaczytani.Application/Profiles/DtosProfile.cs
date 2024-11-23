using AutoMapper;
using Zaczytani.Application.Dtos;
using Zaczytani.Domain.Entities;

namespace Zaczytani.Application.Profiles;

internal class DtosProfile : Profile
{
    public DtosProfile()
    {
        #region Book
        CreateMap<Book, BookDto>();
        #endregion

        #region Author
        CreateMap<Author, AuthorDto>();
        #endregion
    }
}
