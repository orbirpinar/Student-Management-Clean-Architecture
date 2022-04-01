using System.Reflection;
using Authorization.Common.Swagger;
using Authorization.Data;
using Authorization.Services;
using Authorization.Settings;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/account/login";
    });

builder.Services.AddIdentityDbContext(builder.Configuration);
builder.Services.AddOpenIdConnect(builder.Configuration);
builder.Services.AddRabbitMq();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddSwagger(builder.Configuration);
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.OAuthClientId("student-management-authorize");
        options.OAuthClientSecret("bf64a943-fc3d-4104-97bc-62b049b50f2c");
        options.OAuthUsePkce();
        options.EnablePersistAuthorization();
    });
}

app.UseStaticFiles();
app.UseRouting();
app.UseCors(options => options.WithOrigins("https://localhost:7135").AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.SeedClients();
app.SeedUser();
app.Run();