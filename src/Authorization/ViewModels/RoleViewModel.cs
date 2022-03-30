using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;

namespace Authorization.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        
        
        public static Expression<Func<IdentityRole,RoleViewModel>> Projection
        {
            get
            {
                return x => new RoleViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                };
            }
        }
    }
}