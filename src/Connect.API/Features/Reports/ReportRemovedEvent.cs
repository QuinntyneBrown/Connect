using Connect.Core;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Reports
{
    public class ReportRemovedEvent
    {
        public class DomainEvent : INotification
        {
            public DomainEvent(int reportId) => ReportId = reportId;
            public int ReportId { get; set; }
        }

        public class Handler : INotificationHandler<DomainEvent>
        {
            private readonly IHubContext<IntegrationEventsHub> _hubContext;

            public Handler(IHubContext<IntegrationEventsHub> hubContext)
                => _hubContext = hubContext;

            public async Task Handle(DomainEvent notification, CancellationToken cancellationToken) {
                await _hubContext.Clients.All.SendAsync("message", new {
                    Type = "[Report] Removed",
                    Payload = new { reportId = notification.ReportId }
                }, cancellationToken);
            }
        }
    }
}
