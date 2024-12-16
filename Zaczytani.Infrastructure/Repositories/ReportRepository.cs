using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Repositories;
using Zaczytani.Infrastructure.Persistance;

namespace Zaczytani.Infrastructure.Repositories;

internal class ReportRepository(BookDbContext dbContext) : IReportRepository
{
    private readonly BookDbContext _dbContext = dbContext;

    public async Task AddAsync(Report report, CancellationToken cancellationToken)
    {
        await _dbContext.Reports.AddAsync(report, cancellationToken);
    }

    public Task SaveChangesAsync() => _dbContext.SaveChangesAsync();
}
