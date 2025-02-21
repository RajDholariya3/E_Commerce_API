namespace E_Commerce_API.Model
{
    public class OrderDetailsModel
    {
        public int OrderDetailId { get; set; }
        public int OrderID { get; set; }
        public string? OrderNumber { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
       

        public DateTime OrderDate { get; set; }
    }
}
