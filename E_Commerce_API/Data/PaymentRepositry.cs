using E_Commerce_API.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using static E_Commerce_API.Model.DropDownModel;

namespace E_Commerce_API.Data
{
    public class PaymentRepository
    {
        private readonly string _connectionString;

        public PaymentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public List<PaymentsModel> GetAllPayments()
        {
            var payments = new List<PaymentsModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_Payments_SelectAll";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            payments.Add(new PaymentsModel
                            {
                                PaymentId = Convert.ToInt32(reader["PaymentId"]),
                                OrderId = Convert.ToInt32(reader["OrderId"]),
                                OrderNumber = reader["OrderNumber"].ToString(),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                UserName = reader["UserName"].ToString(),
                                PaymentDate = Convert.ToDateTime(reader["PaymentDate"]),
                                AmountPaid = Convert.ToDecimal(reader["AmountPaid"]),
                                PaymentMethod = reader["PaymentMethod"].ToString(),
                                PaymentStatus = reader["PaymentStatus"].ToString(),
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
            return payments;
        }

        public PaymentsModel GetPaymentById(int paymentId)
        {
            PaymentsModel payment = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_Payments_SelectById";
                    command.Parameters.AddWithValue("@PaymentId", paymentId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            payment = new PaymentsModel
                            {
                                PaymentId = Convert.ToInt32(reader["PaymentId"]),
                                OrderId = Convert.ToInt32(reader["OrderId"]),
/*                                OrderNumber = reader["OrderNumber"].ToString(),
*/                                UserId = Convert.ToInt32(reader["UserId"]),
                                PaymentDate = Convert.ToDateTime(reader["PaymentDate"]),
                                AmountPaid = Convert.ToDecimal(reader["AmountPaid"]),
                                PaymentMethod = reader["PaymentMethod"].ToString(),
                                PaymentStatus = reader["PaymentStatus"].ToString(),
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
            return payment;
        }

        public bool InsertPayment(PaymentsModel payment)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("InsertPayment", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@OrderId", payment.OrderId);
                cmd.Parameters.AddWithValue("@UserId", payment.UserId);
                /*cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);*/
                cmd.Parameters.AddWithValue("@AmountPaid", payment.AmountPaid);
                cmd.Parameters.AddWithValue("@PaymentMethod", payment.PaymentMethod);
                cmd.Parameters.AddWithValue("@PaymentStatus", payment.PaymentStatus);

                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool UpdatePayment(PaymentsModel payment)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdatePayment", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@PaymentId", payment.PaymentId);
                cmd.Parameters.AddWithValue("@OrderId", payment.OrderId);
                cmd.Parameters.AddWithValue("@UserId", payment.UserId);
               /* cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);*/
                cmd.Parameters.AddWithValue("@AmountPaid", payment.AmountPaid);
                cmd.Parameters.AddWithValue("@PaymentMethod", payment.PaymentMethod);
                cmd.Parameters.AddWithValue("@PaymentStatus", payment.PaymentStatus);

                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool DeletePayment(int paymentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeletePayment", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@PaymentId", paymentId);

                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public List<PaymentDropdownModel> GetPaymentDropdown()
        {
            var paymentList = new List<PaymentDropdownModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Payment_Dropdown", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    paymentList.Add(new PaymentDropdownModel
                    {
                        PaymentID = Convert.ToInt32(reader["PaymentID"]),
                        PaymentMethod = reader["PaymentMethod"].ToString(),
                        PaymentStatus = reader["PaymentStatus"].ToString()
                    });
                }
            }

            return paymentList;
        }
    }
}
