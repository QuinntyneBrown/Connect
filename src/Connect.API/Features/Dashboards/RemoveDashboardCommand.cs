using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Models;
using Connect.Core.Interfaces;

namespace Connect.API.Features.Dashboards
{
    public class RemoveDashboardCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.DashboardId).NotEqual(default(System.Guid));
            }
        }

        public class Request : IRequest
        {
            public System.Guid DashboardId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            public IAppDbContext _context { get; set; }
            
            public Handler(IAppDbContext context) => _context = context;

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                _context.Dashboards.Remove(await _context.Dashboards.FindAsync(request.DashboardId));
                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
