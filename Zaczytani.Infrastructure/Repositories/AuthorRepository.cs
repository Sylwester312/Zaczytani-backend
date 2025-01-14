using Microsoft.EntityFrameworkCore;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Repositories;
using Zaczytani.Infrastructure.Persistance;

namespace Zaczytani.Infrastructure.Repositories;

internal class AuthorRepository(BookDbContext dbContext) : IAuthorRepository
{
    private readonly BookDbContext _dbContext = dbContext;

    public IQueryable<Author> GetAll() => _dbContext.Authors;

    public async Task<Author?> GetByIdAsync(Guid authorId)
    {
        return await _dbContext.Authors.FirstOrDefaultAsync(a => a.Id == authorId);
    }

    public async Task<Author?> GetByNameAsync(string name)
    {
        return await _dbContext.Authors.Where(a => a.Name == name).FirstOrDefaultAsync();
    }

    public Task SaveChangesAsync() => _dbContext.SaveChangesAsync();
}
