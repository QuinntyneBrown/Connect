using Connect.Core;
using Connect.Core.DomainEvents;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.Core.DomainEvents
{
    public class ProfileCreatedHandler : INotificationHandler<ProfileCreated>
    {
        private readonly IHubContext<IntegrationEventsHub> _hubContext;

        public ProfileCreatedHandler(IHubContext<IntegrationEventsHub> hubContext)
            => _hubContext = hubContext;

        public async Task Handle(ProfileCreated @event, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("events", @event, cancellationToken);
        }
    }
}
