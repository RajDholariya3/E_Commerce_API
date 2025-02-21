namespace E_Commerce_API.Model
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public int LoyaltyPoint { get; set; }
        public string MembershipType { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? UserName { get;  set; }
    }
}
