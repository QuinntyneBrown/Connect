using Connect.Core;
using Connect.Core.DomainEvents;
using Connect.Core.Interfaces;
using Connect.Core.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Dashboards
{
    public class ProfileCreatedHandler : INotificationHandler<ProfileCreated>
    {
        private readonly IHubContext<IntegrationEventsHub> _hubContext;
        private readonly IAppDbContext _context;

        public ProfileCreatedHandler(IAppDbContext context, IHubContext<IntegrationEventsHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task Handle(ProfileCreated @event, CancellationToken cancellationToken)
        {
            _context.Dashboards.Add(new Dashboard
            {
                Name = "Default",
                ProfileId = @event.EventData.Profile.ProfileId
            });
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
