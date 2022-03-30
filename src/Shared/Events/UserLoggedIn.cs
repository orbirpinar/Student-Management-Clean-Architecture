using System;

namespace Shared.Events
{
    public interface UserLoggedIn
    {
         Guid Id { get; }
         string Username { get; }
         string Email { get; }
         
    }
}