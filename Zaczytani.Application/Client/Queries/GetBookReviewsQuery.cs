using AutoMapper;
using MediatR;
using Zaczytani.Application.Dtos;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Client.Queries;

public record GetBookReviewsQuery(Guid BookId) : IRequest<IEnumerable<BookReviewDto>>
{
    private class GetBookReviewsQueryHandler(IReviewRepository reviewRepository, IMapper mapper) : IRequestHandler<GetBookReviewsQuery, IEnumerable<BookReviewDto>>
    {
        private readonly IReviewRepository _reviewRepository = reviewRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<IEnumerable<BookReviewDto>> Handle(GetBookReviewsQuery request, CancellationToken cancellationToken)
        {
            var finalReviews = await _reviewRepository.GetFinalReviewsByBookId(request.BookId, cancellationToken);
            var reviewDtos = _mapper.Map<IEnumerable<BookReviewDto>>(finalReviews);

            foreach (var review in reviewDtos)
            {
                var reviews = await _reviewRepository.GetReviewsByBookIdAndUserId(request.BookId, review.User.Id, cancellationToken);
                review.NotesCount = reviews.Count();
            }

            return reviewDtos;
        }
    }
}
