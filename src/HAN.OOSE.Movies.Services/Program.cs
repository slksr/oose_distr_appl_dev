
using HAN.OOSE.Movies.BusinessLayer;
using HAN.OOSE.Movies.BusinessLayer.Contracts;
using HAN.OOSE.Movies.DataLayer;
using HAN.OOSE.Movies.DataLayer.Contracts;
using HAN.OOSE.Movies.DataLayer.Migrations;
using HAN.OOSE.Movies.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace HAN.OOSE.Movies.Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
               .WriteTo.Console()
               .CreateBootstrapLogger();

            Log.Logger.Information("Starting up the HAN OOSE Movies API");

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("MovieConnection") ?? throw new InvalidOperationException("Connection string 'MovieConnection' not found.");

            builder.Services.AddDbContext<MovieContext>(options =>
                options.UseSqlite(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<IMovieRepository, MovieRepository>();

            builder.Services.AddHealthChecks();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Host.UseSerilog((ctx, lc) =>
            {
                lc.MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext}{NewLine}{Message:lj}{Exception}{NewLine}",
                    theme: AnsiConsoleTheme.Code)
                .Enrich.FromLogContext();
            });

            var app = builder.Build();

            // Seed database with testdata
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                SeedData.Initialize(services);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapHealthChecks("/health");

            app.UseAuthorization();

            app.MapControllers();

            app.UseSerilogRequestLogging();
            app.Run();
        }
    }
}
