namespace E_Commerce_API.Model
{
    public class ProductModel
    {
        public int ProductId  { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public bool IsAvailable { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; }
        public string WarrantyPeriod { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? CategoryName { get; set; }
    }
}
