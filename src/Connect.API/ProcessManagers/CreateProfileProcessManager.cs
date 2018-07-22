using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Connect.Core.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Connect.API.Features.Identity;
using Connect.API.Features.Profiles;
using System;

namespace Connect.API.Sagas
{
    public class CreateProfileProcessManager
    {
        public class Request : IRequest<Response> {
            public string Username { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
            public string Name { get; set; }
            public System.Guid ProfileTypeId { get; set; }
        }

        public class Response
        {
            public System.Guid ProfileId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            private readonly IMediator _mediator;
            public Handler(IAppDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var createUserResponse = await _mediator.Send(new CreateUserCommand.Request() {
                    Username = request.Username,
                    Password = request.Password,
                    ConfirmPassword = request.ConfirmPassword
                });

                var createProfileResponse = await _mediator.Send(new CreateProfileCommand.Request()
                {
                    UserId = createUserResponse.UserId,
                    Name = request.Name,
                    ProfileTypeId =request.ProfileTypeId
                });

                return new Response() {
                    ProfileId = createProfileResponse.ProfileId
                };
            }
        }
    }
}
