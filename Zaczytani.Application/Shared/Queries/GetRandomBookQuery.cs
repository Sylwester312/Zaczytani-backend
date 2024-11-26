using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zaczytani.Application.Dtos;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Shared.Queries;

public class GetRandomBookQuery : IRequest<BookDto>
{
    private class GetRandomBookQueryHandler(IBookRepository bookRepository, IMapper mapper) : IRequestHandler<GetRandomBookQuery, BookDto>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BookDto> Handle(GetRandomBookQuery request, CancellationToken cancellationToken)
        {
            var randomBook = await _bookRepository.GetAll()
                .OrderBy(r => Guid.NewGuid())
                .FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<BookDto>(randomBook);
        }
    }
}
