using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace Authorization.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AuthorizationController : Controller
    {
        [HttpPost("~/connect/token")]
        public async Task<IActionResult> Exchange()
        {
            var request = HttpContext.GetOpenIddictServerRequest() ??
                          throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

            ClaimsPrincipal? claimsPrincipal;

            if (request.IsClientCredentialsGrantType())
            {

                var identity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

                // Subject (sub) is a required field, we use the client id as the subject identifier here.
                identity.AddClaim(OpenIddictConstants.Claims.Subject, request.ClientId ?? throw new InvalidOperationException());

                // Add some claim, don't forget to add destination otherwise it won't be added to the access token.

                claimsPrincipal = new ClaimsPrincipal(identity);
                claimsPrincipal.SetAudiences(request.ClientId);

                claimsPrincipal.SetScopes(request.GetScopes());
            }
            else if (request.IsRefreshTokenGrantType() || request.IsAuthorizationCodeGrantType())
            {
                claimsPrincipal = (await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)).Principal;
            }
            else
            {
                throw new InvalidOperationException("The specified grant type is not supported.");
            }

            // Returning a SignInResult will ask OpenIddict to issue the appropriate access/identity tokens.
            return SignIn(claimsPrincipal!, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
        
        [HttpGet("~/connect/authorize")]
        [HttpPost("~/connect/authorize")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Authorize()
        {
            var request = HttpContext.GetOpenIddictServerRequest() ??
                throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");
        
            // Retrieve the user principal stored in the authentication cookie.
            var result = await HttpContext.AuthenticateAsync();
        
            // If the user principal can't be extracted, redirect the user to the login page.
            if (!result.Succeeded)
            {
                return Challenge(
                    authenticationSchemes: CookieAuthenticationDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties
                    {
                        RedirectUri = Request.PathBase + Request.Path + QueryString.Create(
                            Request.HasFormContentType ? Request.Form.ToList() : Request.Query.ToList())
                    });
            }
        
            // Create a new claims principal
            var claims = new List<Claim>
            {
                // 'subject' claim which is required
                new Claim(OpenIddictConstants.Claims.Subject, result.Principal.Identity!.Name!),
            };
        
            var claimsIdentity = new ClaimsIdentity(claims, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            claimsPrincipal.SetAudiences(request.ClientId!);
            // Set requested scopes (this is not done automatically)
            claimsPrincipal.SetScopes(request.GetScopes());
            
        
            return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
    }
}