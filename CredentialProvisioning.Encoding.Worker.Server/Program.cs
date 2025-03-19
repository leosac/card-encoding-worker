using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Encoding.Key;
using Leosac.CredentialProvisioning.Server.Contracts.Models;
using Leosac.CredentialProvisioning.Server.Shared;
using Leosac.ServerHelper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.WindowsServices;
using Microsoft.OpenApi.Models;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var isService = WindowsServiceHelpers.IsWindowsService();
            var builder = WebApplication.CreateBuilder(args);

            if (isService)
            {
                builder.Logging.AddEventLog(settings =>
                {
                    if (string.IsNullOrEmpty(settings.SourceName))
                        settings.SourceName = builder.Environment.ApplicationName;
                });
            }

            string? envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            string? osName = null;
            if (OperatingSystem.IsWindows())
                osName = "Windows";
            else if (OperatingSystem.IsLinux())
                osName = "Linux";
            if (!string.IsNullOrEmpty(envName) && !string.IsNullOrEmpty(osName))
                builder.Configuration.AddJsonFile($"appsettings.{envName}.{osName}.json", optional: true, reloadOnChange: true);

            var optionsSetup = new OptionsSetup(builder.Configuration);
            builder.Services.ConfigureOptions(optionsSetup);
            builder.Services.AddSignalR().AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.Converters.Add(new EnumWithFlagsJsonConverterFactory(JsonNamingPolicy.CamelCase));
                options.PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            });
            builder.Services.AddProblemDetails();

            builder.Host.UseWindowsService();
            builder.Host.UseSystemd();

            var options = new Options();
            optionsSetup.Configure(options);

            if (isService)
            {
                RunWorkerServer(builder, options);
            }
            else
            {
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

                var rootCommand = new RootCommand("Leosac Credential Provisioning Encoding Worker");
                rootCommand.AddGlobalOption(repositoryOption);
                rootCommand.AddGlobalOption(keyStoreOption);
                runCommand.AddOption(mgtapiOption);
                runCommand.AddOption(apikeyOption);
                runCommand.AddOption(integritykeyOption);
                runCommand.AddOption(readerTypeOption);
                runCommand.AddOption(contactlessReaderOption);
                runCommand.AddOption(samReaderOption);
                runCommand.SetHandler((o) =>
                {
                    RunWorkerServer(builder, options);
                }, new OptionsBinder(options, repositoryOption, keyStoreOption, mgtapiOption, apikeyOption, integritykeyOption, readerTypeOption, contactlessReaderOption, samReaderOption));
                rootCommand.AddCommand(runCommand);
                rootCommand.Invoke(args);
            }
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
                c.CustomSchemaIds((type) => PolymorphicTypeResolver.GetSubTypeDiscriminator(type, false));
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
                        Array.Empty<string>()
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
                options.SerializerOptions.Converters.Add(new EnumWithFlagsJsonConverterFactory(JsonNamingPolicy.CamelCase));
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
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
                keystore = Newtonsoft.Json.JsonConvert.DeserializeObject<KeyProvider>(File.ReadAllText(options.KeyStore));
            }
            keystore ??= new KeyProvider();
            builder.Services.AddSingleton(keystore);
            builder.Services.AddSingleton<EncodingWorker>();
            var integrity = new WorkerCredentialDataIntegrity();
            if (!string.IsNullOrEmpty(options.DataIntegrityKey))
            {
                integrity.LoadPublicKey(options.DataIntegrityKey);
            }
            builder.Services.AddSingleton(integrity);
            builder.Services.AddScoped<LocalReader>();
            builder.Services.AddSingleton<ReaderMediator>();

            var app = builder.Build();

            var worker = app.Services.GetRequiredService<EncodingWorker>();
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
                    var content = Newtonsoft.Json.JsonConvert.DeserializeObject<EncodingFragmentTemplateContent>(File.ReadAllText(file));
                    if (content != null)
                    {
                        worker.LoadTemplate(id, content, ((DateTimeOffset)File.GetLastWriteTimeUtc(file)).ToUnixTimeSeconds());
                    }
                }
            }

            if (options.EnableSwagger.GetValueOrDefault(true))
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            if (options.CompletionJWT != null)
            {
                worker.SetupCompletionTokenJWT(options.CompletionJWT);
            }

            var jwtService = new JwtService(options.JWT);
            if (options.ManagementApi.GetValueOrDefault(true))
            {
                app.MapPost("/auth", (AuthenticateWithAPIKeyRequest req) =>
                {
                    var key = options.JWT?.GetKey();
                    if (req.ApiKey == options.APIKey && key != null)
                    {
                        var claims = new List<Claim>(jwtService.CreateBaseClaims());
                        if (!string.IsNullOrEmpty(req.Application))
                            claims.Add(new Claim("application", req.Application));
                        if (req.Context != null)
                            claims.Add(new Claim("context", req.Context.ToString()!));

                        var token = jwtService.CreateToken([.. claims]);
                        return Results.Ok(new AuthenticationToken()
                        {
                            TokenValue = token,
                            Expiration = jwtService.GetExpirationDate(token)
                        });
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

                app.MapPost("/template/{templateId}", (string templateId, EncodingFragmentTemplateContent template, [FromQuery] long? revision) =>
                {
                    worker.LoadTemplate(templateId, template, revision);
                    return new ObjectIdResponse<string> { Id = templateId };
                })
                .WithName("LoadTemplateWithId").WithTags("template");

                app.MapGet("/templates", () =>
                {
                    return worker.GetTemplates();
                })
                .WithName("GetTemplates").WithTags("template");

                app.MapGet("/template/{templateId}/check", (string templateId, [FromQuery] long? revision) =>
                {
                    return (worker.GetTemplate(templateId, revision) != null);
                })
                .WithName("CheckTemplate").WithTags("template");

                app.MapGet("/template/{templateId}/fields", (string templateId) =>
                {

                })
                .WithName("GetTemplateFields").WithTags("template");
            }

            app.MapGet("/", () =>
            {
                return Results.Ok(new { Message = "Leosac Card Encoding Worker instance" });
            }).WithName("Ping");

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
                worker.Queue.Remove(itemId);
            })
            .WithName("DeleteFromQueue").WithTags("template", "queue");

            if (options.ReaderType == ReaderType.Remote)
            {
                // Virtual LLA readers through a WebSocket channel
                app.MapHub<RemoteReaderHub>("/reader");
            }
            else
            {
                app.MapPost("/template/{templateId}/queue/{itemId}/encode", async (string templateId, string itemId) =>
                {
                    var item = worker.Queue.Get(itemId);
                    if (item == null)
                        return Results.NotFound();

                    var mediator = app.Services.GetRequiredService<ReaderMediator>();
                    var localReader = app.Services.GetRequiredService<LocalReader>();
                    // Use local PC/SC resources
                    var processId = await mediator.EncodeFromQueue(item.TemplateId, itemId, localReader.Initialize);
                    if (string.IsNullOrEmpty(processId))
                        return Results.Problem("The encoding process cannot be started for this item.");

                    worker.Queue.Remove(itemId);
                    return Results.Ok(processId);
                })
                .WithName("Encode").WithTags("template", "queue", "encode");
            }

            app.Logger.LogInformation("App Setup completed, running...");

            app.Run();
        }
    }
}