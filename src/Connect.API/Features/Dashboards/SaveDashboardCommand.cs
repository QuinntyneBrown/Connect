using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Models;
using Connect.Core.Interfaces;

namespace Connect.API.Features.Dashboards
{
    public class SaveDashboardCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Dashboard.DashboardId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public DashboardDto Dashboard { get; set; }
        }

        public class Response
        {            
            public System.Guid DashboardId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var dashboard = await _context.Dashboards.FindAsync(request.Dashboard.DashboardId);

                if (dashboard == null) _context.Dashboards.Add(dashboard = new Dashboard());

                dashboard.Name = request.Dashboard.Name;

                dashboard.ProfileId = request.Dashboard.ProfileId;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { DashboardId = dashboard.DashboardId };
            }
        }
    }
}
