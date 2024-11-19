using AutoMapper;
using MediatR;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Exceptions;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Shared.Queries;

public record GetBookDetailsQuery(Guid BookId) : IRequest<BookDto>
{
    private class GetBookDetailsQueryHandler(IBookRepository bookRepository, IFileStorageRepository fileStorageRepository, IMapper mapper)
        : IRequestHandler<GetBookDetailsQuery, BookDto>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IFileStorageRepository _fileStorageRepository = fileStorageRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<BookDto> Handle(GetBookDetailsQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.BookId)
                ?? throw new NotFoundException($"Book with ID {request.BookId} was not found.");
            
            var bookDto = new BookDto(
                Id: book.Id,
                Title: book.Title,
                Isbn: book.Isbn,
                Description: book.Description,
                ImageUrl: _fileStorageRepository.GetFileUrl(book.Image),
                PageNumber: book.PageNumber,
                Authors: _mapper.Map<IEnumerable<AuthorDto>>(book.Authors));
            
            return bookDto;
        }
    }
}
