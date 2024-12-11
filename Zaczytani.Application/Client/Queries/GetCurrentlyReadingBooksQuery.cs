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

    private class Handler(IReviewRepository reviewRepository, IMapper mapper) : IRequestHandler<GetCurrentlyReadingBooksQuery, IEnumerable<CurrentlyReadingBookDto>>
    {
        private readonly IReviewRepository _reviewRepository = reviewRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<CurrentlyReadingBookDto>> Handle(GetCurrentlyReadingBooksQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _reviewRepository.GetCurrentlyReadingBooksAsync(request.UserId, cancellationToken);
            return _mapper.Map<IEnumerable<CurrentlyReadingBookDto>>(reviews);
        }
    }
}
