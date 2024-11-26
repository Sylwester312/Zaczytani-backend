﻿using Microsoft.EntityFrameworkCore;
using Zaczytani.Domain.Repositories;
using Zaczytani.Infrastructure.Persistance;

namespace Zaczytani.Infrastructure.Repositories;

internal class UserBookRepository(BookDbContext dbContext) : IUserBookRepository
{
    private readonly BookDbContext _dbContext = dbContext;

    public async Task<bool> HasUserDrawnBookTodayAsync(Guid userId)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        return await _dbContext.UserBooks
            .AnyAsync(ub => ub.UserId == userId && DateOnly.FromDateTime(ub.DrawnDate) == today);
    }
}
