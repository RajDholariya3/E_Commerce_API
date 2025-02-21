using E_Commerce_API.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace E_Commerce_API.Data
{
    public class DashboardRepository
    {
        private readonly string _connectionString;

        public DashboardRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public DashboardModel GetDashboardMetrics()
        {
            DashboardModel dashboard = new DashboardModel();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetDashboardMetrics", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                    dashboard.TotalSales = Convert.ToDecimal(reader["TotalSales"]);
                if (reader.NextResult() && reader.Read())
                    dashboard.TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]);
                if (reader.NextResult() && reader.Read())
                    dashboard.TotalOrders = Convert.ToInt32(reader["TotalOrders"]);
                if (reader.NextResult() && reader.Read())
                    dashboard.TotalCustomers = Convert.ToInt32(reader["TotalCustomers"]);

                con.Close();
            }

            return dashboard;
        }

        public RevenueDataModel GetYearWiseRevenueData()
        {
            RevenueDataModel revenueList = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetYearWiseRevenueData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            revenueList = new RevenueDataModel
                            {
                                Year = reader.GetInt32(0),
                                TotalRevenue = reader.GetDecimal(1),
                                GrowthPercentage = reader.IsDBNull(2) ? (decimal?)null : reader.GetDecimal(2)
                            };
                        }
                    }
                }
            }

            return revenueList;
        }




        public List<PaymentSummaryModel> GetPaymentsSummary()
        {
            var payments = new List<PaymentSummaryModel>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetPaymentsSummary", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        payments.Add(new PaymentSummaryModel
                        {
                            PaymentMethod = reader.GetString(0),
                            TotalPayments = reader.GetDecimal(1)
                        });
                    }
                }
            }
            return payments;
        }

        

        public TransactionsSummaryModel GetTransactionsSummary()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetTransactionsSummary", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new TransactionsSummaryModel
                        {
                            TotalOrders = reader.GetInt32(0),
                            OrderAmount = reader.GetDecimal(1),
                            TotalPayments = reader.GetInt32(2),
                            PaymentAmount = reader.GetDecimal(3),
                            TotalBills = reader.GetInt32(4),
                            BillAmount = reader.GetDecimal(5)
                        };
                    }
                }
            }
            return new TransactionsSummaryModel();
        }
    }
}
