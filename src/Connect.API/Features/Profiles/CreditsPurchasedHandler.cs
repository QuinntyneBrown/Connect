using Connect.Core;
using Connect.Core.DomainEvents;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Profiles
{
    public class CreditsPurchasedHandler : INotificationHandler<CreditsPurchased>
    {
        private readonly IHubContext<IntegrationEventsHub> _hubContext;

        public CreditsPurchasedHandler(IHubContext<IntegrationEventsHub> hubContext)
            => _hubContext = hubContext;

        public async Task Handle(CreditsPurchased notification, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("events", notification, cancellationToken);
        }
    }
}
