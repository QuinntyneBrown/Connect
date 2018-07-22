using Connect.Core.Interfaces;
using Connect.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Identity
{
    public class SignOutCommand
    {
        public class Request : IRequest<Response> {
            public string Username { get; set; }
        }

        public class Response { }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IEventStore _eventStore;
            public Handler(IEventStore eventStore) => _eventStore = eventStore;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                
                var user = _eventStore.Query<User>("Username", request.Username);

                user.SignOut();

                _eventStore.Save(user);

                return new Response() { };
            }
        }
    }
}
