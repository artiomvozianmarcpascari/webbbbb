namespace Vintagefur.Domain.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsAvailable { get; set; }
        public int? MaterialId { get; set; }
        public string MaterialName { get; set; }
        public int? StyleId { get; set; }
        public string StyleName { get; set; }
    }
} 