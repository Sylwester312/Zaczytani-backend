using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zaczytani.Domain.Exceptions;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Admin.Commands
{
    public class DeleteBookCommand:IRequest
    {
        private Guid Id { get; set; }
        public void SetId(Guid id)
        {
            Id = id;
        }
        private class DeleteBookCommandHandler(IBookRepository bookRepository):IRequestHandler<DeleteBookCommand>
        {
            private readonly IBookRepository _bookRepository = bookRepository;
            public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
            {
                var book = await _bookRepository.GetByIdAsync(request.Id, cancellationToken);
                if (book == null)
                {
                    throw new NotFoundException($"Book {request.Id} not found.");
                }
                await _bookRepository.DeleteAsync(book,cancellationToken);
                await _bookRepository.SaveChangesAsync(cancellationToken);
            }
        
        }
    }
}
