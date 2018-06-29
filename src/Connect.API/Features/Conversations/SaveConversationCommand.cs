using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Models;
using Connect.Core.Interfaces;

namespace Connect.API.Features.Conversations
{
    public class SaveConversationCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Conversation.ConversationId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public ConversationApiModel Conversation { get; set; }
        }

        public class Response
        {			
            public int ConversationId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var conversation = await _context.Conversations.FindAsync(request.Conversation.ConversationId);

                if (conversation == null) _context.Conversations.Add(conversation = new Conversation());

                
                conversation.RaiseDomainEvent(new ConversationSavedEvent.DomainEvent(conversation));

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ConversationId = conversation.ConversationId };
            }
        }
    }
}
