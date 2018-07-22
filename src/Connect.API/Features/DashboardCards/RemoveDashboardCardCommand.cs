using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Interfaces;

namespace Connect.API.Features.DashboardCards
{
    public class RemoveDashboardCardCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.DashboardCardId).NotEqual(default(System.Guid));
            }
        }

        public class Request : IRequest
        {
            public System.Guid DashboardCardId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            public IAppDbContext _context { get; set; }
            
            public Handler(IAppDbContext context) => _context = context;

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                _context.DashboardCards.Remove(await _context.DashboardCards.FindAsync(request.DashboardCardId));
                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
