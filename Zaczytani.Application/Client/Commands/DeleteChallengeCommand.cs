using MediatR;
using Zaczytani.Application.Filters;
using Zaczytani.Domain.Exceptions;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Client.Commands;

public record DeleteChallengeCommand(Guid ChallengeId) : IRequest, IUserIdAssignable
{
    public Guid UserId { get; private set; }
    public void SetUserId(Guid userId) => UserId = userId;

    private class Handler(IChallengeRepository challengeRepository) : IRequestHandler<DeleteChallengeCommand>
    {
        private readonly IChallengeRepository _challengeRepository = challengeRepository;

        public async Task Handle(DeleteChallengeCommand request, CancellationToken cancellationToken)
        {
            var challenge = await _challengeRepository.GetChallenge(request.ChallengeId, cancellationToken);

            if (challenge == null || challenge.UserId != request.UserId)
            {
                throw new NotFoundException("Challenge not found or you do not have access to delete it.");
            }

            await _challengeRepository.DeleteAsync(request.ChallengeId, cancellationToken);
            await _challengeRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
