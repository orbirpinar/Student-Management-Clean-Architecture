using System;

namespace Shared.Events
{
    public interface UserRegistered
    {
        Guid Id { get; }
        string Username { get; }
        string Email { get; }
        
        string Firstname { get; }
        
        string Lastname { get; }
    }
}