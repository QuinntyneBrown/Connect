using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Connect.Core.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Connect.API.Features.Profiles
{
    public class GetProfilesQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<ProfileDto> Profiles { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Profiles = await _context.Profiles
                    .Include(x => x.ProfileType)
                    .Select(x => ProfileDto.FromProfile(x)).ToListAsync()
                };
        }
    }
}
