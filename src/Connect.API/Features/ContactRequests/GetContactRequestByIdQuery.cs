using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Interfaces;
using FluentValidation;

namespace Connect.API.Features.ContactRequests
{
    public class GetContactRequestByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ContactRequestId).NotEqual(0);
            }
        }

        public class Request : IRequest<Response> {
            public int ContactRequestId { get; set; }
        }

        public class Response
        {
            public ContactRequestDto ContactRequest { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    ContactRequest = ContactRequestDto.FromContactRequest(await _context.ContactRequests.FindAsync(request.ContactRequestId))
                };
        }
    }
}
