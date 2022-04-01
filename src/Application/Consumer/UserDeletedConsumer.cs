using System.Threading.Tasks;
using Application.Features.Teacher.Commands;
using MassTransit;
using MediatR;
using Shared.Events;

namespace Application.Consumer
{
    public class UserDeletedConsumer : IConsumer<UserDeleted>
    {
        private readonly IMediator _mediator;

        public UserDeletedConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<UserDeleted> context)
        {
            await _mediator.Send(new DeleteTeacherByAccountIdCommand {AccountId = context.Message.UserId});
        }
    }
}