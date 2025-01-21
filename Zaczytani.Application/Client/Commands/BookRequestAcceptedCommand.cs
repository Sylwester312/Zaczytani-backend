using MediatR;
using Microsoft.Extensions.Configuration;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Enums;
using Zaczytani.Domain.Exceptions;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Client.Commands;

public record BookRequestAcceptedCommand(Guid Id):IRequest
{
    private class BookRequestAcceptedCommandHandler(IEmailInfoRepository emailInfoRepository,IBookRequestRepository bookRequestRepository,IConfiguration configuration):IRequestHandler<BookRequestAcceptedCommand>
    {
        private readonly IBookRequestRepository _bookRequestRepository=bookRequestRepository;
        private readonly IEmailInfoRepository _emailInfoRepository = emailInfoRepository;
        private readonly string _frontendUrl = configuration.GetSection("FrontendUrl").Value
           ?? throw new InvalidOperationException("Frontend URL is not configured. Please set 'FrontendUrl' in appsettings.json.");
        public async Task Handle(BookRequestAcceptedCommand request,CancellationToken cancellationToken)
        {
            var bookRequest = await _bookRequestRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException($"Book Request with ID {request.Id} was not found.");
            if(bookRequest.User.Email is null)
            {
                return;
            }
            var emailInfo = new EmailInfo()
            {
                EmailTo = bookRequest.User.Email,
                //EmailContent = [bookRequest.Book.Title, string.Format("{0}/review/{1}", _frontendUrl, book.Id)],
                EmailTemplate = EmailTemplate.CommentAdded,
            };

        }
    }
}
