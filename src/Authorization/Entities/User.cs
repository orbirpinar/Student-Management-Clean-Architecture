using Microsoft.AspNetCore.Identity;

namespace Authorization.Entities
{
    public class User: IdentityUser
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
    }
}