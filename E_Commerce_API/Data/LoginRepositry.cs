using System.Data;
using E_Commerce_API.Model;
using Microsoft.Data.SqlClient;

namespace E_Commerce_API.Data
{
    public interface ILoginRepository
    {
        UserModel Login(LoginModel logger);
    }
    public class LoginRepository : ILoginRepository
    {
        private readonly string _connectionString;
        public LoginRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public UserModel Login(LoginModel logger)
        {
            UserModel user = new UserModel();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "PR_User_Login";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("Name", logger.UserName);
                    command.Parameters.AddWithValue("Password", logger.Password);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        user.UserId = Convert.ToInt32(reader["UserId"]);
                        user.Name = reader["UserName"].ToString();
                        user.Email = reader["Email"].ToString();
                        user.PhoneNumber = reader["PhoneNumber"].ToString();
                        user.Address = reader["Address"].ToString();
                        user.IsAdmin = Convert.ToBoolean(reader["IsAdmin"]);
                    
                        return user;
                    }
                    return null;
                }
            }
        }
    }
}
