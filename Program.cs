using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using SzabóFlórabackend.Models;
using SzabóFlórabackend.Services.ILibrary;
using SzabóFlórabackend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration["UID"] = "FKB3F4FEA09CE43C";

// Add services to the container.

builder.Services.AddDbContext<LibrarydbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MySQL");
    options.UseMySQL(connectionString);
});

builder.Services.AddScoped<IAuthors, AuthorsService>();
builder.Services.AddScoped<IBook, BookService>();
builder.Services.AddScoped<ICategory, CategoryService>();

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
