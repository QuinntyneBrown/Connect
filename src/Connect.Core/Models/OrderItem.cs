namespace Connect.Core.Models
{
    public class OrderItem
    {
        public System.Guid OrderId { get; set; }
        public System.Guid ProductId { get; set; }
        public float UnitPrice { get; set; }
        public System.Guid Units { get; set; }
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        public Product Product { get; set; }
    }
}
