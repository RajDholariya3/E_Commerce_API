namespace E_Commerce_API.Model
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public string? OrderNumber { get; set; }
        public int UserId  { get; set; }
        public string? UserName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
