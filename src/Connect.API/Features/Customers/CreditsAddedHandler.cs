using Connect.Core;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Customers
{
    public class CreditsAddedHandler : INotificationHandler<Core.DomainEvents.CustomerCreditsAdded>
    {
        private readonly IHubContext<IntegrationEventsHub> _hubContext;

        public CreditsAddedHandler(IHubContext<IntegrationEventsHub> hubContext)
            => _hubContext = hubContext;

        public async Task Handle(Core.DomainEvents.CustomerCreditsAdded @event, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("events", @event, cancellationToken);
        }
    }
}
