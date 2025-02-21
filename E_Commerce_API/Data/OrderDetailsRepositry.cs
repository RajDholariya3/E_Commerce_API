using E_Commerce_API.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using static E_Commerce_API.Model.DropDownModel;

namespace E_Commerce_API.Data
{
    public class OrderDetailsRepository
    {
        private readonly string _connectionString;

        public OrderDetailsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public List<OrderDetailsModel> GetAllOrderDetails()
        {
            var orderDetails = new List<OrderDetailsModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_OrderDetails_SelectAll";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orderDetails.Add(new OrderDetailsModel
                            {
                                OrderDetailId = Convert.ToInt32(reader["OrderDetailId"]),
                                OrderID = Convert.ToInt32(reader["OrderID"]),
                                OrderNumber = reader["OrderNumber"].ToString(),
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                ProductName = (string)reader["ProductName"],
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                Price = Convert.ToDecimal(reader["Price"]),
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
            return orderDetails;
        }

        public bool InsertOrderDetail(OrderDetailsModel orderDetail)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("InsertOrderDetail", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@OrderID", orderDetail.OrderID);
                cmd.Parameters.AddWithValue("@ProductId", orderDetail.ProductId);
                cmd.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
                cmd.Parameters.AddWithValue("@Price", orderDetail.Price);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool UpdateOrderDetail(OrderDetailsModel orderDetail)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateOrderDetail", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@OrderDetailId", orderDetail.OrderDetailId);
                cmd.Parameters.AddWithValue("@OrderID", orderDetail.OrderID);
                cmd.Parameters.AddWithValue("@ProductId", orderDetail.ProductId);
                cmd.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
                cmd.Parameters.AddWithValue("@Price", orderDetail.Price);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool DeleteOrderDetail(int orderDetailId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DeleteOrderDetail", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrderDetailId", orderDetailId);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting order detail: {ex.Message}");
                        return false;
                    }
                }
            }
        }

        public OrderDetailsModel SelectOrderDetailById(int orderDetailId)
        {
            OrderDetailsModel orderDetail = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PR_OrderDetails_SelectById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderDetailId", orderDetailId);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orderDetail = new OrderDetailsModel
                            {
                                OrderDetailId = Convert.ToInt32(reader["OrderDetailId"]),
                                OrderID = Convert.ToInt32(reader["OrderID"]),
                            /*    OrderNumber = reader["OrderNumber"].ToString(),*/
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                Price = Convert.ToDecimal(reader["Price"]),
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

            return orderDetail;
        }

        public List<OrderDetailsDropdownModel> GetOrderDetailsDropdown()
        {
            var orderDetailsList = new List<OrderDetailsDropdownModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_OrderDetails_Dropdown", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    orderDetailsList.Add(new OrderDetailsDropdownModel
                    {
                        OrderDetailID = Convert.ToInt32(reader["OrderDetailID"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["Name"].ToString(),
                        Quantity = Convert.ToInt32(reader["Quantity"])
                    });
                }
            }

            return orderDetailsList;
        }
    }
}
