using Connect.Core;
using Connect.Core.DomainEvents;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Conversations
{
    public class ConversationRequestedHandler : INotificationHandler<ConversationRequested>
    {
        private readonly IHubContext<IntegrationEventsHub> _hubContext;

        public ConversationRequestedHandler(IHubContext<IntegrationEventsHub> hubContext)
            => _hubContext = hubContext;

        public async Task Handle(ConversationRequested @event, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("events", @event, cancellationToken);
        }
    }
}
