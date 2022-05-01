using Application.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Validation.AspNetCore;
using OpenIddict.Validation.SystemNetHttp;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraStructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgresConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            });

            var openIddictConfig = configuration.GetSection("Authorization").Get<AuthorizationConfig>();
            services.AddOpenIddict()
                .AddValidation(options =>
                {
                    options.SetIssuer(openIddictConfig.Issuer);
                    options.AddAudiences(openIddictConfig.ClientId);
                    options.UseIntrospection()
                        .SetClientId(openIddictConfig.ClientId)
                        .SetClientSecret(openIddictConfig.ClientSecret);

                    options.UseSystemNetHttp();
                    options.UseAspNetCore();
                });

            services.AddHttpClient(typeof(OpenIddictValidationSystemNetHttpOptions).Assembly.GetName().Name)
                .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler {ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator});
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<AppDbContext>());
            services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
            services.AddAuthorization();


            return services;
        }
    }
}