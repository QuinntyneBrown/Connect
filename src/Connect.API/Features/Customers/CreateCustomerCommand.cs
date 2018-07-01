using Connect.Core.DomainEvents;
using Connect.Core.Interfaces;
using Connect.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Connect.API.Features.Customers
{
    public class CreateCustomerCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Customer.CustomerId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public CustomerApiModel Customer { get; set; }
        }

        public class Response
        {			
            public int CustomerId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var customer = await _context.Customers.FindAsync(request.Customer.CustomerId);

                if (customer == null) _context.Customers.Add(customer = new Customer());

                customer.Name = request.Customer.Name;

                //customer.RaiseDomainEvent(new CustomerSaved(customer));

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { CustomerId = customer.CustomerId };
            }
        }
    }
}
