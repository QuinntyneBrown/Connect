using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Models;
using Connect.Core.Interfaces;

namespace Connect.API.Features.Reports
{
    public class RemoveReportCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ReportId).NotEqual(0);
            }
        }

        public class Request : IRequest
        {
            public int ReportId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                var report = await _context.Reports.FindAsync(request.ReportId);
                _context.Reports.Remove(report);
                report.RaiseDomainEvent(new ReportRemovedEvent.DomainEvent(report.ReportId));
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
