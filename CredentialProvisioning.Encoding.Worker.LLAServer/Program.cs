using CommandLine;
using Leosac.CredentialProvisioning.Core;
using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Encoding;
using Leosac.CredentialProvisioning.Server.Contracts.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;

namespace Leosac.CredentialProvisioning.Encoding.Worker.LLAServer
{
    class Program
    {
        public class Options
        {
            [Option('r', "Template Repository", Required = false, HelpText = "Folder where template files are located")]
            public string? TemplateRepository { get; set; }

            [Option('m', "managementApi", Required = false, HelpText = "Enable Worker Management API")]
            public bool? ManagementApi { get; set; }

            [Option('k', "apiKey", Required = false, Default = null, HelpText = "API key. Undefined means unsecure API calls.")]
            public string? APIKey { get; set; }

            [Option('s', "secret", Required = false, Default = null, HelpText = "Secret key. Undefined means unsecure API calls.")]
            public string? SecretKey { get; set; }
        }

        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                var actionJsonBuilder = JsonSubTypes.JsonSubtypesConverterBuilder
                    .Of(typeof(EncodingActionProperties), EncodingActionProperties.Discriminator);
                var actions = EncodingActionProperties.GetAllTypes();
                foreach (var actionType in actions)
                {
                    actionJsonBuilder.RegisterSubtype(actionType, actionType.Name);
                }
                options.SerializerSettings.Converters.Add(actionJsonBuilder.SerializeDiscriminatorProperty().Build());
            });

            var configOptions = builder.Configuration.GetSection("Options").Get<Options>();
            // Override options from config file by options from command lines
            var options = Parser.Default.ParseArguments(() => { return configOptions ?? new Options(); }, args);
            builder.Services.ConfigureOptions(options.Value);

            builder.Services.AddSignalR();

            if (!string.IsNullOrEmpty(options.Value.APIKey) && !string.IsNullOrEmpty(options.Value.SecretKey))
            {
                builder.Services.AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(o =>
                {
                    // We have to use the query string instead of headers because of limitations in Browser API...
                    // Hate that, as it is against best practice but there is no choice. We should also consider
                    // https://docs.microsoft.com/aspnet/core/signalr/security#access-token-logging
                    // when going into production.
                    o.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            // Filter for hubs only
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/hubs/reader")))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            var worker = new EncodingWorker();
            if (options.Value.ManagementApi.GetValueOrDefault(true))
            {
                app.MapPost("/auth", (string secret) =>
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

                app.MapPost("/template/{templateId}", (string templateId, EncodingFragmentTemplateContent template) =>
                {
                    worker.LoadTemplate(Guid.Parse(templateId), template);
                    return templateId;
                })
                .WithName("LoadTemplateWithId");

                app.MapGet("/templates", () =>
                {
                    return worker.GetTemplates();
                })
                .WithName("GetTemplates");

                app.MapGet("/template/{templateId}/check", (string templateId) =>
                {
                    return (worker.GetTemplate(Guid.Parse(templateId)) != null);
                })
                .WithName("CheckTemplate");

                app.MapGet("/template/{templateId}/fields", (string templateId) =>
                {

                })
                .WithName("GetTemplateFields");

                app.MapPost("/template/{templateId}/queue", (string templateId, CredentialBase credential) =>
                {
                    var itemId = worker.Queue.Add(Guid.Parse(templateId), credential);
                    return new ObjectIdResponse<Guid>() { Id = itemId };
                })
                .WithName("AddToQueue");

                app.MapGet("/template/{templateId}/queue/{itemId}", (string templateId, string itemId) =>
                {
                    var item = worker.Queue.Get(Guid.Parse(templateId));
                    if (item == null)
                        return Results.NotFound();

                    return Results.Ok(item.Credential);
                })
                .WithName("GetFromQueue");

                app.MapDelete("/template/{templateId}/queue/{itemId}", (string templateId, string itemId) =>
                {
                    worker.Queue.Remove(Guid.Parse(templateId));
                })
                .WithName("DeleteFromQueue");
            }

            app.MapHub<ReaderHub>("/reader");

            app.Run();
        }
    }
}