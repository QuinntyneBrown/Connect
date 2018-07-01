using Connect.Core;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.ServiceProviders
{
    public class ServiceProviderRemovedEvent
    {
        public class DomainEvent : INotification
        {
            public DomainEvent(int serviceProviderId) => ServiceProviderId = serviceProviderId;
            public int ServiceProviderId { get; set; }
        }

        public class Handler : INotificationHandler<DomainEvent>
        {
            private readonly IHubContext<IntegrationEventsHub> _hubContext;

            public Handler(IHubContext<IntegrationEventsHub> hubContext)
                => _hubContext = hubContext;

            public async Task Handle(DomainEvent notification, CancellationToken cancellationToken) {
                await _hubContext.Clients.All.SendAsync("message", new {
                    Type = "[ServiceProvider] Removed",
                    Payload = new { serviceProviderId = notification.ServiceProviderId }
                }, cancellationToken);
            }
        }
    }
}
