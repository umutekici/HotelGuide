using Microsoft.EntityFrameworkCore;
using RabbitMQ.Interfaces;
using RabbitMQ.Services;
using ReportMicroService.Context;
using ReportMicroService.Interfaces;
using ReportMicroService.Repositories;
using ReportMicroService.Services;
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
        services.AddDbContext<ReportContext>(options =>
            options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                             new MySqlServerVersion(new Version(8, 0, 21))));

        var rabbitMQConfig = Configuration.GetSection("RabbitMQ");
        var hostName = rabbitMQConfig["Host"];
        var port = int.Parse(rabbitMQConfig["Port"]);
        var username = rabbitMQConfig["Username"];
        var password = rabbitMQConfig["Password"];

        services.AddSingleton<IRabbitMQService>(sp =>
        {
            return new RabbitMQService(hostName, port, username, password);
        });

        services.AddScoped<IReportRepository, ReportRepository>();
        services.AddScoped<IReportService, ReportService>();


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