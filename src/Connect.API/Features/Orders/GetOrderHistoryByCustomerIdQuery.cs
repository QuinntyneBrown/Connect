using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Connect.Core.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Connect.API.Features.Orders
{
    public class GetOrderHistoryByCustomerIdQuery
    {
        public class Request : IRequest<Response> {
            public int CustomerId { get; set; }
        }

        public class Response
        {
            public IEnumerable<OrderApiModel> Orders { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Orders = await _context.Orders.Select(x => OrderApiModel.FromOrder(x)).ToListAsync()
                };
        }
    }
}
