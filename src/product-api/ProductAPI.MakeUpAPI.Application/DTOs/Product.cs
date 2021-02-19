using Newtonsoft.Json;

namespace ProductAPI.MakeUpAPI.Application.DTOs
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }

        [JsonProperty("image_link")]
        public string ImageLink { get; set; }

        [JsonProperty("product_type")]
        public string ProductType { get; set; }

        public string Category { get; set; }
    }
}
