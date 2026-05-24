using IxCloud.DataAccess.Context;
using IxCloud.DataAccess.Repositories;
using IxCloud.DataAccess.Seeders;
using IxClouds.Domain.Interfaces.Repositories;
using IxClouds.Domain.Interfaces.Service;
using IxClouds.Domain.Service;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddCors(options => { options.AddPolicy("AngularApp", policy => policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod()); });
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
    await DatabaseSeeder.SeedAsync(db);
}
if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }
app.UseHttpsRedirection();
app.UseCors("AngularApp");
app.UseAuthorization();
app.MapControllers();
app.Run();
