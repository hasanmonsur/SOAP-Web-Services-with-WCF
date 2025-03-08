using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using CoreWCF.Channels;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace WcfService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Kestrel to listen on both HTTP and HTTPS
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(8800); // HTTP
                options.ListenAnyIP(8801, listenOptions => // HTTPS
                {
                    listenOptions.UseHttps(); // Configure HTTPS if needed
                });
            });


            // Add CoreWCF services
            builder.Services.AddServiceModelServices();
            builder.Services.AddServiceModelMetadata();
            builder.Services.AddSingleton<IServiceBehavior, ServiceMetadataBehavior>();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable WSDL endpoint
            var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
            serviceMetadataBehavior.HttpGetEnabled = true;

            // Add the WCF service endpoint
            app.UseServiceModel(builder =>
            {
                builder.AddService<Service>();
                builder.AddServiceEndpoint<Service, IService>(
                    new BasicHttpBinding(), // Use CoreWCF.Channels.BasicHttpBinding
                    "/Service"
                );
            });

            Console.WriteLine("Service is running at http://localhost:5000/Service");
            Console.WriteLine("WSDL available at http://localhost:5000/Service?wsdl");
            Console.WriteLine("Press Ctrl+C to stop the service...");

            await app.RunAsync();
        }
    }
   
}