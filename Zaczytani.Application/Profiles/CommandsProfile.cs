using AutoMapper;
using Zaczytani.Application.Admin.Commands;
using Zaczytani.Application.Client.Commands;
using Zaczytani.Domain.Entities;

namespace Zaczytani.Application.Profiles;

internal class CommandsProfile : Profile
{
    public CommandsProfile()
    {
        CreateMap<CreateBookRequestCommand, BookRequest>();
        CreateMap<AcceptBookRequestCommand, CreateBookCommand>();
    }
}
