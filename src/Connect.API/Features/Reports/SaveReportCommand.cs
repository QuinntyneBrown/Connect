using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Models;
using Connect.Core.Interfaces;

namespace Connect.API.Features.Reports
{
    public class SaveReportCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Report.ReportId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public ReportDto Report { get; set; }
        }

        public class Response
        {			
            public System.Guid ReportId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var report = await _context.Reports.FindAsync(request.Report.ReportId);

                if (report == null) _context.Reports.Add(report = new Report());

                //report.RaiseDomainEvent(new ReportSavedEvent.DomainEvent(report));

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ReportId = report.ReportId };
            }
        }
    }
}
