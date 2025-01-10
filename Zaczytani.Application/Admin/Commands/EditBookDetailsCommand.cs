using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zaczytani.Application.Filters;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Enums;
using Zaczytani.Domain.Exceptions;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Admin.Commands
{
    public class EditBookDetailsCommand : IRequest
    {
        private Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Isbn { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int PageNumber { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string? FileName { get; set; } = string.Empty;
        public List<string> Authors { get; set; } = [];
        public string PublishingHouse { get; set; } = string.Empty;
        public List<BookGenre> Genre { get; set; } = [];
        public string? Series { get; set; } = string.Empty;
        public void SetId(Guid id)
        {
            Id = id;
        }

        private class EditBookDetailsCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository, IPublishingHouseRepository publishingHouseRepository) : IRequestHandler<EditBookDetailsCommand>
        {
            private readonly IBookRepository _bookRepository = bookRepository;
            private readonly IAuthorRepository _authorRepository = authorRepository;
            private readonly IPublishingHouseRepository _publishingHouseRepository = publishingHouseRepository;

            public async Task Handle(EditBookDetailsCommand request, CancellationToken cantellationToken)
            {
                var book = await _bookRepository.GetByIdAsync(request.Id, cantellationToken);
                if(book == null)
                {
                    throw new NotFoundException($"Book {request.Id} not found.");
                }
                book.Title = request.Title;
                book.Isbn = request.Isbn;
                book.Description = request.Description;
                book.PageNumber = request.PageNumber;
                book.ReleaseDate = request.ReleaseDate;
                book.Image = request.FileName;
                book.Genre = request.Genre;
                book.Series = request.Series;

                foreach (var author in request.Authors)
                {
                    var existingAuthor = await _authorRepository.GetByNameAsync(author);

                    if (existingAuthor != null)
                    {
                        book.Authors.Add(existingAuthor);
                    }
                    else
                    {
                        var newAuthor = new Author
                        {
                            Name = author
                        };
                        book.Authors.Add(newAuthor);
                    }
                }
                var existingPublishingHouse = await _publishingHouseRepository.GetByNameAsync(request.PublishingHouse);

                if (existingPublishingHouse != null)
                {
                    book.PublishingHouse = existingPublishingHouse;
                }
                else
                {
                    var newPublishingHouse = new PublishingHouse
                    {
                        Name = request.PublishingHouse
                    };
                    book.PublishingHouse = newPublishingHouse;
                }

                await _bookRepository.EditAsync(book);
                await _bookRepository.SaveChangesAsync(cantellationToken);

            }

        }

    }
}
