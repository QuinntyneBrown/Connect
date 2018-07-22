using Connect.Core;
using Connect.Core.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.ServiceProviders
{
    public class ServiceProviderSavedEvent
    {
        public class DomainEvent : INotification
        {
            public DomainEvent(ServiceProvider serviceProvider) => ServiceProvider = serviceProvider;
            public ServiceProvider ServiceProvider { get; set; }
        }

        public class Handler : INotificationHandler<DomainEvent>
        {
            private readonly IHubContext<IntegrationEventsHub> _hubContext;

            public Handler(IHubContext<IntegrationEventsHub> hubContext)
                => _hubContext = hubContext;

            public async Task Handle(DomainEvent notification, CancellationToken cancellationToken) {
                await _hubContext.Clients.All.SendAsync("message", new {
                    Type = "[ServiceProvider] Saved",
                    Payload = new { serviceProvider = ServiceProviderDto.FromServiceProvider(notification.ServiceProvider) }
                }, cancellationToken);
            }
        }
    }
}
