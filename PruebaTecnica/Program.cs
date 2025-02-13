using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Connection to the database
var connectionDb = builder.Configuration.GetConnectionString("ConnectionDbPerson");

//Register service to connect
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionDb));

//Configuring cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAllOrigins");

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
