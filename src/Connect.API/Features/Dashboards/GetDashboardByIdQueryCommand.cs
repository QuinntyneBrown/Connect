using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Interfaces;
using FluentValidation;

namespace Connect.API.Features.Dashboards
{
    public class GetDashboardByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.DashboardId).NotEqual(0);
            }
        }

        public class Request : IRequest<Response> {
            public int DashboardId { get; set; }
        }

        public class Response
        {
            public DashboardDto Dashboard { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Dashboard = DashboardDto.FromDashboard(await _context.Dashboards.FindAsync(request.DashboardId))
                };
        }
    }
}
