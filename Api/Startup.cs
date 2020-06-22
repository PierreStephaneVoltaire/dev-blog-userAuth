using System;
using Domain.Adapter;
using Infrastructure.Adapters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Api
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
            var clientId = Configuration["AWS:UserPoolClientId"];
            var poolId = Configuration["AWS:UserPoolId"];
            var region = Configuration["AWS:Region"];
            var poolSecret = Configuration["AWS:UserPoolClientSecret"];
            Console.WriteLine(clientId);
            Console.WriteLine(region);

            services.AddSingleton<AuthAdapter, User_Infrstructure_Cognito>(x =>
                new User_Infrstructure_Cognito(clientId, poolId, poolSecret, region));

            services.AddControllers();
            services.AddHealthChecks();
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Auth Api", Version = "v1"});

                
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth Api");
                c.RoutePrefix = string.Empty;
            });
          //  app.UseHttpsRedirection();

            app.UseRouting();

          //  app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseEndpoints(endpoints => { endpoints.MapHealthChecks("/health"); });
        }
    }
}