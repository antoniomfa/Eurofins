using DataAccess;
using DataAccess.Repo;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddPolicy("CustomPolicy", builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
}));

if (builder.Environment.IsProduction())
{
    Console.WriteLine("--> USING PRODUCTION DB...");
}
else
{
    Console.WriteLine("--> USING DEV DB...");
}

builder.Services.AddScoped<IRepo, Repo>();
builder.Services.AddSingleton(sp => sp.GetRequiredService<ILoggerFactory>().CreateLogger("DefaultLogger"));

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("MyTestDb")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CustomPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

