using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Connect.API.Features.Reports
{
    [Authorize]
    [ApiController]
    [Route("api/reports")]
    public class ReportsController
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<SaveReportCommand.Response>> Save(SaveReportCommand.Request request)
            => await _mediator.Send(request);
        
        [HttpDelete("{reportId}")]
        public async Task Remove([FromRoute]RemoveReportCommand.Request request)
            => await _mediator.Send(request);            

        [HttpGet("{reportId}")]
        public async Task<ActionResult<GetReportByIdQuery.Response>> GetById([FromRoute]GetReportByIdQuery.Request request)
            => await _mediator.Send(request);

        [HttpGet]
        public async Task<ActionResult<GetReportsQuery.Response>> Get()
            => await _mediator.Send(new GetReportsQuery.Request());
    }
}
