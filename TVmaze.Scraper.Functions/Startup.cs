using AutoMapper;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using TVmaze.Scraper.Application.Interfaces.ApiClients;
using TVmaze.Scraper.Application.Interfaces.Repositories;
using TVmaze.Scraper.Application.Interfaces.Services;
using TVmaze.Scraper.Application.Services;
using TVmaze.Scraper.Domain.Models;
using TVmaze.Scraper.Infrastructure.Api;
using TVmaze.Scraper.Infrastructure.ApiClients;
using TVmaze.Scraper.Persistence.Entities;
using TVmaze.Scraper.Persistence.Repositories;

namespace TVmaze.Scraper.Functions
{
    public static class Startup
    {
        public static IServiceProvider ConfigureServices(ExecutionContext context)
        {
            var services = new ServiceCollection();
            var databaseSettings = new DatabaseSettings();

            // Configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            config.Bind("DatabaseSettings", databaseSettings);

            services.AddTransient<IShowService, ShowService>();
            services.AddTransient<IShowApiClient, ShowApiClient>();
            services.AddTransient<IShowRepository, ShowRepository>();

            services
                .AddRefitClient<IShowApi>()
                .ConfigureHttpClient(client => client.BaseAddress = new Uri(config["ApiBaseUrl"]));

            services.AddAutoMapper();
            services.AddDbContext<TVmazeContext>(options => options.UseCosmos(config.GetConnectionString("CosmosDB"),
                databaseSettings.PrimaryKey, databaseSettings.DatabaseId));

            return services.BuildServiceProvider();
        }
    }
}
