using MediatR;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Filters;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Enums;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Admin.Commands;

public class CreateBookCommand : IRequest<Guid>, IUserIdAssignable
{
    public string Title { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int PageNumber { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public string? FileName { get; set; } = string.Empty;
    public List<AuthorDto> Authors { get; set; } = [];
    public PublishingHouseDto PublishingHouse { get; set; } = new();
    public List<BookGenre> Genre { get; set; } = [];
    public string? Series { get; set; } = string.Empty;

    private Guid UserId { get; set; }

    public void SetUserId(Guid userId)
    {
        UserId = userId;
    }

    private class CreateBookCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository, IPublishingHouseRepository publishingHouseRepository) : IRequestHandler<CreateBookCommand, Guid>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IAuthorRepository _authorRepository = authorRepository;
        private readonly IPublishingHouseRepository _publishingHouseRepository = publishingHouseRepository;
        public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = request.Title,
                Description = request.Description,
                Isbn = request.Isbn,
                PageNumber = request.PageNumber,
                Image = request.FileName,
                UserId = request.UserId,
                Genre = request.Genre,
                Series = request.Series,
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

          
            var existingPublishingHouse = await _publishingHouseRepository.GetByIdAsync(request.PublishingHouse.Id);

            if (existingPublishingHouse != null)
            {
                book.PublishingHouse = existingPublishingHouse;
            }
            else
            {
                var newPublishingHouse = new PublishingHouse
                {
                    Name = request.PublishingHouse.Name
                };
                book.PublishingHouse = newPublishingHouse;
            }

            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();

            return book.Id;
        }
    }
}
