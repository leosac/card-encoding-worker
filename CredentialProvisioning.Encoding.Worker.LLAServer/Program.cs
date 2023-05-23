using CommandLine;
using Leosac.CredentialProvisioning.Core;
using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Encoding;

namespace Leosac.CredentialProvisioning.Encoding.Worker.LLAServer
{
    class Program
    {
        public class Options
        {
            [Option('t', "Template", Required = false, HelpText = "Template file to load")]
            public string[] Templates { get; set; }

            [Option('m', "managementApi", Required = false, Default = false, HelpText = "Enable Worker Management API")]
            public bool ManagementApi { get; set; }

            [Option('s', "secret", Required = false, Default = false, HelpText = "Secret password. Undefined means unsecure API calls.")]
            public string? Secret { get; set; }
        }

        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            EncodingWorker? worker = null;

            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
            {
                worker = new EncodingWorker();

                if (o.ManagementApi)
                {
                    app.MapPost("/authenticate", (string secret) =>
                    {
                        
                    })
                    .WithName("Authenticate");

                    app.MapPost("/template", (EncodingFragmentTemplateContent template) =>
                    {
                        var templateId = Guid.NewGuid();
                        worker.LoadTemplate(templateId, template);
                        return templateId;
                    })
                    .WithName("LoadTemplate");

                    app.MapPost("/template/{templateId}/queue", (string templateId, CredentialBase credential) =>
                    {

                    })
                    .WithName("AddToQueue");

                    app.MapGet("/template/{templateId}/queue/{itemId}", (string templateId, string itemId) =>
                    {

                    })
                    .WithName("GetFromQueue");

                    app.MapDelete("/template/{templateId}/queue/{itemId}", (string templateId, string itemId) =>
                    {

                    })
                    .WithName("DeleteFromQueue");
                }
            });

            app.Run();
        }
    }
}