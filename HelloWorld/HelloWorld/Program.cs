var builder = WebApplication.CreateBuilder(args);

// container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();



app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();


app.MapGet("/", () => "Hello World!");
app.MapGet("/selam", () => "Merhaba Zafer!");

app.Run();
