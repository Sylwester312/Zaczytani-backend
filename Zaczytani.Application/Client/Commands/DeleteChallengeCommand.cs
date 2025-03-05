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
            var challenge = await _challengeRepository.GetChallenge(request.ChallengeId, cancellationToken)
                ?? throw new NotFoundException("Challenge not found or you do not have access to it.");

            if (challenge.UserId == request.UserId)
            {
                await _challengeRepository.DeleteAsync(request.ChallengeId, cancellationToken);
            }
            else
            {
                var progressList = await _challengeRepository.GetChallengesWithProgressByUserId(request.UserId, cancellationToken);
                var progress = progressList.FirstOrDefault(p => p.ChallengeId == request.ChallengeId);

                if (progress != null)
                {
                    await _challengeRepository.DeleteProgressAsync(progress.Id, cancellationToken);
                }
                else
                {
                    throw new NotFoundException("You are not part of this challenge.");
                }
            }

            await _challengeRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
