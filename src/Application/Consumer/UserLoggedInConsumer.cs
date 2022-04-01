using System.Threading.Tasks;
using MassTransit;
using Shared.Events;

namespace Application.Consumer
{
    public class UserLoggedInConsumer: IConsumer<UserLoggedIn>
    {
        
        public Task Consume(ConsumeContext<UserLoggedIn> context)
        {
            throw new System.NotImplementedException();
        }
    }
}