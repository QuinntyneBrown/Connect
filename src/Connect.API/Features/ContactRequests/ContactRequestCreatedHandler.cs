using Connect.Core;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.ContactRequests
{
    public class ContactRequestCreatedHandler : INotificationHandler<Core.DomainEvents.ContactRequestCreated>
    {
        private readonly IHubContext<IntegrationEventsHub> _hubContext;

        public ContactRequestCreatedHandler(IHubContext<IntegrationEventsHub> hubContext)
            => _hubContext = hubContext;

        public async Task Handle(Core.DomainEvents.ContactRequestCreated @event, CancellationToken cancellationToken)
        {
            await _hubContext.Clients.All.SendAsync("events", @event, cancellationToken);
        }
    }
}
