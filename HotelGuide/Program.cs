using HotelGuide.Context;
using HotelGuide.Interfaces;
using HotelGuide.Repositories;
using HotelGuide.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<HotelContext>(options =>
          options.UseInMemoryDatabase("HotelGuideDb"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel API", Version = "v1" });
});

builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel API V1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();