using MassTransit;
using Shared.Events;

namespace Application.Consumer
{
    public class UserRegisteredConsumerDefinition: ConsumerDefinition<UserRegisteredConsumer>
    {
        public UserRegisteredConsumerDefinition()
        {
            ConcurrentMessageLimit = 4;
        }
    }
}