using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Zaczytani.Application.Http;

namespace Zaczytani.Application.Extenstions;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var applicationAssembly = typeof(ServiceCollectionExtension).Assembly;
        services.AddAutoMapper(applicationAssembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AssignUserIdBehavior<,>));

        services.AddValidatorsFromAssembly(applicationAssembly)
            .AddFluentValidationAutoValidation();

        services.Configure<HttpClientConfig>(configuration.GetSection("BookHttpClient"));
        services.AddHttpClient<BookHttpClient>((sp, client) =>
        {
            var config = sp.GetRequiredService<IOptions<HttpClientConfig>>().Value;
            client.BaseAddress = new Uri(config.BaseUrl);
        });

    }
}