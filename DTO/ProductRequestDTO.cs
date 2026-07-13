namespace E_Commerce_api.DTO
{
    public class ProductRequestDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public string SKU { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
    }
}
