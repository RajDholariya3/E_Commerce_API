using E_Commerce_API.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using static E_Commerce_API.Model.DropDownModel;

namespace E_Commerce_API.Data
{
    public class ReviewRepository
    {
        private readonly string _connectionString;

        public ReviewRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public List<ReviewModel> GetAllReviews()
        {
            var reviews = new List<ReviewModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_Reviews_SelectAll";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reviews.Add(new ReviewModel
                            {
                                ReviewId = Convert.ToInt32(reader["ReviewId"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                UserName = reader["UserName"].ToString(),
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                ProductName = reader["ProductName"].ToString(),
                                Description = reader["Description"].ToString(),
                                ReviewDate = Convert.ToDateTime(reader["ReviewDate"]),
                                Rating = Convert.ToDecimal(reader["Rating"]),
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
            return reviews;
        }

        public ReviewModel GetReviewById(int productID)
        {
            ReviewModel review = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_Reviews_SelectById";
                    command.Parameters.AddWithValue("@ProductId", productID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            review = new ReviewModel
                            {
                                ReviewId = Convert.ToInt32(reader["ReviewId"]),
                                UserId = Convert.ToInt32(reader["UserId"]),
                                UserName = reader["UserName"].ToString(),
                               ProductId = Convert.ToInt32(reader["ProductId"]),
                                
                                Description = reader["Description"].ToString(),
                                ReviewDate = Convert.ToDateTime(reader["ReviewDate"]),
                                Rating = Convert.ToDecimal(reader["Rating"]),
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
            return review;
        }

        public bool InsertReview(ReviewModel review)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("InsertReview", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserId", review.UserId);
                cmd.Parameters.AddWithValue("@ProductId", review.ProductId);
                cmd.Parameters.AddWithValue("@Description", review.Description);
                /*cmd.Parameters.AddWithValue("@ReviewDate", review.ReviewDate);*/
                cmd.Parameters.AddWithValue("@Rating", review.Rating);

                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool UpdateReview(ReviewModel review)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateReview", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@ReviewId", review.ReviewId);
                cmd.Parameters.AddWithValue("@UserId", review.UserId);
                cmd.Parameters.AddWithValue("@ProductId", review.ProductId);
                cmd.Parameters.AddWithValue("@Description", review.Description);
                /* cmd.Parameters.AddWithValue("@ReviewDate", review.ReviewDate);*/
                cmd.Parameters.AddWithValue("@Rating", review.Rating);

                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool DeleteReview(int reviewId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteReview", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@ReviewId", reviewId);

                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public List<ReviewDropdownModel> GetReviewDropdown()
        {
            var reviewList = new List<ReviewDropdownModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Review_Dropdown", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    reviewList.Add(new ReviewDropdownModel
                    {
                        ReviewID = Convert.ToInt32(reader["ReviewID"]),
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["Name"].ToString(),
                        Rating = (int)Convert.ToDecimal(reader["Rating"])
                    });
                }
            }

            return reviewList;
        }
    }
}
