namespace Connect.Core.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public float UnitPrice { get; set; }
        public int Units { get; set; }
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        public Product Product { get; set; }
    }
}
