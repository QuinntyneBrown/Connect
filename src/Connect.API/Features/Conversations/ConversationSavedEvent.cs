using Connect.Core;
using Connect.Core.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Conversations
{
    public class ConversationSavedEvent
    {
        public class DomainEvent : INotification
        {
            public DomainEvent(Conversation conversation) => Conversation = conversation;
            public Conversation Conversation { get; set; }
        }

        public class Handler : INotificationHandler<DomainEvent>
        {
            private readonly IHubContext<IntegrationEventsHub> _hubContext;

            public Handler(IHubContext<IntegrationEventsHub> hubContext)
                => _hubContext = hubContext;

            public async Task Handle(DomainEvent notification, CancellationToken cancellationToken) {
                await _hubContext.Clients.All.SendAsync("message", new {
                    Type = "[Conversation] Saved",
                    Payload = new { conversation = ConversationDto.FromConversation(notification.Conversation) }
                }, cancellationToken);
            }
        }
    }
}
