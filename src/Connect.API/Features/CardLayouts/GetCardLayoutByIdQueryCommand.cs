using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Interfaces;
using FluentValidation;

namespace Connect.API.Features.CardLayouts
{
    public class GetCardLayoutByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.CardLayoutId).NotEqual(default(System.Guid));
            }
        }

        public class Request : IRequest<Response> {
            public System.Guid CardLayoutId { get; set; }
        }

        public class Response
        {
            public CardLayoutDto CardLayout { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    CardLayout = CardLayoutDto.FromCardLayout(await _context.CardLayouts.FindAsync(request.CardLayoutId))
                };
        }
    }
}
