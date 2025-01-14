using MediatR;
using Microsoft.AspNetCore.Identity;
using Zaczytani.Application.Filters;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Exceptions;

namespace Zaczytani.Application.Shared.Commands;

public class ChangePasswordCommand : IRequest, IUserIdAssignable
{
    public string CurrentPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;


    private Guid UserId { get; set; }
    public void SetUserId(Guid userId) => UserId = userId;

    private class ChangePasswordCommandHandler(UserManager<User> userManager) : IRequestHandler<ChangePasswordCommand>
    {
        private readonly UserManager<User> _userManager = userManager;
        public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString())
                 ?? throw new NotFoundException("User with given ID not found");

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                throw new BadRequestException("Failed to change password");
            }
        }
    }
}
