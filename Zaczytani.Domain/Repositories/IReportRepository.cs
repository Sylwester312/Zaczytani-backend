using Zaczytani.Domain.Entities;

namespace Zaczytani.Domain.Repositories;

public interface IReportRepository
{
    Task AddAsync(Report report, CancellationToken cancellationToken);
    Task SaveChangesAsync();
}
