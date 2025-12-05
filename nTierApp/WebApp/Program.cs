
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<RepositoryContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        mig => mig.MigrationsAssembly("WebApp"));
    
    options.EnableSensitiveDataLogging();
});

builder.Services.AddScoped<IBookService, BookManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();

//builder.Services.AddScoped<IBookRepository, BookRepository>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();



var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}/{id?}"
);

app.MapGet("/books/context", (RepositoryContext context) =>
    Results.Ok(context.Books.ToList())
);

app.MapGet("/books/context/1", (RepositoryContext context) =>
{
    var book = context
        .Books
        .SingleOrDefault(b => b.Id == 1);

    if (book is null)
        return Results.NotFound();

    return Results.Ok(book);
});

app.MapGet("/books/repo", (IRepositoryManager manager) => 
    Results.Ok(manager.BookRepository.GetAll()));

app.MapGet("books/repo/1", (IRepositoryManager manager) =>
{ 
    var book = manager.BookRepository.GetOne(b => b.Id == 1);
    if (book is null)
        return Results.NotFound();
    return Results.Ok(book);
});

app.MapGet("books/services", (IBookService service) => 
    Results.Ok(service.GetAllBooks(b=> b.Price>100,true)));

app.MapGet("books/services/1", (IBookService service) =>
    Results.Ok(service.GetBookById(1))
);

// categories
app.MapGet("categories/context", (RepositoryContext context) =>
{
    var categories = context.Categories.ToList();
    return Results.Ok(categories);
});

// categories
app.MapGet("categories/repo", (IRepositoryManager manager) =>
{
    var categories = manager.CategoryRepository.GetAll().ToList();
    return Results.Ok(categories);
});


// 
app.MapGet("/", (IRepositoryManager manager) =>
{
    var baseUrl = "https://localhost:7169";
    return Results.Ok(new
    {
        Books = $"{baseUrl}/api/books",
        Categories = $"{baseUrl}/api/categories"
    });
});


app.Run();
