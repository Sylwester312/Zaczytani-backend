using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zaczytani.Domain.Entities;

namespace Zaczytani.Application.Dtos
{
    public class DtosProfile : Profile
    {
        public DtosProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src =>
                    src.Authors == null ? string.Empty : string.Join(", ", src.Authors.Select(a => a.Name))));
        }
    }
}
