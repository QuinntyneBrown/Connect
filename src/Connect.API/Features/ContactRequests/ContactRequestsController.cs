using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Connect.API.Features.ContactRequests
{
    [Authorize]
    [ApiController]
    [Route("api/contactRequests")]
    public class ContactRequestsController
    {
        private readonly IMediator _mediator;

        public ContactRequestsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<SaveContactRequestCommand.Response>> Save(SaveContactRequestCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{contactRequestId}")]
        public async Task Remove([FromRoute]RemoveContactRequestCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{contactRequestId}")]
        public async Task<ActionResult<GetContactRequestByIdQuery.Response>> GetById([FromRoute]GetContactRequestByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetContactRequestsQuery.Response>> Get()
            => await _mediator.Send(new GetContactRequestsQuery.Request());
    }
}
