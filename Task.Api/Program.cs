using Microsoft.EntityFrameworkCore;
using Task.Api.Data;

var builder = WebApplication.CreateBuilder(args);
//Postgres-Connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionPostgres") ?? string.Empty));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();