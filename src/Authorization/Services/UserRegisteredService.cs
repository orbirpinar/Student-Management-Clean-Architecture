using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Shared.Events;

namespace Authorization.Services
{
    public class UserRegisteredService
    {
        private readonly IBus _bus;


        public UserRegisteredService(IBus bus)
        {
            _bus = bus;
        }

    }
}