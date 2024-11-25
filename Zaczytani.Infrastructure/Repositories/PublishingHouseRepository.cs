﻿using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Repositories;
using Zaczytani.Infrastructure.Persistance;

namespace Zaczytani.Infrastructure.Repositories;

internal class PublishingHouseRepository(BookDbContext dbContext) : IPublishingHouseRepository
{
    private readonly BookDbContext _dbContext = dbContext;

    public IQueryable<PublishingHouse> GetAll() => _dbContext.PublishingHouses;

    public Task SaveChangesAsync() => _dbContext.SaveChangesAsync();
}
