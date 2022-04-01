using System;

namespace Shared.Events
{
    public interface UserUpdated
    {
        Guid Id { get; }
        string Firstname { get; }

        string Lastname { get; }
    }
}