namespace Infrastructure.Identity
{
    public class AuthorizationConfig
    {
        public string Issuer { get; set; } = default!;
        public string ClientId { get; set; } = default!;
        public string ClientSecret { get; set; } = default!;

        public string AuthorizationUrl { get; set; } = default!;

        public string TokenUrl { get; set; } = default!;
    }
}