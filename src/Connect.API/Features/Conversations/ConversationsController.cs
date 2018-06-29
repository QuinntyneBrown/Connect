using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Connect.API.Features.Conversations
{
    [Authorize]
    [ApiController]
    [Route("api/conversations")]
    public class ConversationsController
    {
        private readonly IMediator _mediator;

        public ConversationsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<SaveConversationCommand.Response>> Save(SaveConversationCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{conversationId}")]
        public async Task Remove([FromRoute]RemoveConversationCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{conversationId}")]
        public async Task<ActionResult<GetConversationByIdQuery.Response>> GetById([FromRoute]GetConversationByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetConversationsQuery.Response>> Get()
            => await _mediator.Send(new GetConversationsQuery.Request());
    }
}
