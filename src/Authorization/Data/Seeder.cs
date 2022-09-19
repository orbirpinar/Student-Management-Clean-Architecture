using System;
using System.Globalization;
using Authorization.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;
using Shared.Events;

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
            var studentClientApp = manager.FindByClientIdAsync("student-management-api").GetAwaiter().GetResult();
            var authorizeClientApp =
                manager.FindByClientIdAsync("student-management-authorize").GetAwaiter().GetResult();
            var studentSpa = manager.FindByClientIdAsync("student-management-spa").GetAwaiter().GetResult();
            if (studentClientApp == null)
            {
                manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "student-management-api",
                    ClientSecret = "499D56FA-B47B-5199-BA61-B298D431C318",
                    DisplayName = "Student Management Asp.Net Core Api",
                    RedirectUris = { new Uri("https://localhost:7135/swagger/oauth2-redirect.html") },
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

            if (studentSpa == null)
            {
                manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "student-management-spa",
                    DisplayName = "Student Management React SPA",
                    RedirectUris = { new Uri("http://localhost:3000/callback") },
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.Endpoints.Introspection,
                        OpenIddictConstants.Permissions.Endpoints.Logout,
                        OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                        OpenIddictConstants.Permissions.ResponseTypes.Code,
                        OpenIddictConstants.Scopes.OfflineAccess
                    }
                }).GetAwaiter().GetResult();
            }

            if (authorizeClientApp == null)
            {
                manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "student-management-authorize",
                    ClientSecret = "bf64a943-fc3d-4104-97bc-62b049b50f2c",
                    DisplayName = "Student Management Authorize Service",
                    RedirectUris = { new Uri("https://localhost:7059/swagger/oauth2-redirect.html") },
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
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var bus = scope.ServiceProvider.GetRequiredService<IBus>();

            var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                HasHeaderRecord = false,
                Comment = '#',
                AllowComments = true,
                Delimiter = ","
            };
            const string path = "./Data/teachers.csv";
            using var streamReader = File.OpenText(path);
            using var csvReader = new CsvReader(streamReader, csvConfig);
            while (csvReader.Read())
            {
                var fullName = csvReader.GetField(0);
                var email = TurkishCharacterToEnglish(csvReader.GetField(1));
                var firstName = fullName.Split(" ")[0];
                var lastName = fullName.Split(" ")[1];
                var username = TurkishCharacterToEnglish(fullName.Replace(" ", ".").ToLower());
                var user = new User { UserName = username, Email = email, Firstname = firstName, Lastname = lastName };
                const string password = "Test1234!";
                SeedUser(user, password, userManager, bus);
            }

        }

        private static void SeedUser(User user, string password, UserManager<User> userManager, IPublishEndpoint bus)
        {
            var result = userManager.CreateAsync(user, password).GetAwaiter().GetResult();
            if (result.Succeeded)
            {
                bus.Publish<UserRegistered>(new
                {
                    Id = new Guid(user.Id),
                    Username = user.UserName,
                    user.Email,
                    user.Firstname,
                    user.Lastname
                });
            }
        }
        public static string TurkishCharacterToEnglish(string text)
        {
            char[] turkishChars = { 'ı', 'ğ', 'İ', 'Ğ', 'ç', 'Ç', 'ş', 'Ş', 'ö', 'Ö', 'ü', 'Ü' };
            char[] englishChars = { 'i', 'g', 'I', 'G', 'c', 'C', 's', 'S', 'o', 'O', 'u', 'U' };

            // Match chars
            for (int i = 0; i < turkishChars.Length; i++)
                text = text.Replace(turkishChars[i], englishChars[i]);

            return text;
        }
    }
}
