using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RepositoryContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        mig => mig.MigrationsAssembly("WebApp"));
    
    options.EnableSensitiveDataLogging();
});

builder.Services.AddScoped<IBookService, BookManager>();
builder.Services.AddScoped<IBookRepository, BookRepository>();



var app = builder.Build();

app.MapGet("/context", (RepositoryContext context) =>
    Results.Ok(context.Books.ToList())
);

app.MapGet("/context/1", (RepositoryContext context) =>
{
    var book = context
        .Books
        .SingleOrDefault(b => b.Id == 1);

    if (book is null)
        return Results.NotFound();

    return Results.Ok(book);
});

app.MapGet("/repo", (IBookRepository bookRepo) => 
    Results.Ok(bookRepo.GetAll()));

app.MapGet("/repo/1", (IBookRepository bookRepo) =>
{ 
    var book = bookRepo.GetOne(b => b.Id == 1);
    if (book is null)
        return Results.NotFound();
    return Results.Ok(book);
});

app.MapGet("/services", (IBookService service) => 
    Results.Ok(service.GetAllBooks(b=> b.Price>100,true)));

app.MapGet("/services/1", (IBookService service) =>
    Results.Ok(service.GetBookById(1))
);



app.Run();
