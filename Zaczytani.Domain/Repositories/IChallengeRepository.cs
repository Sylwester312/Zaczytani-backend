using Zaczytani.Domain.Entities;

namespace Zaczytani.Domain.Repositories
{
    public interface IChallengeRepository
    {
        Task<IEnumerable<ChallengeProgress>> GetChallengesWithProgressByUserId(Guid userId, CancellationToken cancellationToken);
    }
}
