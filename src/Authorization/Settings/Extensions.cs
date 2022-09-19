using System;
using System.Net.Http;
using Authorization.Common.Settings;
using Authorization.Consumer;
using Authorization.Data;
using Authorization.Entities;
using Authorization.Services;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;
using OpenIddict.Validation.SystemNetHttp;

namespace Authorization.Settings
{
    public static class Extensions
    {
        public static void AddIdentityDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("IdentityConnection"));
                options.UseOpenIddict();
            });
        }

        public static void AddOpenIdConnect(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIddictConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIddictConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIddictConstants.Claims.Role;
            });

            var openIddictConfig = configuration.GetSection("Authorization").Get<AuthorizationSettings>();
            services.AddOpenIddict()
                .AddCore(options => options.UseEntityFrameworkCore().UseDbContext<ApplicationDbContext>())
                .AddServer(options =>
                {
                    options.SetTokenEndpointUris("/connect/token");
                    options.SetAuthorizationEndpointUris("/connect/authorize");
                    options.SetUserinfoEndpointUris("/connect/userinfo");
                    options.SetIntrospectionEndpointUris("/connect/introspect");
                    options.AllowAuthorizationCodeFlow();
                    options.AllowClientCredentialsFlow();
                    options.AllowAuthorizationCodeFlow().RequireProofKeyForCodeExchange();
                    options.AllowRefreshTokenFlow();



                    options.RegisterScopes(OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Scopes.OfflineAccess,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                        OpenIddictConstants.Permissions.Scopes.Roles);

                    options.SetAccessTokenLifetime(TimeSpan.FromMinutes(5));
                    options.SetRefreshTokenLifetime(TimeSpan.FromDays(7));

                    options.AddEphemeralEncryptionKey()
                        .AddEphemeralSigningKey()
                        .DisableAccessTokenEncryption();

                    options.UseAspNetCore()
                        .EnableAuthorizationEndpointPassthrough()
                        .EnableLogoutEndpointPassthrough()
                        .EnableTokenEndpointPassthrough()
                        .EnableUserinfoEndpointPassthrough();
                })
                .AddValidation(options =>
                {
                    options.SetIssuer(openIddictConfig.Issuer);
                    options.AddAudiences(openIddictConfig.ClientId);
                    options.UseIntrospection()
                        .SetClientId(openIddictConfig.ClientId)
                        .SetClientSecret(openIddictConfig.ClientSecret);

                    options.UseAspNetCore();
                    options.UseSystemNetHttp();
                    options.AddAudiences("student-management-api");
                    options.AddAudiences("student-management-authorize");
                });

            services.AddHttpClient(typeof(OpenIddictValidationSystemNetHttpOptions).Assembly.GetName().Name)
                .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator });
            services.AddDefaultIdentity<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }

        public static void AddRabbitMq(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumer<UserUpdatedConsumer>();
                x.AddBus(_ => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host("rabbitmq://localhost", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ReceiveEndpoint("teacher-account-updated", e =>
                    {
                        e.ConfigureConsumer<UserUpdatedConsumer>(_);
                    });
                }));
            });
        }
    }
}
