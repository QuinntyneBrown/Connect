using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Models;
using Connect.Core.Interfaces;
using Connect.Core.Common;
using Connect.Core.Identity;

namespace Connect.API.Features.Profiles
{
    public class CreateProfileCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.ProfileTypeId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public string Name { get; set; }            
            public System.Guid ProfileTypeId { get; set; }
            public System.Guid UserId { get; set; }

        }

        public class Response
        {			
            public System.Guid ProfileId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            private readonly IPasswordHasher _passwordHasher;
            public Handler(IAppDbContext context, IPasswordHasher passwordHasher) {
                _context = context;
                _passwordHasher = passwordHasher;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var profile = default(dynamic);
                
                //if (request.ProfileTypeId == (int)ProfileTypes.Customer)
                //{
                //    profile = new Customer();
                    
                //    _context.Customers.Add(profile);
                //} else
                //{
                //    profile = new ServiceProvider();
                    
                //    _context.ServiceProviders.Add(profile);
                //}

                profile.UserId = request.UserId;

                profile.Name = request.Name;
                
                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ProfileId = profile.ProfileId };
            }
        }
    }
}
