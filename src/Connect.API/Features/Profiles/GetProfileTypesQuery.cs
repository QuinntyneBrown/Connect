using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Connect.Core.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Connect.API.Features.Profiles
{
    public class GetProfileTypesQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<ProfileTypeApiModel> ProfileTypes { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    ProfileTypes = await _context.ProfileTypes.Select(x => ProfileTypeApiModel.FromProfileType(x)).ToListAsync()
                };
        }
    }
}
