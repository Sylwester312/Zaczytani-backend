using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Repositories;
using Zaczytani.Domain.Enums;
using Zaczytani.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Zaczytani.Infrastructure.Repositories;

internal class BookRequestRepository(BookDbContext dbContext) : IBookRequestRepository
{
    private readonly BookDbContext _dbContext = dbContext;

    public async Task AddAsync(BookRequest entity) => await _dbContext.AddAsync(entity);

    public IQueryable<BookRequest> GetAllPending() => _dbContext.BookRequests
        .Where(b => b.Status == BookRequestStatus.Pending)
        .OrderBy(b => b.CreatedDate);

    public IQueryable<BookRequest> GetByUserId(Guid userId) => _dbContext.BookRequests
        .Where(b => b.UserId == userId)
        .OrderBy(b => b.CreatedDate);

    public async Task<BookRequest?> GetByIdAsync(Guid id) => await _dbContext.BookRequests.FirstOrDefaultAsync(b => b.Id == id);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) => _dbContext.SaveChangesAsync(cancellationToken);
}
