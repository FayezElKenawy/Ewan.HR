using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ewan.Finance.InfraStructure.Loggers.Serilog;
using Serilog;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using SharedCoreLibrary.Application.Abstractions.CustomExceptions;
using SharedCoreLibrary.Application.CustomExceptions;
using SharedApiLibrary.Filters.Swagger;
using SharedApiLibrary.Extensions;
using SharedInfraStructureLibrary.Extensions;
using FluentValidation.AspNetCore;
using Ewan.Finance.API.Common.MiddleWares;
using Ewan.Finance.API.Common.Configurations.Swagger;
using Ewan.Finance.API.Common.Configurations;
using Ewan.Finance.API.Common.Validation;
using Ewan.HR.API.Common.Extensions;
using Ewan.HR.InfraStructure.UinitsOfWork;
using Ewan.HR.Core.Domain.Interfaces;
using SharedInfraStructureLibrary.Interceptors;
using Ewan.HR.InfraStructure.Contexts;
using SharedCoreLibrary.Application.Extensions;
using Ewan.Finance.Core.Application.Triggers;

Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

//Read Configuration from appSettings
var config = SerilogHelper.BuildConfiguration();

try
{
    Log.Information("Application Starting.");

    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog((contx, cfg) =>
        cfg.ReadFrom.Configuration(contx.Configuration)
              .WriteTo.Seq(config["Serilog:SeqServerUrl"], apiKey: config["Serilog:SeqServerToken"]
    ));

    IWebHostEnvironment environment = builder.Environment;
    var configuration = builder.Configuration
        .SetBasePath(environment.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
        .AddEnvironmentVariables()
        .Build();

    // Add services to the container.
    #region Configure Services
    builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = HandleModelValidation.Handle;
    }); 

    builder.Services.AddFluentValidationAutoValidation();


    builder.Services.AddApiVersioning(opt =>
    {
        opt.DefaultApiVersion = new ApiVersion(1, 0);
        opt.AssumeDefaultVersionWhenUnspecified = true;
        opt.ReportApiVersions = true;
    });

    builder.Services.AddVersionedApiExplorer(setup =>
    {
        setup.GroupNameFormat = "'v'VVV";
        setup.SubstituteApiVersionInUrl = true;
    });

    builder.Services.AddEndpointsApiExplorer();

    #region Swagger
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      // Register the Swagger generator, defining 1 or more Swagger documents
      builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

    builder.Services.AddSwaggerGen(c =>
    {
        //c.MapType<DateTime>(() => new OpenApiSchema { Format = "dd/MM/yyyy" });
        //c.MapType<DateTime?>(() => new OpenApiSchema { Format = "dd/MM/yyyy" });
        //c.SwaggerDoc("v1", new OpenApiInfo { Title = $"Seder eGate Ticketing APIs ({ environment.EnvironmentName } Env.)", Version = "1.0.0" });
        c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line

        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);

        //Enable Authentication
        //c.AddSecurityDefinition("JWT",
        //    new OpenApiSecurityScheme
        //    {
        //        BearerFormat = "JWT",
        //        In = ParameterLocation.Header,
        //        Description = "Please insert JWT with Bearer into field",
        //        Name = "Authorization",
        //        Type = SecuritySchemeType.ApiKey,
        //    });
        //c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        //        {
        //            new OpenApiSecurityScheme {
        //                Reference = new OpenApiReference {
        //                    Type = ReferenceType.SecurityScheme,
        //                    Id = "JWT"
        //                }
        //            },
        //            Array.Empty<string>()
        //            }
        //        });

        c.OperationFilter<AcceptLanguageHeaderOperationFilter>();
        c.OperationFilter<NestedPermissionHeaderOperationFilter>();
    });
    #endregion

    #region Mappers
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    #endregion

    #region Localization
    builder.Services.AddLocalization();

    // Configure supported cultures and localization options
    builder.Services.Configure<RequestLocalizationOptions>(options =>
    {
        var supportedCultures = new[]
        {
            new CultureInfo("en"),
            new CultureInfo("ar"){ DateTimeFormat = { Calendar = new GregorianCalendar() } }
        };
        supportedCultures.ToList().ForEach(c =>
        {
            c.DateTimeFormat.LongTimePattern = "HH:mm:ss";
            c.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
        });
        // State what the default culture for your application is. This will be used if no specific culture
        // can be determined for a given request.
        options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");

        // You must explicitly state which cultures your application supports.
        // These are the cultures the app supports for formatting numbers, dates, etc.
        options.SupportedCultures = supportedCultures;

        // These are the cultures the app supports for UI strings, i.e. we have localized resources for.
        options.SupportedUICultures = supportedCultures;
      
        // You can change which providers are configured to determine the culture for requests, or even add a custom
        // provider with your own logic. The providers will be asked in order to provide a culture for each request,
        // and the first to provide a non-null result that is in the configured supported cultures list will be used.
        // By default, the following built-in providers are configured:
        // - QueryStringRequestCultureProvider, sets culture via "culture" and "ui-culture" query string values, useful for testing
        // - CookieRequestCultureProvider, sets culture via "ASPNET_CULTURE" cookie
        // - AcceptLanguageHeaderRequestCultureProvider, sets culture via the "Accept-Language" request header
        //options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
        //{
        //  // My custom request culture logic
        //  return new ProviderCultureResult("en");
        //}));
    });
    #endregion

    #region Authentication
    //builder.Services
    //    .AddAuthentication(options =>
    //    {
    //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    //    })
    //    .AddJwtBearer(options =>
    //    {
    //        options.SaveToken = true;
    //        options.RequireHttpsMetadata = false;
    //        options.TokenValidationParameters = new TokenValidationParameters()
    //        {
    //            ValidateIssuer = false,
    //            ValidateAudience = false,
    //            ValidateIssuerSigningKey = true,
    //            //ValidAudience = configuration["JWT:ValidAudience"],
    //            //ValidIssuer = configuration["JWT:ValidIssuer"],
    //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    //        };
    //    });
    #endregion

    #region Authorization
    //builder.Services.AddAuthorization(
    //    options =>
    //    {
    //        options.AddPolicy(
    //            "FinanceUser",
    //            new AuthorizationPolicyBuilder()
    //                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
    //                .RequireAuthenticatedUser()
    //                .AddRequirements(new UserPolicyRequirement(configuration))
    //                .Build());

    //        options.AddPolicy(
    //            "AnonymousFinanceUser",
    //            new AuthorizationPolicyBuilder()
    //                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
    //                .RequireAuthenticatedUser()
    //                .Build());

    //    });
    #endregion

    #region DBContexts
    builder.Services.AddDbContext<HrContext>(
        (sp, optionsBuilder) =>
        {
            var updateAuditDataInterceptor = sp.GetRequiredService<UpdateAuditDataInterceptor>();

            //optionsBuilder.UseSqlServer(configuration.GetConnectionString("HRConnection"))
            //              .AddInterceptors(updateAuditDataInterceptor);
            optionsBuilder
                .UseTriggers(triggerOptions =>
                {
                    triggerOptions.AddTrigger<UserTransactionTrigger>(ServiceLifetime.Scoped);
                });
        });
    #endregion

    #region Policy
    //builder.Services.AddScoped<IAuthorizationHandler, UserPolicyAuthorizationHandler>();
    #endregion

    #region UnitOfWorks
    builder.Services.AddScoped<HRUnitOfWork>();

    builder.Services.AddSharedCoreLibraryUnitOfWork(
        x => x.GetRequiredService<HRUnitOfWork>());

    builder.Services.AddScoped<IHRUnitOfWork>(
        x => x.GetRequiredService<HRUnitOfWork>());
    #endregion

    #region services
    builder.Services.AddServices();
    #endregion

    #region Shared InfraStructure Services
    builder.Services.AddLocalizationService();
    builder.Services.AddCodeHelper();
    builder.Services.AddApiHelper();
    builder.Services.AddUpdateAuditDataInterceptor();
    #endregion

    builder.Services.AddHttpContextAccessor();
    #endregion
    
    var app = builder.Build();

    IApiVersionDescriptionProvider apiVersionDescriptionProvider =
        app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    // Configure the HTTP request pipeline.
    #region Configure
    app.UseRequestLocalization();

    if (app.Environment.IsDevelopment() 
        || app.Environment.EnvironmentName == "Debug"
        || app.Environment.EnvironmentName == "Local"
        || app.Environment.EnvironmentName == "Test")
    {
        app.UseSwagger();
        app.UseSwaggerUI(opt => {
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                opt.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });
        app.UseDeveloperExceptionPage();
    }

    app.UseExceptionHandler(builder =>
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
                else if (exceptionHandler.Error is BusinessException)
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
    });

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseSerilogRequestLogging(options =>
    {
    });

    app.UseRouting();

    #region CORS

    //CORS must be after UseRouting and before UseAuthorization & UseEndpoints
    app.UseCors(x => x.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .WithExposedHeaders("Content-Disposition"));
    #endregion

    #region Authentication and Autorization
    // UseAuthentication is placed just before UseAuthorization
    app.UseMiddleware<HeaderMiddleWare>();
    app.UseAuthentication();
    app.UseAuthorization();
    #endregion

    app.UseMiddleware<LogUserNameMiddleware>();

    app.MapControllers();

    app.Run();

    #endregion

}
catch (Exception ex)
{
    Log.Fatal(ex, "The Application failed to start.");
}
finally
{
    //Allows the logger to log any pending messages while the application closes down
    Log.CloseAndFlush();
}
