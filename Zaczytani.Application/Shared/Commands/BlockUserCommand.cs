using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Zaczytani.Application.Configuration;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Exceptions;
namespace Zaczytani.Application.Shared.Commands;

public record BlockUserCommand(Guid UserId) : IRequest
{
    private class BlockUserCommandHandler(UserManager<User> userManager, IOptions<UserManagementSettings> settings) : IRequestHandler<BlockUserCommand>
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly UserManagementSettings _settings = settings.Value;

        public async Task Handle(BlockUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString())
                 ?? throw new NotFoundException("User with given ID not found");

            user.LockoutEnd = DateTimeOffset.UtcNow.AddMinutes(_settings.BlockDurationInMinutes);

            await _userManager.UpdateAsync(user);
        }
    }
}
