using Connect.Core.DomainEvents;
using Connect.Core.Interfaces;
using Connect.Core.Models;
using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Connect.API.Features.ContactRequests
{
    public class CreateContactRequestCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.ContactRequest.ContactRequestId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public ContactRequestDto ContactRequest { get; set; }
        }

        public class Response
        {			
            public int ContactRequestId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var contactRequest = new ContactRequest();

                _context.ContactRequests.Add(contactRequest);

                contactRequest.Name = request.ContactRequest.Name;

                //contactRequest.RaiseDomainEvent(new ContactRequestSaved(contactRequest));

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ContactRequestId = contactRequest.ContactRequestId };
            }
        }
    }
}
