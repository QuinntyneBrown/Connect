using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Interfaces;
using FluentValidation;

namespace Connect.API.Features.Reports
{
    public class GetReportByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ReportId).NotEqual(0);
            }
        }

        public class Request : IRequest<Response> {
            public int ReportId { get; set; }
        }

        public class Response
        {
            public ReportApiModel Report { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Report = ReportApiModel.FromReport(await _context.Reports.FindAsync(request.ReportId))
                };
        }
    }
}
