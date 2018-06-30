using Connect.Core;
using Connect.Core.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Reports
{
    public class ReportSavedEvent
    {
        public class DomainEvent : INotification
        {
            public DomainEvent(Report report) => Report = report;
            public Report Report { get; set; }
        }

        public class Handler : INotificationHandler<DomainEvent>
        {
            private readonly IHubContext<IntegrationEventsHub> _hubContext;

            public Handler(IHubContext<IntegrationEventsHub> hubContext)
                => _hubContext = hubContext;

            public async Task Handle(DomainEvent notification, CancellationToken cancellationToken) {
                await _hubContext.Clients.All.SendAsync("message", new {
                    Type = "[Report] Saved",
                    Payload = new { report = ReportApiModel.FromReport(notification.Report) }
                }, cancellationToken);
            }
        }
    }
}
