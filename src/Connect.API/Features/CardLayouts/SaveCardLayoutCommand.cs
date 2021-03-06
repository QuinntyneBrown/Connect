using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Models;
using Connect.Core.Interfaces;

namespace Connect.API.Features.CardLayouts
{
    public class SaveCardLayoutCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.CardLayout.CardLayoutId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public CardLayoutDto CardLayout { get; set; }
        }

        public class Response
        {			
            public System.Guid CardLayoutId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var cardLayout = await _context.CardLayouts.FindAsync(request.CardLayout.CardLayoutId);

                if (cardLayout == null) _context.CardLayouts.Add(cardLayout = new CardLayout());

                cardLayout.Name = request.CardLayout.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { CardLayoutId = cardLayout.CardLayoutId };
            }
        }
    }
}
