
using DevExtremePB_Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//builder.Services.AddDbContext<DBPUNTO_VENTAContext>(options =>
//                    options.UseSqlServer("Data Source=DESKTOP-264IV9T\\MCHACONSERVER; Initial Catalog=DBPUNTO_VENTA; Integrated Security = True"));

builder.Services.AddDbContext<dbventasContext>(options =>
                    options.UseSqlServer("Data Source=127.0.0.1;Initial Catalog=dbventas;User ID=sa;Password=1234"));

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

