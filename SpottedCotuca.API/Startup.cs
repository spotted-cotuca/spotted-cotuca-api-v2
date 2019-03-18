using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpottedCotuca.Aplication.Repositories;
using SpottedCotuca.Aplication.Repositories.Datastore;
using SpottedCotuca.Application.Data.Clients;
using SpottedCotuca.Application.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace SpottedCotuca
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.ConfigureSwagger();

            services.ConfigureFacebookClient();
            services.ConfigureTwitterClient();

            services.AddTransient<SpotService>();
            services.AddTransient<SpotRepository, DatastoreSpotRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Spotted Cotuca API v1");
            });
        }
    }

    public static class StartupExtensions
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Spotted Cotuca API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public static void ConfigureFacebookClient(this IServiceCollection services)
        {
            services.AddSingleton<IFacebookClient>(serviceProvider => new FacebookClient(
                                                                                Environment.GetEnvironmentVariable("FACEBOOK_PAGE_ID"),
                                                                                Environment.GetEnvironmentVariable("FACEBOOK_ACCESS_TOKEN")));
        }

        public static void ConfigureTwitterClient(this IServiceCollection services)
        {
            var authCredentials = new TwitterAuthCredentials
            {
                ConsumerKey = Environment.GetEnvironmentVariable("TWITTER_CONSUMER_KEY"),
                ConsumerSecret = Environment.GetEnvironmentVariable("TWITTER_CONSUMER_SECRET"),
                AccessToken = Environment.GetEnvironmentVariable("TWITTER_ACCESS_TOKEN"),
                AccessTokenSecret = Environment.GetEnvironmentVariable("TWITTER_ACCESS_TOKEN_SECRET")
            };

            services.AddSingleton<ITwitterClient>(serviceProvider => new TwitterClient(authCredentials));
        }
    }
}
