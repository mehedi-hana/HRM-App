using HanaHRMApi.Models;
using HanaHRMApi.ServiceExtensions;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddOpenApi();

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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options.Title = "Hana HRM API";
    });

}

app.UseHttpsRedirection();

app.UseCors("HanaHRMPolicy");

app.MapControllers();

app.MapGet("", () => Results.Redirect("/scalar"));

app.Run();
