using System;
using System.Reflection;
using Application.Consumer;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMassTransit(x =>
            {
                x.AddConsumers(typeof(UserRegisteredConsumer).Assembly);
                x.AddConsumers(typeof(UserUpdatedConsumer).Assembly);
                x.AddConsumers(typeof(UserDeletedConsumer).Assembly);
                x.SetKebabCaseEndpointNameFormatter();
                x.AddBus(_ => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                  cfg.Host(new Uri("rabbitmq://localhost"), h =>
                  {
                      h.Username("guest");
                      h.Password("guest");
                  });  
                  cfg.ReceiveEndpoint("user-register",  e=>
                  {
                      e.ConfigureConsumer<UserRegisteredConsumer>(_);
                  });
                  cfg.ReceiveEndpoint("user-updated",  e=>
                  {
                      e.ConfigureConsumer<UserUpdatedConsumer>(_);
                  });
                  cfg.ReceiveEndpoint("user-deleted",  e=>
                  {
                      e.ConfigureConsumer<UserDeletedConsumer>(_);
                  });
                }));
            });
            return services;
        }
    }
}