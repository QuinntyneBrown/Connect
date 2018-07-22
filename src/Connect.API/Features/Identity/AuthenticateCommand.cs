using Connect.Core.Exceptions;
using Connect.Core.Identity;
using Connect.Core.Interfaces;
using Connect.Core.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.API.Features.Identity
{
    public class AuthenticateCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Username).NotEqual(default(string));
                RuleFor(request => request.Password).NotEqual(default(string));
            }            
        }

        public class Request : IRequest<Response>
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class Response
        {
            public string AccessToken { get; set; }
            public System.Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAccessTokenRepository _repository;            
            private readonly IEventStore _eventStore;
            private readonly IOptionsSnapshot<AuthenticationSettings> _authenticationSettings;
            private readonly IPasswordHasher _passwordHasher;
            private readonly ISecurityTokenFactory _securityTokenFactory;
  
            public Handler(IAccessTokenRepository repository, IEventStore eventStore, IOptionsSnapshot<AuthenticationSettings> authenticationSettings, IPasswordHasher passwordHasher, ISecurityTokenFactory securityTokenFactory)
            {
                _eventStore = eventStore;
                _authenticationSettings = authenticationSettings;
                _repository = repository;                
                _passwordHasher = passwordHasher;
                _securityTokenFactory = securityTokenFactory;
            }

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = _eventStore.Query<User>("Username", request.Username);
                
                user.SignIn(_passwordHasher.HashPassword(user.Salt, request.Password));

                _eventStore.Save(user);

                return Task.FromResult(new Response()
                {
                    AccessToken = _securityTokenFactory.Create(request.Username),
                    UserId = user.UserId
                });                
            }               
        }
    }
}
