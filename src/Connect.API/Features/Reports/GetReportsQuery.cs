using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Connect.Core.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Connect.API.Features.Reports
{
    public class GetReportsQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<ReportDto> Reports { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Reports = await _context.Reports.Select(x => ReportDto.FromReport(x)).ToListAsync()
                };
        }
    }
}
