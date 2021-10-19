using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;

namespace APISWAGGER
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Swagger Demo",
                    Version = "v1",
                    Description = "TBD",

                    Contact = new OpenApiContact { Name = "John Doe", Email = "john@xyzmail.com", Url = new Uri("https://www.example.com") },

                    License = new OpenApiLicense { Name = "License Terms", Url = new Uri("https://www.example.com") }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
             
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {

                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Demo");

                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    internal class License : OpenApiLicense
    {
        new public string Name { get; set; }
        new public string Url { get; set; }
    }

    internal class Contact : OpenApiContact
    {
        new public string Name { get; set; }
        new public string Email { get; set; }
        new public string Url { get; set; }
    }
}