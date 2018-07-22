using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Interfaces;
using FluentValidation;

namespace Connect.API.Features.Conversations
{
    public class GetConversationByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ConversationId).NotEqual(default(System.Guid));
            }
        }

        public class Request : IRequest<Response> {
            public System.Guid ConversationId { get; set; }
        }

        public class Response
        {
            public ConversationDto Conversation { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Conversation = ConversationDto.FromConversation(await _context.Conversations.FindAsync(request.ConversationId))
                };
        }
    }
}
