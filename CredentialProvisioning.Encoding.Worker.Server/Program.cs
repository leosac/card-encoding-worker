using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Encoding.Key;
using Leosac.CredentialProvisioning.Server.Contracts.Models;
using Leosac.CredentialProvisioning.Server.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.CommandLine;
using System.Security.Claims;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var optionsSetup = new OptionsSetup(builder.Configuration);
            builder.Services.ConfigureOptions(optionsSetup);
            builder.Services.AddSignalR();

            var options = new Options();
            optionsSetup.Configure(options);

            var repositoryOption = new Option<string?>(
                name: "--template-repository",
                description: "Folder where template files are located.",
                getDefaultValue: () => options.TemplateRepository
            );

            var keyStoreOption = new Option<string?>(
                name: "--keystore",
                description: "File key store to load.",
                getDefaultValue: () => options.KeyStore
            );

            var runCommand = new Command(
                name: "--run",
                description: "Run the worker server"
            );

            var svcCommand = new Command(
                name: "--service",
                description: "Run as a service"
            );

            var mgtapiOption = new Option<bool?>(
                name: "--management-api",
                description: "Enable Worker Management API.",
                getDefaultValue: () => options.ManagementApi
            );

            var apikeyOption = new Option<string?>(
                name: "--api-key",
                description: "API key. Undefined means unsecure API calls.",
                getDefaultValue: () => options.APIKey
            );

            var integritykeyOption = new Option<string?>(
                name: "--integrity-key",
                description: "The public key for data integrity verification. Undefined means data integrity is not checked.",
                getDefaultValue: () => options.DataIntegrityKey
            );

            var readerTypeOption = new Option<ReaderType>(
                name: "--reader-type",
                description: "Type of reader technology. Remote = use readers from a remote client over WebSocket / Local = use local PC/SC reader resources.",
                getDefaultValue: () => options.ReaderType
            );

            var contactlessReaderOption = new Option<string>(
                name: "--reader-contactless",
                description: "The contactless reader alias/name.",
                getDefaultValue: () => options.ContactlessReader
            );

            var samReaderOption = new Option<string>(
                name: "--reader-sam",
                description: "The SAM reader alias/name.",
                getDefaultValue: () => options.SAMReader
            );

            var svcOption = new Option<bool>(
                name: "--service",
                description: "Run as a service",
                getDefaultValue: () => false
            );

            var rootCommand = new RootCommand("Leosac Credential Provisioning Encoding Worker");
            rootCommand.AddGlobalOption(repositoryOption);
            rootCommand.AddGlobalOption(keyStoreOption);
            runCommand.AddOption(mgtapiOption);
            runCommand.AddOption(apikeyOption);
            runCommand.AddOption(integritykeyOption);
            runCommand.AddOption(readerTypeOption);
            runCommand.AddOption(contactlessReaderOption);
            runCommand.AddOption(samReaderOption);
            runCommand.AddOption(svcOption);
            runCommand.SetHandler((o) =>
            {
                if (o.RunAsService)
                {
                    if (OperatingSystem.IsWindows())
                    {
                        builder.Host.UseWindowsService();
                    }
                    else
                    {
                        builder.Host.UseSystemd();
                    }
                }

                RunWorkerServer(builder, options);
            }, new OptionsBinder(options, repositoryOption, keyStoreOption, mgtapiOption, apikeyOption, integritykeyOption, readerTypeOption, contactlessReaderOption, samReaderOption, svcOption));
            rootCommand.AddCommand(runCommand);
            rootCommand.Invoke(args);
        }

        private static void RunWorkerServer(WebApplicationBuilder builder, Options options)
        {
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
                c.SelectDiscriminatorNameUsing((baseType) => "$type");
                c.SelectDiscriminatorValueUsing((subType) => PolymorphicTypeResolver.GetSubTypeDiscriminator(subType));
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

                var xmlFile = $"{typeof(EncodingAction).Assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });

            builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.TypeInfoResolver = new PolymorphicTypeResolver();
            });

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
            builder.Services.AddSingleton<EncodingWorker>();
            var integrity = new WorkerCredentialDataIntegrity();
            if (!string.IsNullOrEmpty(options.DataIntegrityKey))
            {
                integrity.LoadPublicKey(options.DataIntegrityKey);
            }
            builder.Services.AddSingleton(integrity);

            var app = builder.Build();

            var worker = app.Services.GetService<EncodingWorker>();
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
                        worker.LoadTemplate(id, content, ((DateTimeOffset)File.GetLastWriteTimeUtc(file)).ToUnixTimeSeconds());
                    }
                }
            }

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

                app.MapGet("/template/{templateId}/check", (string templateId, long? revision) =>
                {
                    return (worker.GetTemplate(templateId, revision) != null);
                })
                .WithName("CheckTemplate").WithTags("template");

                app.MapGet("/template/{templateId}/fields", (string templateId) =>
                {

                })
                .WithName("GetTemplateFields").WithTags("template");
            }

            app.MapPost("/template/{templateId}/queue", (string templateId, WorkerCredentialBase credential) =>
            {
                if (integrity.IsEnabled() && !integrity.Verify(credential))
                {
                    return Results.BadRequest("Invalid credential signature.");
                }
                var itemId = worker.Queue.Add(templateId, credential);
                return Results.Ok(new ObjectIdResponse<string>() { Id = itemId });
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

            if (options.ReaderType == ReaderType.Remote)
            {
                app.MapHub<ReaderHub>("/reader");
            }
            else
            {
                // TODO: implements "/encode/" endpoints and use local PC/SC resources
                throw new NotImplementedException();
            }

            app.Run();
        }
    }
}