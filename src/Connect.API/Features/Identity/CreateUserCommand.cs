using Connect.Core.Identity;
using Connect.Core.Interfaces;
using Connect.Core.Models;
using MediatR;
using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Identity
{
    public class CreateUserCommand
    {
        public class Request : IRequest<Response> {
            public string Username { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }

        public class Response
        {
            public System.Guid UserId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IEventStore _eventStore { get; set; }
            private readonly IPasswordHasher _passwordHasher;
            public Handler(IEventStore eventStore, IPasswordHasher passwordHasher) {
                _eventStore = eventStore;
                _passwordHasher = passwordHasher;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                var user = new User(request.Username, salt, _passwordHasher.HashPassword(salt, request.Password));

                _eventStore.Save(user);

                return new Response()
                {
                    UserId = user.UserId
                };
            }
        }
    }
}
