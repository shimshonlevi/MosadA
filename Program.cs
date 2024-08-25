using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Mosad1.Data;
using Mosad1.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Mosad1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Mosad1Context") ?? throw new InvalidOperationException("Connection string 'Mosad1Context' not found.")));

builder.Services.AddScoped<DistanceCalculate>();

// Add services to the container.
builder.Services.AddScoped<missonServis>();
//IMissionService,
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



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
