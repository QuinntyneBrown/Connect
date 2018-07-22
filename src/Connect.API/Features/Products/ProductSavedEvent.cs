using Connect.Core;
using Connect.Core.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Products
{
    public class ProductSavedEvent
    {
        public class DomainEvent : INotification
        {
            public DomainEvent(Product product) => Product = product;
            public Product Product { get; set; }
        }

        public class Handler : INotificationHandler<DomainEvent>
        {
            private readonly IHubContext<IntegrationEventsHub> _hubContext;

            public Handler(IHubContext<IntegrationEventsHub> hubContext)
                => _hubContext = hubContext;

            public async Task Handle(DomainEvent notification, CancellationToken cancellationToken) {
                await _hubContext.Clients.All.SendAsync("message", new {
                    Type = "[Product] Saved",
                    Payload = new { product = ProductDto.FromProduct(notification.Product) }
                }, cancellationToken);
            }
        }
    }
}
