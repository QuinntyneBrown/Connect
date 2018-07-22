using Connect.Core.DomainEvents;
using Connect.Core.Interfaces;
using Connect.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Connect.API.Features.Customers
{
    public class RemoveCustomerCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.CustomerId).NotEqual(default(System.Guid));
            }
        }

        public class Request : IRequest
        {
            public System.Guid CustomerId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                var customer = await _context.Customers.FindAsync(request.CustomerId);
                _context.Customers.Remove(customer);
                //customer.RaiseDomainEvent(new CustomerRemoved(customer.CustomerId));
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
