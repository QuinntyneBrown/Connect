using Connect.Core.DomainEvents;
using Connect.Core.Interfaces;
using Connect.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Connect.API.Features.ServiceProviders
{
    public class CreateServiceProviderCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.ServiceProvider.ServiceProviderId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public ServiceProviderDto ServiceProvider { get; set; }
        }

        public class Response
        {			
            public int ServiceProviderId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var serviceProvider = await _context.ServiceProviders.FindAsync(request.ServiceProvider.ServiceProviderId);

                if (serviceProvider == null) _context.ServiceProviders.Add(serviceProvider = new ServiceProvider());

                serviceProvider.Name = request.ServiceProvider.Name;

                //serviceProvider.RaiseDomainEvent(new ServiceProviderSaved(serviceProvider));

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ServiceProviderId = serviceProvider.ServiceProviderId };
            }
        }
    }
}
