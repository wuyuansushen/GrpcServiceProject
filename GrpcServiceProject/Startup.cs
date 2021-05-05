using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrpcServiceProject.Models;
using Microsoft.AspNetCore.Authentication;
using GrpcServiceProject.Middlewares;
using Microsoft.Extensions.Options;

namespace GrpcServiceProject
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddHealthChecks();
            services.AddTransient<NotFoundMiddleware>();
            services.AddDbContext<AuthContext>(opt => {
                opt.UseMySql(connectionString:@"server=localhost;user=root;password=1234567890;database=ef",serverVersion:new MySqlServerVersion(new Version(10,3,27){ })).EnableSensitiveDataLogging();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseStaticFiles();
            app.UseGrpcWeb(new GrpcWebOptions() { DefaultEnabled = true });

            app.UseNotFound();

            #region in-line middleware
            /*app.Use(async (context,next)=>
            {
                await next();
                if (context.Response.StatusCode == 404)
                    await context.Response.WriteAsync(@"( *^-^)Are you finding your mother?");
            }
            );*/
            #endregion

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>();
                endpoints.MapHealthChecks("/health");
                endpoints.MapGet("/", async (context) =>
                {
                    await context.Response.WriteAsync(@"Communication with Administrations to get gRPC Client for connection.");
                });
            });
        }
    }
}
