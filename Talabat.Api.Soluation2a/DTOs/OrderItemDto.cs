namespace Talabat.Api.Soluation2a.DTOs
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int productId { get; set; }
        public string productName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}