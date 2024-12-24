using Microsoft.EntityFrameworkCore;
using ProductsAPI.Common.DBContext;
using ProductsAPI.DataAccess.Contract;
using ProductsAPI.DataAccess.Repository;

var builder = WebApplication.CreateBuilder(args);
#region for logging
builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();
#endregion
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductsDb")));
builder.Services.AddScoped<IProductDBManager, ProductDBManager>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
