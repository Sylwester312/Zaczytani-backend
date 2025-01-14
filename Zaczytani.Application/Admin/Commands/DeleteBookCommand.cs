using MediatR;
using Zaczytani.Domain.Exceptions;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Admin.Commands
{
    public record DeleteBookCommand(Guid Id) : IRequest
    {
        private class DeleteBookCommandHandler(IBookRepository bookRepository) : IRequestHandler<DeleteBookCommand>
        {
            private readonly IBookRepository _bookRepository = bookRepository;
            public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
            {
                var book = await _bookRepository.GetByIdAsync(request.Id, cancellationToken);
                if (book == null)
                {
                    throw new NotFoundException($"Book {request.Id} not found.");
                }
                await _bookRepository.DeleteAsync(book, cancellationToken);
                await _bookRepository.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
