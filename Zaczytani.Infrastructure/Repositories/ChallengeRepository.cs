using Microsoft.EntityFrameworkCore;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Repositories;
using Zaczytani.Infrastructure.Persistance;

namespace Zaczytani.Infrastructure.Repositories;

internal class ChallengeRepository(BookDbContext dbContext) : IChallengeRepository
{
    private readonly BookDbContext _dbContext = dbContext;

    public async Task<IEnumerable<ChallengeProgress>> GetChallengesWithProgressByUserId(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.ChallengeProgresses
            .Include(cp => cp.Challenge)
            .Where(cp => cp.UserId == userId)
            .ToListAsync(cancellationToken);
    }
}
