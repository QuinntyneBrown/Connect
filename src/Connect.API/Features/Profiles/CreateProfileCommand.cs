using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Models;
using Connect.Core.Interfaces;

namespace Connect.API.Features.Profiles
{
    public class CreateProfileCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Profile.ProfileTypeId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public ProfileApiModel Profile { get; set; }
        }

        public class Response
        {			
            public int ProfileId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var profile = new Profile();

                _context.Profiles.Add(profile);

                profile.Name = request.Profile.Name;

                profile.RaiseDomainEvent(new Core.DomainEvents.ProfileCreated(profile));
                
                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ProfileId = profile.ProfileId };
            }
        }
    }
}
