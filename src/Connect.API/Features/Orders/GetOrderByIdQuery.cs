using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Interfaces;
using FluentValidation;

namespace Connect.API.Features.Orders
{
    public class GetOrderByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.OrderId).NotEqual(0);
            }
        }

        public class Request : IRequest<Response> {
            public int OrderId { get; set; }
        }

        public class Response
        {
            public OrderDto Order { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Order = OrderDto.FromOrder(await _context.Orders.FindAsync(request.OrderId))
                };
        }
    }
}
