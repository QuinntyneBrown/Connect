using Connect.Core.Models;

namespace Connect.API.Features.Products
{
    public class ProductApiModel
    {        
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public static ProductApiModel FromProduct(Product product)
        {
            var model = new ProductApiModel();
            model.ProductId = product.ProductId;
            model.Name = product.Name;
            model.Description = product.Description;
            return model;
        }
    }
}
