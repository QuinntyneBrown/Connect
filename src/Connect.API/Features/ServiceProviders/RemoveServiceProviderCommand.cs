using Connect.Core.DomainEvents;
using Connect.Core.Interfaces;
using Connect.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Connect.API.Features.ServiceProviders
{
    public class RemoveServiceProviderCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ServiceProviderId).NotEqual(0);
            }
        }

        public class Request : IRequest
        {
            public int ServiceProviderId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                var serviceProvider = await _context.ServiceProviders.FindAsync(request.ServiceProviderId);
                _context.ServiceProviders.Remove(serviceProvider);
                //serviceProvider.RaiseDomainEvent(new ServiceProviderRemoved(serviceProvider.ServiceProviderId));
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
