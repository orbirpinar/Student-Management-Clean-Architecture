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
                }));
            });
            return services;
        }
    }
}