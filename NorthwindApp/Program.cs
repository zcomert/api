using Microsoft.EntityFrameworkCore;
using NorthwindApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NorthwindDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();



app.MapGet("/", (NorthwindDbContext context) => 
    context.Products.ToList()
);

app.Run();
