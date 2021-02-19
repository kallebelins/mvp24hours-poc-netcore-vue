namespace ProductAPI.Core.DTOs.Products
{
    public class GetByIdProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string ImageLink { get; set; }
        public string ProductType { get; set; }
        public string Category { get; set; }
    }
}
