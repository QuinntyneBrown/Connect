using Connect.Core;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Identity
{
    public class UserCreatedHandler : INotificationHandler<Core.DomainEvents.UserCreated>
    {
        private readonly IHubContext<IntegrationEventsHub> _hubContext;

        public UserCreatedHandler(IHubContext<IntegrationEventsHub> hubContext)
            => _hubContext = hubContext;

        public async Task Handle(Core.DomainEvents.UserCreated @event, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("events", @event, cancellationToken);
        }
    }
}
