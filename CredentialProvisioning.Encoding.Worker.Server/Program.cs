using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Encoding.Key;
using Leosac.CredentialProvisioning.Encoding.LLA;
using Leosac.CredentialProvisioning.Server.Contracts.Models;
using Leosac.CredentialProvisioning.Server.Shared;
using Leosac.ServerHelper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting.WindowsServices;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
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
                // Simple command-line parsing
                var boundOptions = options;
                for (int i = 0; i < args.Length; i++)
                {
                    var a = args[i];
                    switch (a)
                    {
                        case "--template-repository":
                            if (i + 1 < args.Length) boundOptions.TemplateRepository = args[++i];
                            break;
                        case "--keystore":
                            if (i + 1 < args.Length) boundOptions.KeyStore = args[++i];
                            break;
                        case "--management-api":
                            if (i + 1 < args.Length && bool.TryParse(args[i + 1], out var m)) { boundOptions.ManagementApi = m; ++i; }
                            else { boundOptions.ManagementApi = true; }
                            break;
                        case "--api-key":
                            if (i + 1 < args.Length) boundOptions.APIKey = args[++i];
                            break;
                        case "--integrity-key":
                            if (i + 1 < args.Length) boundOptions.DataIntegrityKey = args[++i];
                            break;
                        case "--reader-type":
                            if (i + 1 < args.Length && Enum.TryParse<ReaderType>(args[i + 1], true, out var rt)) { boundOptions.ReaderType = rt; ++i; }
                            break;
                        case "--reader-contactless":
                            if (i + 1 < args.Length) boundOptions.ContactlessReader = args[++i];
                            break;
                        case "--reader-sam":
                            if (i + 1 < args.Length) boundOptions.SAMReader = args[++i];
                            break;
                        case "--pkcs11-library":
                            if (i + 1 < args.Length) boundOptions.PKCS11Library = args[++i];
                            break;
                        case "--pkcs11-password":
                            if (i + 1 < args.Length) boundOptions.PKCS11Password = args[++i];
                            break;
                        default:
                            // ignore unknown
                            break;
                    }
                }

                RunWorkerServer(builder, boundOptions);
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
                c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", document)] = []
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

            bool requireAuth = false;
            if (!string.IsNullOrEmpty(options.APIKey) && !string.IsNullOrEmpty(options.JWT?.Key))
            {
                requireAuth = true;
                builder.Services.AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = options.JWT.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(options.JWT.GetKey())
                    };

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

                builder.Services.AddAuthorizationBuilder()
                    .AddDefaultPolicy("global", policy => {
                        policy.RequireAuthenticatedUser();
                        policy.RequireAssertion(context => !context.User.HasClaim(c => c.Type == "context"));
                    })
                    .AddPolicy("queue", policy => {
                        policy.RequireAuthenticatedUser();
                        policy.RequireAssertion(context =>
                        {
                            if (!context.User.HasClaim(c => c.Type == "context"))
                                return true;

                            var claimCtx = context.User.Claims.Where(c => c.Type == "context").FirstOrDefault();
                            if (context.Resource is HttpContext hctx)
                            {
                                object? itemId = null;
                                if (hctx.Request.RouteValues.TryGetValue("itemId", out itemId) && itemId != null)
                                {
                                    return claimCtx != null && claimCtx.Value == itemId.ToString();
                                }
                            }
                            else if (context.Resource is HubInvocationContext hictx)
                            {
                                return claimCtx != null && hictx.HubMethodArguments.Count >= 2 && claimCtx.Value == hictx.HubMethodArguments[1]?.ToString();
                            }

                            return false;
                        });
                    });
            }
            else
            {
                builder.Services.AddAuthorizationBuilder()
                    .AddDefaultPolicy("global", policy => {
                        policy.RequireAssertion(context => true);
                    })
                    .AddPolicy("queue", policy => {
                        policy.RequireAssertion(context => true);
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

            if (requireAuth)
            {
                app.UseAuthentication();
            }
            app.UseAuthorization();

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

            CredentialKeyExt.PKCS11Library = options.PKCS11Library;
            CredentialKeyExt.PKCS11Password = options.PKCS11Password;

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
                app.MapPost("/key", (CredentialKey key) =>
                {
                    keystore.Add(key);
                    return true;
                })
                .RequireAuthorization()
                .WithName("LoadKey").WithTags("key");

                app.MapPost("/keys", (CredentialKey[] keys) =>
                {
                    foreach (var key in keys)
                    {
                        keystore.Add(key);
                    }
                    return true;
                })
                .RequireAuthorization()
                .WithName("LoadKeys").WithTags("key");

                app.MapPost("/template", (EncodingFragmentTemplateContent template) =>
                {
                    var templateId = Guid.NewGuid().ToString();
                    worker.LoadTemplate(templateId, template);
                    return new ObjectIdResponse<string> { Id = templateId.ToString() };
                })
                .RequireAuthorization()
                .WithName("LoadTemplate").WithTags("template");

                app.MapPost("/template/{templateId}", (string templateId, EncodingFragmentTemplateContent template, [FromQuery] long? revision) =>
                {
                    worker.LoadTemplate(templateId, template, revision);
                    return new ObjectIdResponse<string> { Id = templateId };
                })
                .RequireAuthorization()
                .WithName("LoadTemplateWithId").WithTags("template");

                app.MapGet("/templates", () =>
                {
                    return worker.GetTemplates();
                })
                .RequireAuthorization()
                .WithName("GetTemplates").WithTags("template");

                app.MapGet("/template/{templateId}/check", (string templateId, [FromQuery] long? revision) =>
                {
                    return (worker.GetTemplate(templateId, revision) != null);
                })
                .RequireAuthorization()
                .WithName("CheckTemplate").WithTags("template");

                app.MapGet("/template/{templateId}/fields", (string templateId) =>
                {

                })
                .RequireAuthorization()
                .WithName("GetTemplateFields").WithTags("template");
            }

            app.MapGet("/", () =>
            {
                return Results.Ok(new { Message = "Leosac Card Encoding Worker instance" });
            })
            .AllowAnonymous()
            .WithName("Ping");

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
            .AllowAnonymous()
            .WithName("Authenticate").WithTags("authentication");

            app.MapPost("/template/{templateId}/queue", (string templateId, WorkerCredentialBase credential) =>
            {
                if (integrity.IsEnabled() && !integrity.Verify(credential))
                {
                    return Results.BadRequest("Invalid credential signature.");
                }
                var itemId = worker.Queue.Add(templateId, credential);
                return Results.Ok(new ObjectIdResponse<string>() { Id = itemId });
            })
            .RequireAuthorization()
            .WithName("AddToQueue").WithTags("template", "queue");

            app.MapGet("/template/{templateId}/queue/{itemId}", (string templateId, string itemId) =>
            {
                var item = worker.Queue.Get(itemId);
                if (item == null)
                    return Results.NotFound();

                return Results.Ok(item.Credential);
            })
            .RequireAuthorization("queue")
            .WithName("GetFromQueue").WithTags("template", "queue");

            app.MapDelete("/template/{templateId}/queue/{itemId}", (string templateId, string itemId) =>
            {
                worker.Queue.Remove(itemId);
            })
            .RequireAuthorization("queue")
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
                .RequireAuthorization("queue")
                .WithName("Encode").WithTags("template", "queue", "encode");
            }

            app.Logger.LogInformation("App Setup completed, running...");

            app.Run();
        }
    }
}