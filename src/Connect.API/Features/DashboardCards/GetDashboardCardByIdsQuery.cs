using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Connect.Core.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Connect.API.Features.DashboardCards
{
    public class GetDashboardCardByIdsQuery
    {
        public class Request : IRequest<Response> {
            public System.Guid[] DashboardCardIds { get; set; }
        }

        public class Response
        {
            public IEnumerable<DashboardCardDto> DashboardCards { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    DashboardCards = await _context.DashboardCards
                    .Where(x => request.DashboardCardIds.Contains(x.DashboardCardId))
                    .Select(x => DashboardCardDto.FromDashboardCard(x)).ToListAsync()
                };
        }
    }
}
