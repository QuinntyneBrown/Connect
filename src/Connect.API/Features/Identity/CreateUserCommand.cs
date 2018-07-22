using Connect.Core.Identity;
using Connect.Core.Interfaces;
using Connect.Core.Models;
using MediatR;
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
            public int UserId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            private readonly IPasswordHasher _passwordHasher;
            public Handler(IAppDbContext context, IPasswordHasher passwordHasher) {
                _context = context;
                _passwordHasher = passwordHasher;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var user = new User()
                {
                    Username = request.Username
                };

                _context.Users.Add(user);
                
                user.Password = _passwordHasher.HashPassword(user.Salt, request.Password);
                
                await _context.SaveChangesAsync(cancellationToken);

                return new Response()
                {
                    UserId = user.UserId
                };
            }
        }
    }
}
