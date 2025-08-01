using Microsoft.EntityFrameworkCore;
using System.IO; 
using TodoList.Data;

var builder = WebApplication.CreateBuilder(args);


var dbPath = Path.Combine(builder.Environment.ContentRootPath, "todo.db");
var connectionString = $"Data Source={dbPath}";

Console.WriteLine($"[DIAGNÓSTICO] Usando banco de dados em: {dbPath}");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();