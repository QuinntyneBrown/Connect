using Connect.Core.DomainEvents;
using Connect.Core.Interfaces;
using Connect.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Orders
{
    public class CreateOrderCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Order.OrderId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public OrderApiModel Order { get; set; }
        }

        public class Response
        {			
            public int OrderId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var order = new Order();

                _context.Orders.Add(order);
                
                order.RaiseDomainEvent(new OrderCreated(order));

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { OrderId = order.OrderId };
            }
        }
    }
}
