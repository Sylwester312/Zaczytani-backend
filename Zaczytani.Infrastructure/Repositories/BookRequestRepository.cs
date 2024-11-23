using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Repositories;
using Zaczytani.Infrastructure.Persistance;

namespace Zaczytani.Infrastructure.Repositories;

internal class BookRequestRepository(BookDbContext dbContext) : IBookRequestRepository
{
    private readonly BookDbContext _dbContext = dbContext;

    public async Task AddAsync(BookRequest entity) => await _dbContext.AddAsync(entity);

    public Task SaveChangesAsync() => _dbContext.SaveChangesAsync();
}
