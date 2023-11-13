using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TimeManagementClassLibrary;

namespace TimeManagementApp
{
    public class DBController
    {
        private SqlConnection con;
        private SqlCommand cmd;

        string connectionString = "Server=tcp:time-manager-servier.database.windows.net,1433;Initial Catalog=time-manager;Persist Security Info=False;User ID=TimeManager;Password=formula.1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public DBController()
        {
 
        }

        public void InsertStartSemester(Semester semester)
        {
            con.Open();
            string insertQuery = "INSERT INTO StartSemester (NumberOfWeeks, StartDate) VALUES (@NumberOfWeeks, @StartDate)";
            cmd = new SqlCommand(insertQuery, con);
            cmd.Parameters.AddWithValue("@NumberOfWeeks", semester.NumberOfWeeks);
            cmd.Parameters.AddWithValue("@StartDate", semester.StartDate);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public async Task InsertUserAsync(Dto.UserDto user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                await con.OpenAsync();

                string query = @"
                INSERT INTO users (Name, Surname, PasswordHash, Username)
                VALUES (@Name, @Surname, @PasswordHash, @Username);
            ";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Surname", user.Surname);
                    cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@Username", user.Username);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        
        
    }
}
