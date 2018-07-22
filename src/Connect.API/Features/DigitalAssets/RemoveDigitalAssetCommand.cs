using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Models;
using Connect.Core.Interfaces;

namespace Connect.API.Features.DigitalAssets
{
    public class RemoveDigitalAssetCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.DigitalAssetId).NotEqual(default(System.Guid));
            }
        }

        public class Request : IRequest
        {
            public System.Guid DigitalAssetId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            public IAppDbContext _context { get; set; }
            
            public Handler(IAppDbContext context) => _context = context;

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                _context.DigitalAssets.Remove(await _context.DigitalAssets.FindAsync(request.DigitalAssetId));
                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
