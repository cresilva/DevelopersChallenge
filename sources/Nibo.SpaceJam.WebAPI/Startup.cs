using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Nibo.SpaceJam.Repository;
using Swashbuckle.AspNetCore.Swagger;

namespace Nibo.SpaceJam.WebAPI
{
    /// <summary>
    /// Application startup configurations
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Dependency injection container
        /// </summary>
        public IContainer ApplicationContainer { get; private set; }

        /// <summary>
        /// Startup method
        /// </summary>
        /// <param name="configuration">Injected configuration</param>
        public Startup(IConfiguration configuration) { }

        /// <summary>
        /// Regiser services of application
        /// </summary>
        /// <param name="services">Services collection</param>
        /// <returns>Service provider with loaded services</returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var configurationBuilder = new ConfigurationBuilder()
           .SetBasePath(AppContext.BaseDirectory)
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddEnvironmentVariables();

            var config = configurationBuilder.Build();

            services.AddSingleton<IConfigurationRoot>((x) =>
            {
                return config;
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(new NotFoundExceptionFilter());
                options.Filters.Add(new ValidationExceptionFilter());
                options.Filters.Add(new ArgumentExceptionFilter());
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    policy => policy
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowAnyOrigin()
                                .AllowCredentials()
                                .Build());
            });

            //Use swagger for generate api documentation
            services.AddSwaggerGen(swaggerConfig =>
            {
                swaggerConfig.SwaggerDoc("v1", new Info { Title = "Nibo Space Jam API", Version = "v1" });

                swaggerConfig.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Nibo.SpaceJam.WebAPI.xml"));
                swaggerConfig.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Nibo.SpaceJam.Models.xml"));
                swaggerConfig.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Nibo.SpaceJam.Services.Abstractions.xml"));
                swaggerConfig.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Nibo.SpaceJam.Repository.Abstractions.xml"));
            });

            var builder = new ContainerBuilder();

            builder.RegisterModule(new RepositoryMappings());
            builder.RegisterModule(new ServiceMappings());

            builder.Populate(services);

            this.ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        /// <summary>
        /// Configure application
        /// </summary>
        /// <param name="app">Injected instance of application builder</param>
        /// <param name="env">Injected instance of application environment</param>
        /// <param name="cosmosWrapper">Injected instance of cosmos wrapper</param>
        /// <param name="config">Injected instance of configurations</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ICosmosWrapper cosmosWrapper, IConfigurationRoot config)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Enable swagger
            app.UseSwagger();
            app.UseSwaggerUI(swaggerConfig =>
            {
                swaggerConfig.SwaggerEndpoint("/swagger/v1/swagger.json", "Nibo Space Jam API");
                swaggerConfig.ShowRequestHeaders();
            });

            app.UseCors("CorsPolicy");
            app.UseMvc();

            //Initialize database
            cosmosWrapper.CreateDatabaseIfNotExistsAsync(new Microsoft.Azure.Documents.Database() { Id = config["AzureCosmosDB:DatabaseName"] }).Wait();
        }
    }
}