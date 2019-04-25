using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Swashbuckle.AspNetCore.Swagger;
using TVmaze.Scraper.Application.Interfaces.ApiClients;
using TVmaze.Scraper.Application.Interfaces.Repositories;
using TVmaze.Scraper.Application.Interfaces.Services;
using TVmaze.Scraper.Application.Services;
using TVmaze.Scraper.Domain.Models;
using TVmaze.Scraper.Infrastructure.Api;
using TVmaze.Scraper.Infrastructure.ApiClients;
using TVmaze.Scraper.Persistence.Entities;
using TVmaze.Scraper.Persistence.Repositories;

namespace TVmaze.Scraper.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "TVmaze Scraper API", Version = "v1" });
            });
            services.AddAutoMapper();

            services.AddTransient<IShowService, ShowService>();
            services.AddTransient<IShowApiClient, ShowApiClient>();
            services.AddTransient<IShowRepository, ShowRepository>();

            services
                .AddRefitClient<IShowApi>();

            var databaseSettings = new DatabaseSettings();
            Configuration.Bind("DatabaseSettings", databaseSettings);

            services.AddDbContext<TVmazeContext>(options => options.UseCosmos(Configuration.GetConnectionString("CosmosDB"),
                databaseSettings.PrimaryKey, databaseSettings.DatabaseId));
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TVmaze Scraper API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
