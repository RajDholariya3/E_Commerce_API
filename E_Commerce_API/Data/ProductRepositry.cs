using E_Commerce_API.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using static E_Commerce_API.Model.DropDownModel;

namespace E_Commerce_API.Data
{
    public class ProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public List<ProductModel> GetAllProducts()
        {
            var products = new List<ProductModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_Products_SelectAll";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new ProductModel
                            {
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                Name = reader["Name"].ToString(),
                                BrandName = reader["BrandName"].ToString(),
                                Description = reader["Description"].ToString(),
                                CategoryId = Convert.ToInt32(reader["CategoryId"]),
                                CategoryName = reader["CategoryName"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"]),
                                Discount = Convert.ToDecimal(reader["Discount"]),
                                IsAvailable = Convert.ToBoolean(reader["IsAvailable"]),
                                StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                                ImageUrl = reader["ImageUrl"].ToString(),
                                WarrantyPeriod = reader["WarrantyPeriod"].ToString(),
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
            return products;
        }

        public ProductModel GetProductById(int productId)
        {
            ProductModel product = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "PR_Products_SelectById";
                    command.Parameters.AddWithValue("@ProductId", productId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product = new ProductModel
                            {
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                Name = reader["Name"].ToString(),
                                BrandName = reader["BrandName"].ToString(),
                                Description = reader["Description"].ToString(),
                                CategoryId = Convert.ToInt32(reader["CategoryId"]),
                                Price = Convert.ToDecimal(reader["Price"]),
                                Discount = Convert.ToDecimal(reader["Discount"]),
                                IsAvailable = Convert.ToBoolean(reader["IsAvailable"]),
                                StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                                ImageUrl = reader["ImageUrl"].ToString(),
                                WarrantyPeriod = reader["WarrantyPeriod"].ToString(),
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
            return product;
        }

        public bool InsertProduct(ProductModel product)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("InsertProduct", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@BrandName", product.BrandName);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Discount", product.Discount);
                cmd.Parameters.AddWithValue("@IsAvailable", product.IsAvailable);
                cmd.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);
                cmd.Parameters.AddWithValue("@ImageUrl", product.ImageUrl);
                cmd.Parameters.AddWithValue("@WarrantyPeriod", product.WarrantyPeriod);

                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool UpdateProduct(ProductModel product)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateProduct", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@BrandName", product.BrandName);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Discount", product.Discount);
                cmd.Parameters.AddWithValue("@IsAvailable", product.IsAvailable);
                cmd.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);
                cmd.Parameters.AddWithValue("@ImageUrl", product.ImageUrl);
                cmd.Parameters.AddWithValue("@WarrantyPeriod", product.WarrantyPeriod);

                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool DeleteProduct(int productId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteProduct", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@ProductId", productId);

                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public List<ProductDropdownModel> GetProductDropdown()
        {
            var productList = new List<ProductDropdownModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Product_Dropdown", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    productList.Add(new ProductDropdownModel
                    {
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["Name"].ToString()
                    });
                }
            }

            return productList;
        }
    }
}
