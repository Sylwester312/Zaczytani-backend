using AutoMapper;
using MediatR;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Filters;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Enums;
using Zaczytani.Domain.Repositories;

public record GetUserProfileQuery : IRequest<UserProfileDto>, IUserIdAssignable
{
    public Guid UserId { get; private set; }

    public void SetUserId(Guid userId) => UserId = userId;

    private class Handler(
        IUserRepository userRepository,
        IBookShelfRepository bookShelfRepository,
        IChallengeRepository challengeRepository,
        IMapper mapper
    ) : IRequestHandler<GetUserProfileQuery, UserProfileDto>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IBookShelfRepository _bookShelfRepository = bookShelfRepository;
        private readonly IChallengeRepository _challengeRepository = challengeRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<UserProfileDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var favoriteGenres = await _userRepository.GetFavoriteGenresAsync(request.UserId, cancellationToken);

            var bookShelves = await _bookShelfRepository.GetAllByUserIdAsync(request.UserId, cancellationToken);

            var readBooksShelf = bookShelves.FirstOrDefault(bs => bs.Type == BookShelfType.Read);
            var currentlyReadingShelf = bookShelves.FirstOrDefault(bs => bs.Type == BookShelfType.Reading);

            var challenges = await _challengeRepository.GetChallengesWithProgressByUserId(request.UserId, cancellationToken);

            var profileDto = new UserProfileDto
            {
                FirstName = "User",
                LastName = "Name",
                FavoriteGenres = favoriteGenres.Select(g => g.ToString()).ToList(),
                ReadBooks = _mapper.Map<IEnumerable<BookDto>>(readBooksShelf?.Books ?? Enumerable.Empty<Book>()),
                CurrentlyReading = _mapper.Map<IEnumerable<BookDto>>(currentlyReadingShelf?.Books ?? Enumerable.Empty<Book>()),
                Challenges = _mapper.Map<IEnumerable<ChallengeDto>>(challenges)
            };

            return profileDto;
        }
    }
}
