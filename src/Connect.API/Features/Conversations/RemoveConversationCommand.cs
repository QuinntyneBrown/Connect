using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Interfaces;

namespace Connect.API.Features.Conversations
{
    public class RemoveConversationCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ConversationId).NotEqual(default(System.Guid));
            }
        }

        public class Request : IRequest
        {
            public System.Guid ConversationId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                var conversation = await _context.Conversations.FindAsync(request.ConversationId);
                _context.Conversations.Remove(conversation);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
