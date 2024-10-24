using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ReportMicroService.Context;
using ReportMicroService.Interfaces;
using ReportMicroService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ReportContext>(options =>
          options.UseInMemoryDatabase("ReportDb"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel API", Version = "v1" });
});

builder.Services.AddScoped<IReportService, ReportService>();

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
