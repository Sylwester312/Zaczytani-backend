﻿using Microsoft.EntityFrameworkCore;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Repositories;
using Zaczytani.Infrastructure.Persistance;

internal class UserDrawnBookRepository(BookDbContext dbContext) : IUserDrawnBookRepository
{
    private readonly BookDbContext _dbContext = dbContext;

    public async Task<UserDrawnBook?> GetDrawnBookByUserIdAndDateAsync(Guid userId, DateTime date, CancellationToken cancellationToken)
    {
        return await _dbContext.UserBooks
            .Include(udb => udb.Book)
            .FirstOrDefaultAsync(udb => udb.UserId == userId && udb.DrawnDate == date, cancellationToken);
    }

    public async Task AddAsync(UserDrawnBook userDrawnBook)
    {
        await _dbContext.UserBooks.AddAsync(userDrawnBook);
    }

    public Task SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
}
