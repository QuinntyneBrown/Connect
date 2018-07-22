using Connect.API.Features.Orders;
using Connect.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Sagas
{
    public class CreateOrderProcessManager
    {
        public class Request : IRequest<Response> {
            public ICollection<OrderItemApiModel> Items { get; set; } 
                = new HashSet<OrderItemApiModel>();
        }

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

                var orderResponse = await _mediator.Send(new CreateOrderCommand.Request() {
                    Order = new OrderApiModel()
                    {
                        CustomerId = customer.CustomerId,
                        Items = request.Items
                    }
                });

                await _mediator.Send(new ProcessOrderPaymentCommand.Request()
                {
                    OrderId = orderResponse.Order.OrderId
                });

                await _mediator.Send(new ShipOrderCommand.Request()
                {
                   Order = orderResponse.Order
                });

                return new Response() {
                    OrderId = orderResponse.Order.OrderId
                };
            }
        }
    }
}
