using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Filters;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Enums;
using Zaczytani.Domain.Exceptions;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Client.Queries;

public record GetUserProfileQuery : IRequest<UserProfileDto>, IUserIdAssignable
{
    public Guid UserId { get; private set; }

    public void SetUserId(Guid userId) => UserId = userId;

    private class Handler(
        UserManager<User> userManager,
        IUserRepository userRepository,
        IBookShelfRepository bookShelfRepository,
        IFileStorageRepository fileStorageRepository,
        IMapper mapper
    ) : IRequestHandler<GetUserProfileQuery, UserProfileDto>
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IBookShelfRepository _bookShelfRepository = bookShelfRepository;
        private readonly IFileStorageRepository _fileStorageRepository = fileStorageRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<UserProfileDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString())
                ?? throw new NotFoundException("User not found");

            var userImage = _fileStorageRepository.GetFileUrl(user.Image);

            var favoriteGenres = await _userRepository.GetFavoriteGenresAsync(request.UserId, cancellationToken);

            var bookShelves = await _bookShelfRepository.GetAllByUserIdAsync(request.UserId, cancellationToken);

            var readBooksShelf = bookShelves.FirstOrDefault(bs => bs.Type == BookShelfType.Read);
            var currentlyReadingShelf = bookShelves.FirstOrDefault(bs => bs.Type == BookShelfType.Reading);

            var readBookDtos = readBooksShelf?.Books.Select(b =>
            {
                var book = _mapper.Map<BookDto>(b);
                book.ImageUrl = _fileStorageRepository.GetFileUrl(b.Image);
                return book;
            });

            var currentlyReading = currentlyReadingShelf?.Books.Select(b =>
            {
                var book = _mapper.Map<BookDto>(b);
                book.ImageUrl = _fileStorageRepository.GetFileUrl(b.Image);
                return book;
            });

            var profileDto = new UserProfileDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                ImageUrl = userImage,
                TotalBooksRead = readBooksShelf?.Books.Count ?? 0,
                FavoriteGenres = favoriteGenres.Select(g => g.ToString()).ToList(),
                ReadBooks = readBookDtos ?? [],
                CurrentlyReading = currentlyReading ?? [],
                Badges = new List<string> { "First Book Read", "100 Books Read" } // Na sztywno
            };

            return profileDto;
        }
    }

}