using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SharedApiLibrary.Filters.Swagger;
using SharedCoreLibrary.Application.Abstractions.CustomExceptions;
using SharedCoreLibrary.Application.CustomExceptions;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Net;
using System.Reflection;

namespace SharedApiLibrary.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void ExceptionHandlerConfig(this IApplicationBuilder builder)
        {
            builder.Run(async context =>
            {
                // Get error details
                var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();
                if (exceptionHandler != null)
                {
                    if (exceptionHandler.Error is NotFoundException)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    }
                    else if (exceptionHandler.Error is ValidationException)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    }
                    context.Response.AddApplicationErrorHeader(exceptionHandler.Error.Message);
                    await context.Response.WriteAsync(exceptionHandler.Error.Message);
                }
            });
        }

        public static void SwaggerGenConfig(this SwaggerGenOptions options)
        {
            //c.MapType<DateTime>(() => new OpenApiSchema { Format = "dd/MM/yyyy" });
            //c.MapType<DateTime?>(() => new OpenApiSchema { Format = "dd/MM/yyyy" });
            //c.SwaggerDoc("v1", new OpenApiInfo { Title = $"Seder eGate Internal Portal APIs ({ environment.EnvironmentName } Env.)", Version = "1.0.0" });

            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetCallingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            //Enable Authentication
            options.AddSecurityDefinition("JWT",
                new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "JWT"
                        }
                    },
                    Array.Empty<string>()
                    }
                });

            options.OperationFilter<AcceptLanguageHeaderOperationFilter>();
            options.OperationFilter<NestedPermissionHeaderOperationFilter>();
        }

        public static void SwaggerUIConfig(this SwaggerUIOptions options, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        }
    }
}
