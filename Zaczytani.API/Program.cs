using AutoMapper;
using Zaczytani.Application.Dtos;
using Zaczytani.API.Extenstions;
using Zaczytani.Application.Extenstions;
using Zaczytani.Domain.Entities;
using Zaczytani.Infrastructure.Extenstions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(DtosProfile));

builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGroup("api/Identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

app.UseCors("frontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
