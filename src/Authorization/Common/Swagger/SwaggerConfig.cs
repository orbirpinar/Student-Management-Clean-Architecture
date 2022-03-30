using System;
using System.Collections.Generic;
using Authorization.Common.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Authorization.Common.Swagger
{
    public static class SwaggerConfig
    {
        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo {Title = "Student Management Authorization API", Version = "v1"});
                var authorizationConfig = configuration.GetSection("Authorization").Get<AuthorizationSettings>();
                var securityScheme = new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.OAuth2,
                    Scheme = "Bearer",
                    Flows = new OpenApiOAuthFlows {AuthorizationCode = new OpenApiOAuthFlow {AuthorizationUrl = new Uri(authorizationConfig.AuthorizationUrl), TokenUrl = new Uri(authorizationConfig.TokenUrl)}}
                };
                config.AddSecurityDefinition("oauth2", securityScheme);
                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme {Reference = new OpenApiReference
                        {
                            Id = "oauth2", Type = ReferenceType.SecurityScheme
                        }}, new List<string>()
                    }
                });
            });
        }
    }
}