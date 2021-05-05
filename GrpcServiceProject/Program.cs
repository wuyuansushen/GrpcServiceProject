using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using GrpcServiceProject.Models;
using GrpcServiceProject.Data;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServiceProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var host = CreateHostBuilder(args);

            CreateDbIfNoExist(host);

            await host.RunAsync();
        }

        private static void CreateDbIfNoExist(IHost host)
        {
            using var create = host.Services.CreateScope();
                var serviceProv = create.ServiceProvider;
                var context = serviceProv.GetRequiredService<AuthContext>();
                context.Database.EnsureCreated();
                if(!context.Users.Any())
                {
                    DbInitialize.Initialize(context);
                }
                else { }
        }
        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHost CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(Kopt => {
                        Kopt.ListenAnyIP(6000, lopt =>
                        {
                            lopt.Protocols = HttpProtocols.Http2;
                            #region TLSsupportToggle
                            /*lopt.UseHttps(aopt=> {
                                aopt.ServerCertificate=X509Certificate2.CreateFromPemFile("crt/crt.pem", "crt/crt.key");
                                aopt.SslProtocols = SslProtocols.Tls13;
                            });*/
                            #endregion
                        });
                    });
                    webBuilder.UseStartup<Startup>();
                }).Build();
    }
}
