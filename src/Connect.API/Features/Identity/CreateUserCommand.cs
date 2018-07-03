using Connect.Core.Interfaces;
using Connect.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Identity
{
    public class CreateUserCommand
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public int UserId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var user = new User();

                _context.Users.Add(user);

                user.RaiseDomainEvent(new Core.DomainEvents.UserCreated(user));

                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    UserId = user.UserId
                };
            }
        }
    }
}
