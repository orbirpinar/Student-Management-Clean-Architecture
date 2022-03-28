using System;
using Authorization.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;

namespace Authorization.Data
{
    internal static class Seeder
    {
        internal static void SeedClients(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();

            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
            var existingClientApp = manager.FindByClientIdAsync("student-management-api").GetAwaiter().GetResult();
            if (existingClientApp == null)
            {
                manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "student-management-api",
                    ClientSecret = "499D56FA-B47B-5199-BA61-B298D431C318",
                    DisplayName = "Student Management Asp.Net Core Api",
                    RedirectUris = {new Uri("https://localhost:7135/swagger/oauth2-redirect.html")},
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.Endpoints.Introspection,
                        OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                        OpenIddictConstants.Permissions.ResponseTypes.Code,
                        OpenIddictConstants.Scopes.OfflineAccess
                    }
                }).GetAwaiter().GetResult();
            }
        }

        internal static void SeedUser(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var user = new User {UserName = "test_user"};

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var existingUser = userManager.FindByNameAsync(user.UserName).GetAwaiter().GetResult();
            if (existingUser != null) return;
            var hash = userManager.PasswordHasher.HashPassword(user, "Test1234!");
            user.PasswordHash = hash;
            userManager.CreateAsync(user).GetAwaiter().GetResult();
        }
    }
}