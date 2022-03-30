using Authorization.Entities;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Authorization.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, 
            IOptions<OperationalStoreOptions> operationalStoreOptions) 
            : base(options, operationalStoreOptions)
        {
        }
    }
}