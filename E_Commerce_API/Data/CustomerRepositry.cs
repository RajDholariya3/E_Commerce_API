using E_Commerce_API.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using static E_Commerce_API.Model.DropDownModel;

namespace E_Commerce_API.Data
{
    public class CustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public List<CustomerModel> GetAllCustomers()
        {
            var customers = new List<CustomerModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_Customers_SelectAll";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(new CustomerModel
                            {
                                CustomerId = Convert.ToInt32(reader["CustomerId"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                UserName = Convert.ToString(reader["UserName"]),
                                LoyaltyPoint = Convert.ToInt32(reader["LoyaltyPoint"]),
                                MembershipType = reader["MembershipType"].ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"]),
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
            return customers;
        }

        public bool InsertCustomer(CustomerModel customer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("InsertCustomer", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserId", customer.UserId);
                cmd.Parameters.AddWithValue("@LoyaltyPoint", customer.LoyaltyPoint);
                cmd.Parameters.AddWithValue("@MembershipType", customer.MembershipType);
                cmd.Parameters.AddWithValue("@IsActive", customer.IsActive);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool UpdateCustomer(CustomerModel customer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateCustomer", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                cmd.Parameters.AddWithValue("@UserId", customer.UserId);
                cmd.Parameters.AddWithValue("@LoyaltyPoint", customer.LoyaltyPoint);
                cmd.Parameters.AddWithValue("@MembershipType", customer.MembershipType);
                cmd.Parameters.AddWithValue("@IsActive", customer.IsActive);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool DeleteCustomer(int customerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DeleteCustomer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CustomerId", customerId);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting Customer: {ex.Message}");
                        return false;
                    }
                }
            }
        }

        public CustomerModel SelectCustomerById(int customerId)
        {
            CustomerModel customer = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("PR_Customers_SelectById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customer = new CustomerModel
                            {
                                CustomerId = Convert.ToInt32(reader["CustomerId"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                LoyaltyPoint = Convert.ToInt32(reader["LoyaltyPoint"]),
                                MembershipType = reader["MembershipType"].ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"]),
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

            return customer;
        }
        public List<CustomerDropdownModel> GetCustomerDropdowns()
        {
            var customers = new List<CustomerDropdownModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_Dropdown", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    customers.Add(new CustomerDropdownModel
                    {
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        CustomerName = reader["CustomerName"].ToString()
                    });
                }
            }

            return customers;
        }
    }
}
