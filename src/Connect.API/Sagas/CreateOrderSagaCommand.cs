using Connect.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Sagas
{
    public class CreateOrderSagaCommand
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public int OrderId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            private readonly IMediator _mediator;
            private readonly IHttpContextAccessor _httpContextAccessor;
            public Handler(
                IAppDbContext context, 
                IMediator mediator,
                IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                _mediator = mediator;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var httpContext = _httpContextAccessor.HttpContext;

                var customer = _context.Customers
                    .Include(x => x.User)
                    .Where(x => x.User.Username == httpContext.User.Identity.Name)
                    .Single();

                var orderResponse = _mediator.Send(new Features.Orders.CreateOrderCommand.Request() {
                    Order = new Features.Orders.OrderApiModel()
                    {
                        CustomerId = customer.CustomerId
                    }
                });
			    return new Response() { };
            }
        }
    }
}
