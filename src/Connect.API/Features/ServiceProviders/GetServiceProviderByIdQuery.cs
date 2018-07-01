using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Interfaces;
using FluentValidation;

namespace Connect.API.Features.ServiceProviders
{
    public class GetServiceProviderByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ServiceProviderId).NotEqual(0);
            }
        }

        public class Request : IRequest<Response> {
            public int ServiceProviderId { get; set; }
        }

        public class Response
        {
            public ServiceProviderApiModel ServiceProvider { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    ServiceProvider = ServiceProviderApiModel.FromServiceProvider(await _context.ServiceProviders.FindAsync(request.ServiceProviderId))
                };
        }
    }
}
