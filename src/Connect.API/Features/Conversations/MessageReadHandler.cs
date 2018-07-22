using Connect.Core;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Conversations
{
    public class MessageReadHandler : INotificationHandler<Core.DomainEvents.MessageRead>
    {
        private readonly IHubContext<IntegrationEventsHub> _hubContext;

        public MessageReadHandler(IHubContext<IntegrationEventsHub> hubContext)
            => _hubContext = hubContext;

        public async Task Handle(Core.DomainEvents.MessageRead @event, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("events", @event, cancellationToken);
        }
    }
}
