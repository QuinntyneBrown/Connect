using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Connect.Core.Models;
using Connect.Core.Interfaces;

namespace Connect.API.Features.Products
{
    public class RemoveProductCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ProductId).NotEqual(0);
            }
        }

        public class Request : IRequest
        {
            public int ProductId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            public IAppDbContext _context { get; set; }
            
			public Handler(IAppDbContext context) => _context = context;

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.FindAsync(request.ProductId);
                _context.Products.Remove(product);
                //product.RaiseDomainEvent(new ProductRemovedEvent.DomainEvent(product.ProductId));
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
