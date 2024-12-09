using AutoMapper;
using MediatR;
using Zaczytani.Application.Exceptions;
using Zaczytani.Application.Filters;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Client.Commands;

public class CreateReviewCommand : IRequest<Guid>, IUserIdAssignable
{
    public string? Content { get; set; }

    public int? Rating { get; set; }

    public int Progress { get; set; }

    public bool IsFinal { get; set; } = false;

    public bool? ContainsSpoilers { get; set; }

    protected Guid BookId { get; set; }

    public void SetBookId(Guid bookId) => BookId = bookId;

    private Guid UserId { get; set; }

    public void SetUserId(Guid userId) => UserId = userId;
    
    private class CreateReviewCommandHandler(IBookRepository bookRepository, IReviewRepository reviewRepository, IMapper mapper) : IRequestHandler<CreateReviewCommand, Guid>
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IReviewRepository _reviewRepository = reviewRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Guid> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = _mapper.Map<Review>(request);

            var _ = await _bookRepository.GetByIdAsync(request.BookId)
                ?? throw new NotFoundException($"Book with ID {request.BookId} was not found.");

            await _reviewRepository.AddAsync(review);
            await _reviewRepository.SaveChangesAsync();

            return review.Id;
        }
    }
}
