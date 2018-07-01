using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Connect.API.Features.ServiceProviders
{
    [Authorize]
    [ApiController]
    [Route("api/serviceProviders")]
    public class ServiceProvidersController
    {
        private readonly IMediator _mediator;

        public ServiceProvidersController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<SaveServiceProviderCommand.Response>> Save(SaveServiceProviderCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{serviceProviderId}")]
        public async Task Remove([FromRoute]RemoveServiceProviderCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{serviceProviderId}")]
        public async Task<ActionResult<GetServiceProviderByIdQuery.Response>> GetById([FromRoute]GetServiceProviderByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetServiceProvidersQuery.Response>> Get()
            => await _mediator.Send(new GetServiceProvidersQuery.Request());
    }
}
