using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;
using MediatR;
using EMONAPI.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.IO;
using EMONAPI.Domain.Datagram;
using EMONAPI.Persistance.Repositories.Datagram;
using EMONAPI.Persistance.Repositories;
using EMONAPI.Domain.Temprature;

namespace EMONAPI
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static void Register(HttpConfiguration config)
        {
            var corsAttr = new EnableCorsAttribute("http://localhost:4200", "*", "*");
            config.EnableCors();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                    });
            });
            services.AddControllers();
            services.AddMediatR(typeof(Startup));
            services.AddScoped<IDatagramRepository, DatagramRepository>();
            services.AddScoped<ITempratureRepository, TempratureRepository>();
            services.AddDbContext<MeterContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Add cors
            app.UseCors(MyAllowSpecificOrigins);

            app.UseSwagger();

            app.UseHttpsRedirection();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./v1/swagger.json", "My API V1");
                c.RoutePrefix = "swagger";
            });

            app.UseRouting();

            app.UseAuthorization();

            // Check if request is for the Angular project, redirect the request to the Angular project
            app.Use(async (context, next) =>
            {
                await next().ConfigureAwait(false);

                if (context.Response.StatusCode == 404 &&
                !Path.HasExtension(context.Request.Path.Value) &&
                !context.Request.Path.Value.StartsWith("/api") &&
                !context.Request.Path.Value.StartsWith("/auth") &&
                !context.Request.Path.Value.StartsWith("/swagger") &&
                !context.Request.Path.Value.StartsWith("/images"))
                {
                    context.Request.Path = "/index.html";
                    context.Response.StatusCode = 200;
                    await next().ConfigureAwait(false);
                }
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
