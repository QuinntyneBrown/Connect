using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Connect.API.Features.Profiles
{
    [ApiController]
    [Route("api/profiletypes")]
    public class ProfileTypesController
    {
        private readonly IMediator _mediator;
        public ProfileTypesController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<GetProfileTypesQuery.Response>> Get() 
            => await _mediator.Send(new GetProfileTypesQuery.Request());
    }
}
