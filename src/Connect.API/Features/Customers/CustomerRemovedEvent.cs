using Connect.Core;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Customers
{
    public class CustomerRemovedEvent
    {
        public class DomainEvent : INotification
        {
            public DomainEvent(System.Guid customerId) => CustomerId = customerId;
            public System.Guid CustomerId { get; set; }
        }

        public class Handler : INotificationHandler<DomainEvent>
        {
            private readonly IHubContext<IntegrationEventsHub> _hubContext;

            public Handler(IHubContext<IntegrationEventsHub> hubContext)
                => _hubContext = hubContext;

            public async Task Handle(DomainEvent notification, CancellationToken cancellationToken) {
                await _hubContext.Clients.All.SendAsync("message", new {
                    Type = "[Customer] Removed",
                    Payload = new { customerId = notification.CustomerId }
                }, cancellationToken);
            }
        }
    }
}
