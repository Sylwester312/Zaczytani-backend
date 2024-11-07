using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Repositories;
using Zaczytani.Infrastructure.Persistance;
using Zaczytani.Infrastructure.Repositories;

namespace Zaczytani.Infrastructure.Extenstions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ZaczytaniDb");
        services.AddDbContext<BookDbContext>(options => options.UseSqlServer(connectionString).LogTo(Console.WriteLine, LogLevel.Information));

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<UserRole>()
            .AddEntityFrameworkStores<BookDbContext>();

        services.AddScoped<IBookRepository, BookRepository>();
    }
}
