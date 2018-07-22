using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Models;
using Connect.Core.Interfaces;

namespace Connect.API.Features.CardLayouts
{
    public class RemoveCardLayoutCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.CardLayoutId).NotEqual(default(System.Guid));
            }
        }

        public class Request : IRequest
        {
            public System.Guid CardLayoutId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                _context.CardLayouts.Remove(await _context.CardLayouts.FindAsync(request.CardLayoutId));
                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
