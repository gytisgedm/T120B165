using System.Configuration;
using System.Reflection;
using api.Context;
using api.Features.Assets.Queries;
using Cqrs.Hosts;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FixedAssetsContext>(o =>
           o.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FixedAssets;Trusted_Connection=True"));

builder.Services.AddMediatR(typeof(Program).GetTypeInfo().Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
