using Connect.Core;
using Connect.Core.DomainEvents;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Customers
{
    public class CustomerCreatedHandler : INotificationHandler<Core.DomainEvents.CustomerCreated>
    {
        private readonly IHubContext<IntegrationEventsHub> _hubContext;

        public CustomerCreatedHandler(IHubContext<IntegrationEventsHub> hubContext)
            => _hubContext = hubContext;

        public async Task Handle(CustomerCreated @event, CancellationToken cancellationToken)
            => await _hubContext.Clients.All
            .SendAsync("events", new
            {
                Type = nameof(CustomerCreated),
                Payload = @event
            }, cancellationToken);
    }
}
