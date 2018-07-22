using Connect.Core;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Products
{
    public class ProductRemovedEvent
    {
        public class DomainEvent : INotification
        {
            public DomainEvent(System.Guid productId) => ProductId = productId;
            public System.Guid ProductId { get; set; }
        }

        public class Handler : INotificationHandler<DomainEvent>
        {
            private readonly IHubContext<IntegrationEventsHub> _hubContext;

            public Handler(IHubContext<IntegrationEventsHub> hubContext)
                => _hubContext = hubContext;

            public async Task Handle(DomainEvent notification, CancellationToken cancellationToken) {
                await _hubContext.Clients.All.SendAsync("message", new {
                    Type = "[Product] Removed",
                    Payload = new { productId = notification.ProductId }
                }, cancellationToken);
            }
        }
    }
}
