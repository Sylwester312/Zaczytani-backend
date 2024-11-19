using MediatR;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Filters;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Admin.Commands;

public class CreateBookCommand : IRequest<Guid>, IUserIdAssignable
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public int PageNumber { get; set; }
    public List<AuthorDto> Authors { get; set; } = new();
    private Guid UserId { get; set; }

    public void SetUserId(Guid userId)
    {
        UserId = userId;
    }

    private class CreateBookCommandHandler(IBookRepository bookRepository) : IRequestHandler<CreateBookCommand, Guid>
    {
        public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = request.Title,
                Description = request.Description,
                Isbn = request.Isbn,
                PageNumber = request.PageNumber,
                Image = request.FileName,
            };
    
            foreach (var authorDto in request.Authors)
            {
                var existingAuthor = await bookRepository.GetAuthorByIdAsync(authorDto.Id);

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

            await bookRepository.AddAsync(book);
            await bookRepository.SaveChangesAsync();

            return book.Id;
        }
    }
}
