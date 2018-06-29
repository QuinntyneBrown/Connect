using Connect.Core;
using Connect.Core.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Profiles
{
    public class ProfileSavedEvent
    {
        public class DomainEvent : INotification
        {
            public DomainEvent(Profile profile) => Profile = profile;
            public Profile Profile { get; set; }
        }

        public class Handler : INotificationHandler<DomainEvent>
        {
            private readonly IHubContext<IntegrationEventsHub> _hubContext;

            public Handler(IHubContext<IntegrationEventsHub> hubContext)
                => _hubContext = hubContext;

            public async Task Handle(DomainEvent notification, CancellationToken cancellationToken) {
                await _hubContext.Clients.All.SendAsync("message", new {
                    Type = "[Profile] Saved",
                    Payload = new { profile = ProfileApiModel.FromProfile(notification.Profile) }
                }, cancellationToken);
            }
        }
    }
}
