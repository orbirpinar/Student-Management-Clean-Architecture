using System;

namespace Domain.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;

        public Teacher Teacher { get; set; } = default!;
    }
}