using Zaczytani.Domain.Entities;

namespace Zaczytani.Domain.Repositories;

public interface IPublishingHouseRepository
{
    IQueryable<PublishingHouse> GetAll();
    Task SaveChangesAsync();
}
