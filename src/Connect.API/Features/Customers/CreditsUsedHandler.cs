using Connect.Core;
using Connect.Core.DomainEvents;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Customers
{
    public class CreditsUsedHandler : INotificationHandler<Core.DomainEvents.CreditsUsed>
    {
        private readonly IHubContext<IntegrationEventsHub> _hubContext;

        public CreditsUsedHandler(IHubContext<IntegrationEventsHub> hubContext)
            => _hubContext = hubContext;

        public async Task Handle(CreditsUsed @event, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("events", new {
                Type = nameof(CreditsUsed),
                Payload = @event
            }
            , cancellationToken);
        }
    }
}
