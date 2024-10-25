using Microsoft.EntityFrameworkCore;
using BibliotecaApi.Data;
using BibliotecaApi.Models;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("Biblioteca") ?? "Data Source=BibliotecaDataBase.db";
builder.Services.AddSqlite<AppDbContext>(connectionString);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.MapGet("/bibliotecas", async (AppDbContext db) => await db.Bibliotecas.ToListAsync());

app.MapPost("/biblioteca", async (AppDbContext db, Biblioteca biblioteca) =>
{
    await db.Bibliotecas.AddAsync(biblioteca);
    await db.SaveChangesAsync();
    return Results.Created($"/biblioteca/{biblioteca.Id}", biblioteca);
});

app.MapDelete("/biblioteca/{id:int}", async (int id, AppDbContext db) =>
{
    var biblioteca = await db.Bibliotecas.FindAsync(id);
    if (biblioteca == null)
    {
        return Results.NotFound();
    }
    db.Bibliotecas.Remove(biblioteca);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapPut("/biblioteca/{id}", async (AppDbContext db, Biblioteca updateBiblioteca, int id) =>
{
    var biblioteca = await db.Bibliotecas.FindAsync(id);
    if (biblioteca is null) return Results.NotFound();
    
    biblioteca.Nome = updateBiblioteca.Nome;
    biblioteca.Inicio_funcionamento = updateBiblioteca.Inicio_funcionamento;
    biblioteca.Fim_funcionamento = updateBiblioteca.Fim_funcionamento;
    biblioteca.inauguracao = updateBiblioteca.inauguracao;
    biblioteca.contato = updateBiblioteca.contato;

    await db.SaveChangesAsync();
    return Results.NoContent();
});


app.Run();
