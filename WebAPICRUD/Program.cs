using WebAPICRUD.Models;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbcrudContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CadenaSQL") ?? throw new InvalidOperationException("Connection string 'DbcrudContext' not found.")));


builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}


app.UseCors("NuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();
