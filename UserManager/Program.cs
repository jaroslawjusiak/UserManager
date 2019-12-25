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
        private static ILoggingBuilder _logging;
        private static readonly ILogger _logger;

        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args).Build();

            X509Certificate2 cert = new X509Certificate2(Path.Combine(Directory.GetCurrentDirectory(), "cert.pfx"), "pass123");
            Console.WriteLine("cert private key: " + cert.PrivateKey);

            try
            {
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
