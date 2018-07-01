using Connect.Core;
using Connect.Core.DomainEvents;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Profiles
{
    public class CreditsConsumedHandler : INotificationHandler<CreditsConsumed>
    {
        private readonly IHubContext<IntegrationEventsHub> _hubContext;

        public CreditsConsumedHandler(IHubContext<IntegrationEventsHub> hubContext)
            => _hubContext = hubContext;

        public async Task Handle(CreditsConsumed @event, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("events", @event, cancellationToken);
        }
    }
}
