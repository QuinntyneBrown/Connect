using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Connect.Core.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Connect.API.Features.Customers
{
    public class GetCustomersQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<CustomerApiModel> Customers { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Customers = await _context.Customers.Select(x => CustomerApiModel.FromCustomer(x)).ToListAsync()
                };
        }
    }
}
