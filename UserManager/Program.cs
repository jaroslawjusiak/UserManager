using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace UserManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args).Build();

            try
            {
                X509Certificate2 cert = new X509Certificate2(Path.Combine("/https/", "aspnetapp.pfx"), "crypticpassword");
                Console.WriteLine("cert PrivateKey: " + cert.PrivateKey);
                Console.WriteLine("cert IssuerName: " + cert.IssuerName);
                Console.WriteLine("cert SerialNumber: " + cert.SerialNumber);

                builder.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
