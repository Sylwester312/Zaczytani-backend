using Zaczytani.Domain.Entities;

namespace Zaczytani.Domain.Repositories
{
    public interface IChallengeRepository
    {
        Task AddAsync(Challenge entity, CancellationToken cancellationToken);
        Task AddAsync(ChallengeProgress entity, CancellationToken cancellationToken);
        IQueryable<Challenge> GetChallenges();
        Task<Challenge?> GetChallenge(Guid Id, CancellationToken cancellationToken);
        Task<IEnumerable<ChallengeProgress>> GetChallengesWithProgressByUserId(Guid userId, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
