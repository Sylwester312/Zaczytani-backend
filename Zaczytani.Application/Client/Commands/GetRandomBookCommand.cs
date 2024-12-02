using MediatR;
using AutoMapper;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Repositories;
using Zaczytani.Application.Filters;
using Microsoft.EntityFrameworkCore;
using Zaczytani.Application.Dtos;

public record GetRandomBookCommand : IRequest<BookDto>, IUserIdAssignable
{
    public Guid UserId { get; private set; }

    public void SetUserId(Guid userId)
    {
        UserId = userId;
    }

    private class Handler(IUserDrawnBookRepository userDrawnBookRepository, IBookRepository bookRepository, IMapper mapper) : IRequestHandler<GetRandomBookCommand, BookDto>
    {
        private readonly IUserDrawnBookRepository _userDrawnBookRepository = userDrawnBookRepository;
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BookDto> Handle(GetRandomBookCommand request, CancellationToken cancellationToken)
        {
            var existingDrawnBook = await _userDrawnBookRepository.GetDrawnBookByUserIdAndDateAsync(request.UserId, DateTime.UtcNow.Date, cancellationToken);
            if (existingDrawnBook != null)
            {
                return _mapper.Map<BookDto>(existingDrawnBook.Book);
            }

            var randomBook = await _bookRepository.GetAll()
                .OrderBy(_ => Guid.NewGuid())
                .FirstOrDefaultAsync(cancellationToken);

            if (randomBook == null)
            {
                throw new InvalidOperationException("No books available to draw.");
            }

            var userDrawnBook = new UserDrawnBook
            {
                UserId = request.UserId,
                BookId = randomBook.Id,
                DrawnDate = DateTime.UtcNow.Date
            };

            await _userDrawnBookRepository.AddAsync(userDrawnBook);
            await _userDrawnBookRepository.SaveChangesAsync();

            return _mapper.Map<BookDto>(randomBook);
        }
    }
}
