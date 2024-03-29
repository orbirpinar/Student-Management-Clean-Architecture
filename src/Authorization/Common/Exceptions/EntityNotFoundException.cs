
namespace Authorization.Common.Exceptions
{
    public class EntityNotFoundException: Exception
    {
        
        public EntityNotFoundException()
        : base()
        {
        }

        public EntityNotFoundException(string message) : base(message)
        {
            
        }

        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
            
        }

        public EntityNotFoundException(string name, string key) : base($"Entity {name} ({key}) was not found.")
        {
            
        }
    }
}