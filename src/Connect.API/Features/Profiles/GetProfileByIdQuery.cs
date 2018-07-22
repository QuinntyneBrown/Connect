using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Interfaces;
using FluentValidation;

namespace Connect.API.Features.Profiles
{
    public class GetProfileByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ProfileId).NotEqual(default(System.Guid));
            }
        }

        public class Request : IRequest<Response> {
            public System.Guid ProfileId { get; set; }
        }

        public class Response
        {
            public ProfileDto Profile { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Profile = ProfileDto.FromProfile(await _context.Profiles.FindAsync(request.ProfileId))
                };
        }
    }
}
