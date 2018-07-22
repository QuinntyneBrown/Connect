using Connect.Core.DomainEvents;
using Connect.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Connect.Core.Common.Products;

namespace Connect.API.Features.Orders
{
    public class ShipOrderCommand
    {
        public class Request : IRequest<Response> {
            public OrderApiModel Order { get; set; }
        }

        public class Response { }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IMediator _mediator;
            public Handler(IMediator mediator) => _mediator = mediator;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                
                foreach (var item in request.Order.Items)
                {
                    int credits = default(int);
                    switch (item.ProductId)
                    {
                        case (int)Credits100:
                            credits = 100;
                            break;

                        case (int)Credits300:
                            credits = 300;
                            break;

                        case (int)Credits750:
                            credits = 750;
                            break;
                    }
                    
                }
                
                return new Response() { };
            }
        }
    }
}
