using MediatR;
using Zaczytani.Application.Exceptions;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Client.Commands;

public record AttachBookCommand(Guid BookShelfId, Guid BookId) : IRequest
{
    public class AttachBookCommandHandler(IBookRepository bookRepository, IBookShelfRepository bookShelfRepository) : IRequestHandler<AttachBookCommand>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IBookShelfRepository _bookShelfRepository = bookShelfRepository;

        public async Task Handle(AttachBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.BookId, cancellationToken)
                ?? throw new NotFoundException("Book with given ID not found");
            var bookShelf = await _bookShelfRepository.GetByIdAsync(request.BookShelfId, cancellationToken)
                ?? throw new NotFoundException("Shelf with given ID not found");

            bookShelf.Books.Add(book);
            await _bookRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
