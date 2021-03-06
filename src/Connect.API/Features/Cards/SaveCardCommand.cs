using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Models;
using Connect.Core.Interfaces;

namespace Connect.API.Features.Cards
{
    public class SaveCardCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Card.CardId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public CardDto Card { get; set; }
        }

        public class Response
        {            
            public System.Guid CardId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var card = await _context.Cards.FindAsync(request.Card.CardId);

                if (card == null) _context.Cards.Add(card = new Card());

                card.Name = request.Card.Name;
                card.Description = request.Card.Description;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { CardId = card.CardId };
            }
        }
    }
}
