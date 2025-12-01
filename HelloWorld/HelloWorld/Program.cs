var builder = WebApplication.CreateBuilder(args);

// container
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.MapGet("/", () => "Hello World!");
app.MapGet("/selam", () => "Merhaba Zafer!");

app.Run();
