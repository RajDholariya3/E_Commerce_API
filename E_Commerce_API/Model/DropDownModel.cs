namespace E_Commerce_API.Model
{
    public class DropDownModel
    {
        public class OrderDropdownModel
        {
            public int OrderID { get; set; }
            public string OrderNumber { get; set; }

        }

        public class CustomerDropdownModel
        {
            public int CustomerID { get; set; }
            public string CustomerName { get; set; }
        }

        public class OrderDetailsDropdownModel
        {
            public int OrderDetailID { get; set; }
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
        }

        public class BillDropdownModel
        {
            public int BillID { get; set; }
            public string OrderStatus { get; set; }
        }

        public class ProductDropdownModel
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
        
        }

        public class ReviewDropdownModel
        {
            public int ReviewID { get; set; }
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public int Rating { get; set; }
        }

        public class PaymentDropdownModel
        {
            public int PaymentID { get; set; }
            public string PaymentMethod { get; set; }
            public string PaymentStatus { get; set; }
        }

        public class CategoriesDropdownModel
        {
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }
        }

        public class ProductCategoryDropdownModel
        {
            public string BrandName { get; set; }
            public string CategoryName { get; set; }
        }

        public class UserDropDownModel
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
        }

    }
}
