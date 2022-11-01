using Core.ApplicationServices.ShortUrl.CommandHandler;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using EventStore.ClientAPI;
using Framework.ApplicationServices.Common;
using Framework.Domain.Data;
using Infrastructure.Data.EventSourcing;
using Infrastructure.Data.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.ApplicationServices.WebFramework.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ShortUrlContext>(options =>
            {
                //var x = configuration.GetConnectionString("SqlServer");
                options
                    .UseSqlServer(configuration.GetConnectionString("SqlServer"));
                //.UseSqlServer(configuration.GetConnectionString("SqlServer"));
                //Tips
                //Automatic client evaluation is no longer supported. This event is no longer generated.
                //This line is no longer needed.
                //.ConfigureWarnings(warning => warning.Throw(RelationalEventId.QueryClientEvaluationWarning));
            });
        }
        public static void AddEventStore(this IServiceCollection services, IConfiguration configuration, SiteSettings siteSetting)
        {
            var esConnection = EventStoreConnection.Create(configuration.GetConnectionString("EventStore"), ConnectionSettings.Create().KeepReconnecting(), siteSetting.ApplicationName);
            var store = new EventSource(esConnection);
            services.AddSingleton(esConnection);
            services.AddSingleton<IEventSource>(store);
        }

        public static void AddMinimalMvc(this IServiceCollection services)
        {
            //https://github.com/aspnet/AspNetCore/blob/0303c9e90b5b48b309a78c2ec9911db1812e6bf3/src/Mvc/Mvc/src/MvcServiceCollectionExtensions.cs
            services.AddControllers(options =>
            {
                // options.Filters.Add(new AuthorizeFilter()); //Apply AuthorizeFilter as global filter to all actions

                //Like [ValidateAntiforgeryToken] attribute but dose not validatie for GET and HEAD http method
                //You can ingore validate by using [IgnoreAntiforgeryToken] attribute
                //Use this filter when use cookie 
                //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

                //options.UseYeKeModelBinder();
            }).AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.Converters.Add(new StringEnumConverter());
                option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //option.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                //option.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });
            services.AddSwaggerGenNewtonsoftSupport();

            #region Old way (We don't need this from ASP.NET Core 3.0 onwards)
            ////https://github.com/aspnet/Mvc/blob/release/2.2/src/Microsoft.AspNetCore.Mvc/MvcServiceCollectionExtensions.cs
            //services.AddMvcCore(options =>
            //{
            //    options.Filters.Add(new AuthorizeFilter());

            //    //Like [ValidateAntiforgeryToken] attribute but dose not validatie for GET and HEAD http method
            //    //You can ingore validate by using [IgnoreAntiforgeryToken] attribute
            //    //Use this filter when use cookie 
            //    //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

            //    //options.UseYeKeModelBinder();
            //})
            //.AddApiExplorer()
            //.AddAuthorization()
            //.AddFormatterMappings()
            //.AddDataAnnotations()
            //.AddJsonOptions(option =>
            //{
            //    //option.JsonSerializerOptions
            //})
            //.AddNewtonsoftJson(/*option =>
            //{
            //    option.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            //    option.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            //}*/)

            ////Microsoft.AspNetCore.Mvc.Formatters.Json
            ////.AddJsonFormatters(/*options =>
            ////{
            ////    options.Formatting = Newtonsoft.Json.Formatting.Indented;
            ////    options.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            ////}*/)

            //.AddCors()
            //.SetCompatibilityVersion(CompatibilityVersion.Latest); //.SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            #endregion
        }

        public static void AddElmahCore(this IServiceCollection services, IConfiguration configuration, SiteSettings siteSetting)
        {
            services.AddElmah<SqlErrorLog>(options =>
            {
                options.Path = siteSetting.ElmahPath;
                //options.ConnectionString = "Server=148.251.102.200,3131; Database=Eliein_test; User Id=Eliein; Password=Ipaco@1401";
                options.ConnectionString = configuration.GetConnectionString("Elmah");
                //options.CheckPermissionAction = httpContext => httpContext.User.Identity.IsAuthenticated;
            });
        }

        //public static void AddJwtAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
        //{
        //    services.AddAuthentication(options =>
        //    {
        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //    }).AddJwtBearer(options =>
        //    {
        //        var secretKey = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
        //        var encryptionKey = Encoding.UTF8.GetBytes(jwtSettings.EncryptKey);

        //        var validationParameters = new TokenValidationParameters
        //        {
        //            ClockSkew = TimeSpan.Zero, // default: 5 min
        //            RequireSignedTokens = true,

        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(secretKey),

        //            RequireExpirationTime = true,
        //            ValidateLifetime = true,

        //            ValidateAudience = false, //default : false
        //            ValidAudience = jwtSettings.Audience,

        //            ValidateIssuer = false, //default : false
        //            ValidIssuer = jwtSettings.Issuer,

        //            TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey)
        //        };

        //        options.RequireHttpsMetadata = false;
        //        options.SaveToken = true;
        //        options.TokenValidationParameters = validationParameters;
        //        options.Events = new JwtBearerEvents
        //        {
        //            OnAuthenticationFailed = context =>
        //            {
        //                //var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
        //                //logger.LogError("Authentication failed.", context.Exception);

        //                if (context.Exception != null)
        //                    throw new AppException(ApiResultStatusCode.UnAuthorized, "Authentication failed.", HttpStatusCode.Unauthorized, context.Exception, null);

        //                return Task.CompletedTask;
        //            },
        //            OnTokenValidated = async context =>
        //            {
        //                var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<ApplicationUser>>();
        //                var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();

        //                var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
        //                if (claimsIdentity.Claims?.Any() != true)
        //                    context.Fail("This token has no claims.");

        //                var securityStamp = claimsIdentity.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);
        //                if (!securityStamp.HasValue())
        //                    context.Fail("This token has no security stamp");

        //                //Find user and token from database and perform your custom validation
        //                var userId = claimsIdentity.GetUserId<int>();
        //                var user = await userRepository.GetByIdAsync(context.HttpContext.RequestAborted, userId);

        //                //if (user.SecurityStamp != Guid.Parse(securityStamp))
        //                //    context.Fail("Token security stamp is not valid.");

        //                var validatedUser = await signInManager.ValidateSecurityStampAsync(context.Principal);
        //                if (validatedUser == null)
        //                    context.Fail("Token security stamp is not valid.");

        //                if (!user.IsActive)
        //                    context.Fail("User is not active.");

        //                await userRepository.UpdateLastLoginDateAsync(user, context.HttpContext.RequestAborted);
        //            },
        //            OnChallenge = context =>
        //            {
        //                //var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
        //                //logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);

        //                if (context.AuthenticateFailure != null)
        //                    throw new AppException(ApiResultStatusCode.UnAuthorized, "Authenticate failure.", HttpStatusCode.Unauthorized, context.AuthenticateFailure, null);
        //                throw new AppException(ApiResultStatusCode.UnAuthorized, "You are unauthorized to access this resource.", HttpStatusCode.Unauthorized);

        //                //return Task.CompletedTask;
        //            }
        //        };
        //    });
        //}

        public static void AddCustomApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                //url segment => {version}
                options.AssumeDefaultVersionWhenUnspecified = true; //default => false;
                options.DefaultApiVersion = new ApiVersion(1, 0); //v1.0 == v1
                options.ReportApiVersions = true;

                //ApiVersion.TryParse("1.0", out var version10);
                //ApiVersion.TryParse("1", out var version1);
                //var a = version10 == version1;

                //options.ApiVersionReader = new QueryStringApiVersionReader("api-version");
                // api/posts?api-version=1

                //options.ApiVersionReader = new UrlSegmentApiVersionReader();
                // api/v1/posts

                //options.ApiVersionReader = new HeaderApiVersionReader(new[] { "Api-Version" });
                // header => Api-Version : 1

                //options.ApiVersionReader = new MediaTypeApiVersionReader()

                //options.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader("api-version"), new UrlSegmentApiVersionReader())
                // combine of [querystring] & [urlsegment]
            });
        }

        public static void AddEventHandlers(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, ShortUrlUnitOfWork>();

            services.AddScoped<CreateHandler>();
            services.AddScoped<ReviewUrlHandler>();
            services.AddScoped<SetShortUrlHandler>();
            services.AddScoped<SetUrlHandler>();
        }
    }
}
