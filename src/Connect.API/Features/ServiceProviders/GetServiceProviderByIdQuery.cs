using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Interfaces;
using FluentValidation;
using System;

namespace Connect.API.Features.ServiceProviders
{
    public class GetServiceProviderByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ServiceProviderId).NotEqual(default(System.Guid));
            }
        }

        public class Request : IRequest<Response> {
            public System.Guid ServiceProviderId { get; set; }
        }

        public class Response
        {
            public ServiceProviderDto ServiceProvider { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    ServiceProvider = ServiceProviderDto.FromServiceProvider(await _context.ServiceProviders.FindAsync(request.ServiceProviderId))
                };
        }
    }
}
