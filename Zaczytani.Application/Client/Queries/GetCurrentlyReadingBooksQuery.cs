using AutoMapper;
using MediatR;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Filters;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Client.Queries;

public class GetCurrentlyReadingBooksQuery : IRequest<IEnumerable<CurrentlyReadingBookDto>>, IUserIdAssignable
{
    public Guid UserId { get; private set; }

    public void SetUserId(Guid userId) => UserId = userId;

    private class Handler(IReviewRepository reviewRepository, IFileStorageRepository fileStorageRepository, IMapper mapper) : IRequestHandler<GetCurrentlyReadingBooksQuery, IEnumerable<CurrentlyReadingBookDto>>
    {
        private readonly IReviewRepository _reviewRepository = reviewRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IFileStorageRepository _fileStorageRepository = fileStorageRepository;

        public async Task<IEnumerable<CurrentlyReadingBookDto>> Handle(GetCurrentlyReadingBooksQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _reviewRepository
        .GetCurrentlyReadingBooksAsync(request.UserId, cancellationToken);

            var latestReviews = reviews
                .GroupBy(r => r.BookId)
                .Select(group => group.OrderByDescending(r => r.CreatedDate).First())
                .ToList();

            var currentlyReadingBooks = _mapper.Map<IEnumerable<CurrentlyReadingBookDto>>(latestReviews);

            foreach (var book in currentlyReadingBooks)
            {
                book.ImageUrl = _fileStorageRepository.GetFileUrl(book.ImageUrl);
            }

            return currentlyReadingBooks;
        }
    }
}
