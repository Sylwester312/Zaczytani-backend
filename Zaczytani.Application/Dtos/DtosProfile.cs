using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zaczytani.Domain.Entities;

namespace Zaczytani.Application.Dtos;

internal class DtosProfile : Profile
{
    public class DtosProfile : Profile
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
