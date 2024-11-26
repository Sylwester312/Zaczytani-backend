using Zaczytani.API.Extenstions;
using Zaczytani.Application.Extenstions;
using Zaczytani.Domain.Entities;
using Zaczytani.Infrastructure.Extenstions;
using Zaczytani.Infrastructure.Seeders;
using Zaczytani.API.Middlewares;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.Services.MigrateDatabaseAsync();

    await seeder.Seed();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGroup("api/Identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

app.UseCors("frontend");

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
