using System.Globalization;

namespace E_Commerce_API.Model
{
    public class BillsModel
    {
        public int BillId { get; set; }
        public string BillNumber { get; set; }

        public DateTime BillDate { get; set; }
        public string ShippingAddress { get; set; }
        public int UserId { get; set; }
        public string?  UserName { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int OrderId { get; set; }
        public string? OrderNumber { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
