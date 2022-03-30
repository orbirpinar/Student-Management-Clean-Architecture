using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Authorization.Entities;
using Microsoft.AspNetCore.Identity;

namespace Authorization.ViewModels
{
    public class UserViewModel
    {

        public string Id { get; set; } = null!;
        public string? Firstname { get; set; }
        
        public string? Lastname { get; set; }

        public string? Email { get; set; }
        
        public string? Username { get; set; }

        public IList<string>? roles { get; set; }

        
        public static Expression<Func<User,UserViewModel>> Projection
        {
            get
            {
                return x => new UserViewModel
                {
                    Id = x.Id,
                    Username = x.UserName,
                    Email = x.Email,
                    Firstname = x.Firstname,
                    Lastname = x.Lastname
                };
            }
        }
    }
    
}