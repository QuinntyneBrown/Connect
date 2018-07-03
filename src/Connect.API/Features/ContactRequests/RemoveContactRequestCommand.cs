using Connect.Core.DomainEvents;
using Connect.Core.Interfaces;
using Connect.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Connect.API.Features.ContactRequests
{
    public class RemoveContactRequestCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ContactRequestId).NotEqual(0);
            }
        }

        public class Request : IRequest
        {
            public int ContactRequestId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                var contactRequest = await _context.ContactRequests.FindAsync(request.ContactRequestId);
                _context.ContactRequests.Remove(contactRequest);
                //contactRequest.RaiseDomainEvent(new ContactRequestRemoved(contactRequest.ContactRequestId));
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
