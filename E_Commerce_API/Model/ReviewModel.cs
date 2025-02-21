namespace E_Commerce_API.Model
{
    public class ReviewModel
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string Description { get; set; }
        public DateTime ReviewDate { get; set; }
        public decimal Rating { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
