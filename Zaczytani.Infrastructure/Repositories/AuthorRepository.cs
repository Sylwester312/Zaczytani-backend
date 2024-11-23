using Microsoft.EntityFrameworkCore;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Repositories;
using Zaczytani.Infrastructure.Persistance;

namespace Zaczytani.Infrastructure.Repositories;

internal class AuthorRepository(BookDbContext dbContext) : IAuthorRepository
{
    private readonly BookDbContext _dbContext = dbContext;

    public async Task<Author?> GetByIdAsync(Guid authorId)
    {
        return await _dbContext.Authors.FirstOrDefaultAsync(a => a.Id == authorId);
    }

    public Task SaveChangesAsync() => _dbContext.SaveChangesAsync();
}
