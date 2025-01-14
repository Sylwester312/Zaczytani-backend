using MediatR;
using Zaczytani.Application.Filters;
using Zaczytani.Domain.Enums;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Client.Commands;

public record ReadBookCommand(Guid BookId) : IRequest, IUserIdAssignable
{
    private Guid UserId { get; set; }

    public void SetUserId(Guid userId) => UserId = userId;

    private class ReadBookCommandHandler(IBookShelfRepository bookShelfRepository, IMediator mediator) : IRequestHandler<ReadBookCommand>
    {
        private readonly IBookShelfRepository _bookShelfRepository = bookShelfRepository;
        private readonly IMediator _mediator = mediator;

        public async Task Handle(ReadBookCommand request, CancellationToken cancellationToken)
        {
            var readBookShelf = await _bookShelfRepository.GetBookShelfByTypeAsync(BookShelfType.Read, request.UserId, cancellationToken);
            var readingBookShelf = await _bookShelfRepository.GetBookShelfByTypeAsync(BookShelfType.Reading, request.UserId, cancellationToken);

            if (readBookShelf != null && readingBookShelf != null)
            {
                var attachCommand = new AttachBookCommand(readBookShelf.Id, request.BookId);
                await _mediator.Send(attachCommand, cancellationToken);

                var detachCommand = new DetachBookCommand(readingBookShelf.Id, request.BookId);
                await _mediator.Send(detachCommand, cancellationToken);

                var updateProgressesCommand = new UpdateChallengeProgressesCommand(request.BookId, true);
                await _mediator.Send(updateProgressesCommand, cancellationToken);
            }
        }
    }
}
