using HotelGuide.Context;
using HotelGuide.Interfaces;
using HotelGuide.Repositories;
using HotelGuide.Services;
using HotelMicroService.Services;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Interfaces;
using RabbitMQ.Services;
using System.Text.Json;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
       services.AddDbContext<HotelContext>(options =>
       options.UseInMemoryDatabase(Configuration.GetConnectionString("DefaultConnection")));

        var rabbitMQConfig = Configuration.GetSection("RabbitMQ");
        var hostName = rabbitMQConfig["Host"];
        var port = int.Parse(rabbitMQConfig["Port"]);
        var username = rabbitMQConfig["Username"];
        var password = rabbitMQConfig["Password"];

        services.AddSingleton<IRabbitMQService>(sp =>
        {
            return new RabbitMQService(hostName, port, username, password);
        });

        services.AddScoped<IHotelService, HotelService>();
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<IHotelService, HotelService>();

        services.AddScoped<ReportListenerService>();


        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel API V1");
            c.RoutePrefix = string.Empty;
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
