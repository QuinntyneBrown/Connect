using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Interfaces;
using FluentValidation;

namespace Connect.API.Features.Customers
{
    public class GetCustomerByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.CustomerId).NotEqual(default(System.Guid));
            }
        }

        public class Request : IRequest<Response> {
            public System.Guid CustomerId { get; set; }
        }

        public class Response
        {
            public CustomerDto Customer { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Customer = CustomerDto.FromCustomer(await _context.Customers.FindAsync(request.CustomerId))
                };
        }
    }
}
