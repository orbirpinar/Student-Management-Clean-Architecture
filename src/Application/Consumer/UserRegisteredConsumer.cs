using System.Threading.Tasks;
using Application.Features.Teacher.Commands.CreateTeacher;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Events;

namespace Application.Consumer
{
    public class UserRegisteredConsumer: IConsumer<UserRegistered>
    {
        private readonly ILogger<UserRegisteredConsumer> _logger;
        private readonly IMediator _mediator;

        public UserRegisteredConsumer(ILogger<UserRegisteredConsumer> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }



        public async Task Consume(ConsumeContext<UserRegistered> context)
        {
            
            _logger.LogInformation("ID {id}; USERNAME {username}; EMAIL {email}; ",
                context.Message.Id,context.Message.Username,context.Message.Username);
            var command = new CreateTeacherAccountCommand
            {
                AccountId = context.Message.Id, 
                Firstname = context.Message.Firstname,
                Lastname = context.Message.Lastname,
                Email = context.Message.Email, 
                Username = context.Message.Username
            };
            await _mediator.Send(command);
        }
    }
}