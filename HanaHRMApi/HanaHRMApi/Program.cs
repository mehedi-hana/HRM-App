using HanaHRMApi.Models;
using HanaHRMApi.ServiceExtensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register DbContext
builder.Services.AddDbContext<HanaHrmContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HanaHrmDatabase")));

builder.Services.AddApplicationServices();


builder.Services.AddCors(options =>
{
    options.AddPolicy("HanaHRMPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Swagger Service
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

app.UseCors("HanaHRMPolicy");

app.MapControllers();

app.Run();
