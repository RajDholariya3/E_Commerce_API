using E_Commerce_API.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using static E_Commerce_API.Model.DropDownModel;

namespace E_Commerce_API.Data
{
    public class BillRepository
    {
        private readonly string _connectionString;

        public BillRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public List<BillsModel> GetAllBills()
        {
            var bills = new List<BillsModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_Bills_SelectAll";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bills.Add(new BillsModel
                            {
                                BillId = Convert.ToInt32(reader["BillId"]),
                                BillNumber = reader["BillNumber"].ToString(),
                                BillDate = Convert.ToDateTime(reader["BillDate"]),
                                ShippingAddress = reader["ShippingAddress"].ToString(),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                UserName=reader["UserName"].ToString(),
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                ProductName = reader["ProductName"].ToString(),
                                OrderId = Convert.ToInt32(reader["OrderId"]),
                                OrderNumber = reader["OrderNumber"].ToString(),
                                CreatedDate = reader.IsDBNull(reader.GetOrdinal("CreatedDate"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("ModifiedDate"))
                            });
                        }
                    }
                }
            }
            return bills;
        }

        public bool InsertBill(BillsModel bill)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("InsertBill", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BillNumber", bill.BillNumber);
                cmd.Parameters.AddWithValue("@ShippingAddress", bill.ShippingAddress);
                cmd.Parameters.AddWithValue("@UserId", bill.UserId);
                cmd.Parameters.AddWithValue("@ProductId", bill.ProductId);
                cmd.Parameters.AddWithValue("@OrderId", bill.OrderId);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool UpdateBill(BillsModel bill)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateBill", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BillId", bill.BillId);
                cmd.Parameters.AddWithValue("@BillNumber", bill.BillNumber);
                cmd.Parameters.AddWithValue("@ShippingAddress", bill.ShippingAddress);
                cmd.Parameters.AddWithValue("@UserId", bill.UserId);
                cmd.Parameters.AddWithValue("@ProductId", bill.ProductId);
                cmd.Parameters.AddWithValue("@OrderId", bill.OrderId);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool DeleteBill(int billId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DeleteBill", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BillId", billId);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting bill: {ex.Message}");
                        return false;
                    }
                }
            }
        }

        public BillsModel SelectBillById(int billId)
        {
            BillsModel bill = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PR_Bills_SelectById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BillId", billId);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bill = new BillsModel
                            {
                                BillId = Convert.ToInt32(reader["BillId"]),
                                BillNumber = reader["BillNumber"].ToString(),
                                BillDate = Convert.ToDateTime(reader["BillDate"]),
                                ShippingAddress = reader["ShippingAddress"].ToString(),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                UserName = reader["UserName"].ToString(),
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                ProductName = reader["ProductName"].ToString(),
                                OrderId = Convert.ToInt32(reader["OrderId"]),
                                OrderNumber = reader["OrderNumber"].ToString(),
                                CreatedDate = reader.IsDBNull(reader.GetOrdinal("CreatedDate"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("ModifiedDate"))
                            }; 
                        }
                    }
                }
            }

            return bill;
        }

        public List<BillDropdownModel> GetBillDropdown()
        {
            var billList = new List<BillDropdownModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Bill_Dropdown", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    billList.Add(new BillDropdownModel
                    {
                        BillID = Convert.ToInt32(reader["BillID"]),
                        OrderStatus = reader["Status"].ToString()
                    });
                }
            }

            return billList;
        }
    }
}
