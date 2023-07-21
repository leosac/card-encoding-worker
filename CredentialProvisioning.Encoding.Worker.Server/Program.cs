using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Encoding.Key;
using Leosac.CredentialProvisioning.Server.Contracts.Models;
using Leosac.CredentialProvisioning.Server.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Leosac Card Encoding Worker API",
                        Version = "v1"
                    }
                );
                c.UseAllOfToExtendReferenceSchemas();
                c.UseAllOfForInheritance();
                c.UseOneOfForPolymorphism();
                c.SelectDiscriminatorNameUsing((baseType) => "typeName");
                c.SelectDiscriminatorValueUsing((subType) => subType.FullName);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.TypeInfoResolver = new PolymorphicTypeResolver();
            });

            var optionsSetup = new OptionsSetup(builder.Configuration, args);
            builder.Services.ConfigureOptions(optionsSetup);
            builder.Services.AddSignalR();

            var options = new Options();
            optionsSetup.Configure(options);
            if (!string.IsNullOrEmpty(options.APIKey) && !string.IsNullOrEmpty(options.JWT?.Key))
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

            var worker = new EncodingWorker();
            if (!string.IsNullOrEmpty(options.TemplateRepository))
            {
                if (!Directory.Exists(options.TemplateRepository))
                {
                    throw new Exception("The template repository folder doesn't exist.");
                }

                var files = Directory.GetFiles(options.TemplateRepository, "*.json");
                foreach (var file in files)
                {
                    var id = Path.GetFileNameWithoutExtension(file);
                    var content = JsonConvert.DeserializeObject<EncodingFragmentTemplateContent>(File.ReadAllText(file));
                    if (content != null)
                    {
                        worker.LoadTemplate(id, content);
                    }
                }
            }
            builder.Services.AddSingleton(worker);
            KeyProvider? keystore = null;
            if (!string.IsNullOrEmpty(options.KeyStore))
            {
                if (!File.Exists(options.KeyStore))
                {
                    throw new Exception("The Key Store file doesn't exist.");
                }
                keystore = JsonConvert.DeserializeObject<KeyProvider>(File.ReadAllText(options.KeyStore));
            }
            if (keystore == null)
            {
                keystore = new KeyProvider();
            }
            builder.Services.AddSingleton(keystore);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            var jwtService = new JwtService(options.JWT);
            if (options.ManagementApi.GetValueOrDefault(true))
            {
                app.MapPost("/auth", (AuthenticateWithAPIKeyRequest req) =>
                {
                    if (req.ApiKey == options.APIKey && !string.IsNullOrEmpty(options.JWT?.Key))
                    {
                        var claims = new List<Claim>(jwtService.CreateBaseClaims());
                        if (!string.IsNullOrEmpty(req.Application))
                            claims.Add(new Claim("application", req.Application));
                        if (req.Context != null)
                            claims.Add(new Claim("context", req.Context.ToString()));

                        return Results.Ok(jwtService.CreateToken(claims.ToArray()));
                    }

                    return Results.Unauthorized();
                })
                .WithName("Authenticate").WithTags("authentication");

                app.MapPost("/key", (CredentialKey key) =>
                {
                    keystore.Add(key);
                    return true;
                })
                .WithName("LoadKey").WithTags("key");

                app.MapPost("/keys", (CredentialKey[] keys) =>
                {
                    foreach (var key in keys)
                    {
                        keystore.Add(key);
                    }
                    return true;
                })
                .WithName("LoadKeys").WithTags("key");

                app.MapPost("/template", (EncodingFragmentTemplateContent template) =>
                {
                    var templateId = Guid.NewGuid().ToString();
                    worker.LoadTemplate(templateId, template);
                    return new ObjectIdResponse<string> { Id = templateId.ToString() };
                })
                .WithName("LoadTemplate").WithTags("template");

                app.MapPost("/template/{templateId}", (string templateId, EncodingFragmentTemplateContent template) =>
                {
                    worker.LoadTemplate(templateId, template);
                    return new ObjectIdResponse<string> { Id = templateId };
                })
                .WithName("LoadTemplateWithId").WithTags("template");

                app.MapGet("/templates", () =>
                {
                    return worker.GetTemplates();
                })
                .WithName("GetTemplates").WithTags("template");

                app.MapGet("/template/{templateId}/check", (string templateId) =>
                {
                    return (worker.GetTemplate(templateId) != null);
                })
                .WithName("CheckTemplate").WithTags("template");

                app.MapGet("/template/{templateId}/fields", (string templateId) =>
                {

                })
                .WithName("GetTemplateFields").WithTags("template");

                app.MapPost("/template/{templateId}/queue", (string templateId, CredentialBase credential) =>
                {
                    var itemId = worker.Queue.Add(templateId, credential);
                    return new ObjectIdResponse<string>() { Id = itemId };
                })
                .WithName("AddToQueue").WithTags("template", "queue");

                app.MapGet("/template/{templateId}/queue/{itemId}", (string templateId, string itemId) =>
                {
                    var item = worker.Queue.Get(itemId);
                    if (item == null)
                        return Results.NotFound();

                    return Results.Ok(item.Credential);
                })
                .WithName("GetFromQueue").WithTags("template", "queue");

                app.MapDelete("/template/{templateId}/queue/{itemId}", (string templateId, string itemId) =>
                {
                    worker.Queue.Remove(templateId);
                })
                .WithName("DeleteFromQueue").WithTags("template", "queue");
            }

            app.MapHub<ReaderHub>("/reader");

            app.Run();
        }
    }
}