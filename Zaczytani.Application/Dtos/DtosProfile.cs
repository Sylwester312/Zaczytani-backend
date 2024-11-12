using AutoMapper;
using Zaczytani.Domain.Entities;

namespace Zaczytani.Application.Dtos;

internal class DtosProfile : Profile
{
    public DtosProfile()
    {
        #region Book
        CreateMap<Book, BookDto>();
        #endregion

        #region
        CreateMap<Author, AuthorDto>();
        #endregion
    }
}
