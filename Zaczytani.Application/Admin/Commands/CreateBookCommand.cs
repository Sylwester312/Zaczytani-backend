using MediatR;
using Zaczytani.Application.Filters;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Admin.Commands;

public class CreateBookCommand : IRequest<string>, IUserIdAssignable
{
    public string Title { get; set; } = string.Empty;

    private Guid UserId { get; set; }

    public void SetUserId(Guid userId)
    {
        UserId = userId;
    }

    private class CreateBookCommandHandler(IBookRepository bookRepository) : IRequestHandler<CreateBookCommand, string>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        public Task<string> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult("Book created successfully!");
        }
    }
}
