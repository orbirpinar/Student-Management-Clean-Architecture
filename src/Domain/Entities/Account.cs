using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Account : ValueObject
    {
        public Guid Id { get; private set; }
        public string Username { get;  set; } = default!;
        public string Email { get; set; } = default!;

        public string? Firstname { get; set; }
        public string? Lastname { get; set; }

        public Account()
        {
        }
        public Account(Guid id,string username, string email, string? firstname, string? lastname)
        {
            Id = id;
            Username = username;
            Email = email;
            Firstname = firstname;
            Lastname = lastname;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Username;
            yield return Email;
        }
    }
}