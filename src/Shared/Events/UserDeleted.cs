using System;

namespace Shared.Events
{
    public interface UserDeleted
    {
        public Guid UserId { get; }
    }
}