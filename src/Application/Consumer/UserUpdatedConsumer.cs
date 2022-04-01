using System.Threading.Tasks;
using Application.Features.Teacher.Commands;
using MassTransit;
using MediatR;
using Shared.Events;

namespace Application.Consumer
{
    public class UserUpdatedConsumer : IConsumer<UserUpdated>
    {
        private readonly IMediator _mediator;

        public UserUpdatedConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<UserUpdated> context)
        {
            var command = new UpdateTeacherAccountCommand {AccountId = context.Message.Id, Firstname = context.Message.Firstname, Lastname = context.Message.Lastname};
            await _mediator.Send(command);
        }
    }
}