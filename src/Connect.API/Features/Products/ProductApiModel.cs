using Connect.Core.Models;

namespace Connect.API.Features.Products
{
    public class ProductDto
    {        
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public static ProductDto FromProduct(Product product)
        {
            var model = new ProductDto();
            model.ProductId = product.ProductId;
            model.Name = product.Name;
            model.Description = product.Description;
            return model;
        }
    }
}
