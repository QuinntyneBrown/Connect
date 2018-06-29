using Connect.Core;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Profiles
{
    public class ProfileRemovedEvent
    {
        public class DomainEvent : INotification
        {
            public DomainEvent(int profileId) => ProfileId = profileId;
            public int ProfileId { get; set; }
        }

        public class Handler : INotificationHandler<DomainEvent>
        {
            private readonly IHubContext<IntegrationEventsHub> _hubContext;

            public Handler(IHubContext<IntegrationEventsHub> hubContext)
                => _hubContext = hubContext;

            public async Task Handle(DomainEvent notification, CancellationToken cancellationToken) {
                await _hubContext.Clients.All.SendAsync("message", new {
                    Type = "[Profile] Removed",
                    Payload = new { profileId = notification.ProfileId }
                }, cancellationToken);
            }
        }
    }
}
