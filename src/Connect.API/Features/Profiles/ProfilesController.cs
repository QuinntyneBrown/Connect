using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Connect.API.Features.Profiles
{
    [Authorize]
    [ApiController]
    [Route("api/profiles")]
    public class ProfilesController
    {
        private readonly IMediator _mediator;

        public ProfilesController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<CreateProfileCommand.Response>> Save(CreateProfileCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{profileId}")]
        public async Task Remove([FromRoute]RemoveProfileCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{profileId}")]
        public async Task<ActionResult<GetProfileByIdQuery.Response>> GetById([FromRoute]GetProfileByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetProfilesQuery.Response>> Get()
            => await _mediator.Send(new GetProfilesQuery.Request());
    }
}
