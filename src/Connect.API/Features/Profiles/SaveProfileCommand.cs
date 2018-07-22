using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Models;
using Connect.Core.Interfaces;
using System.Linq;

namespace Connect.API.Features.Profiles
{
    public class SaveProfileCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Profile.ProfileTypeId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public ProfileDto Profile { get; set; }
        }

        public class Response
        {			
            public System.Guid ProfileId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var profile = await _context.Profiles.FindAsync(request.Profile.ProfileId);

                if (profile == null) _context.Profiles.Add(profile = new Profile());

                profile.Name = request.Profile.Name;

                profile.ProfileTypeId = _context.ProfileTypes.Where(x => x.Name == request.Profile.Name).Single().ProfileTypeId;
                
                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ProfileId = profile.ProfileId };
            }
        }
    }
}
