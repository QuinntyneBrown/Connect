using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Models;
using Connect.Core.Interfaces;

namespace Connect.API.Features.Profiles
{
    public class RemoveProfileCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ProfileId).NotEqual(0);
            }
        }

        public class Request : IRequest
        {
            public int ProfileId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                var profile = await _context.Profiles.FindAsync(request.ProfileId);
                _context.Profiles.Remove(profile);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
