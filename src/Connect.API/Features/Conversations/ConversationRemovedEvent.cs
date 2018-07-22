using Connect.Core;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Conversations
{
    public class ConversationRemovedEvent
    {
        public class DomainEvent : INotification
        {
            public DomainEvent(System.Guid conversationId) => ConversationId = conversationId;
            public System.Guid ConversationId { get; set; }
        }

        public class Handler : INotificationHandler<DomainEvent>
        {
            private readonly IHubContext<IntegrationEventsHub> _hubContext;

            public Handler(IHubContext<IntegrationEventsHub> hubContext)
                => _hubContext = hubContext;

            public async Task Handle(DomainEvent notification, CancellationToken cancellationToken) {
                await _hubContext.Clients.All.SendAsync("message", new {
                    Type = "[Conversation] Removed",
                    Payload = new { conversationId = notification.ConversationId }
                }, cancellationToken);
            }
        }
    }
}
