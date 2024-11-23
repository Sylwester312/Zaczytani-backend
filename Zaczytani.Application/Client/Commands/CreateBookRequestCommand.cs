using MediatR;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Filters;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Client.Commands;

public class CreateBookRequestCommand : IRequest<Guid>, IUserIdAssignable
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string? Isbn { get; set; } = string.Empty;
    public string? FileName { get; set; } = string.Empty;
    public int? PageNumber { get; set; }
    public DateOnly? ReleaseDate { get; set; }
    public List<AuthorDto> Authors { get; set; } = [];
    private Guid UserId { get; set; }

    public void SetUserId(Guid userId)
    {
        UserId = userId;
    }

    private class CreateBookRequestCommandHandler(IBookRequestRepository bookRequestRepository, IAuthorRepository authorRepository) : IRequestHandler<CreateBookRequestCommand, Guid>
    {
        private readonly IBookRequestRepository _bookRequestRepository = bookRequestRepository;
        private readonly IAuthorRepository _authorRepository = authorRepository;
        public async Task<Guid> Handle(CreateBookRequestCommand request, CancellationToken cancellationToken)
        {
            var book = new BookRequest
            {
                Title = request.Title,
                Description = request.Description,
                Isbn = request.Isbn,
                PageNumber = request.PageNumber,
                Image = request.FileName,
                CreatedById = request.UserId,
                ReleaseDate = request.ReleaseDate,
            };

            foreach (var authorDto in request.Authors)
            {
                var existingAuthor = await _authorRepository.GetByIdAsync(authorDto.Id);

                if (existingAuthor != null)
                {
                    book.Authors.Add(existingAuthor);
                }
                else
                {
                    var newAuthor = new Author
                    {
                        Id = authorDto.Id == Guid.Empty ? Guid.NewGuid() : authorDto.Id,
                        Name = authorDto.Name
                    };
                    book.Authors.Add(newAuthor);
                }
            }

            await _bookRequestRepository.AddAsync(book);
            await _bookRequestRepository.SaveChangesAsync();

            return book.Id;
        }
    }
}
