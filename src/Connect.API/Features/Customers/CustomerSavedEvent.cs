using Connect.Core;
using Connect.Core.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Customers
{
    public class CustomerSavedEvent
    {
        public class DomainEvent : INotification
        {
            public DomainEvent(Customer customer) => Customer = customer;
            public Customer Customer { get; set; }
        }

        public class Handler : INotificationHandler<DomainEvent>
        {
            private readonly IHubContext<IntegrationEventsHub> _hubContext;

            public Handler(IHubContext<IntegrationEventsHub> hubContext)
                => _hubContext = hubContext;

            public async Task Handle(DomainEvent notification, CancellationToken cancellationToken) {
                await _hubContext.Clients.All.SendAsync("message", new {
                    Type = "[Customer] Saved",
                    Payload = new { customer = CustomerApiModel.FromCustomer(notification.Customer) }
                }, cancellationToken);
            }
        }
    }
}
