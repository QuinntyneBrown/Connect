using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Connect.Core.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Connect.API.Features.Dashboards
{
    public class GetDashboardByProfileIdQuery
    {
        public class Request : IRequest<Response> {
            public System.Guid ProfileId { get; set; }
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
                    Dashboard = DashboardDto.FromDashboard(await _context.Dashboards
                        .Include(x => x.DashboardCards)
                        .SingleOrDefaultAsync(x => x.ProfileId == request.ProfileId))
                };
        }
    }
}
