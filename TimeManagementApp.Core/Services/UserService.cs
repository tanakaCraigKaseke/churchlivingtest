using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagementApp.Core.DataTransferObjects;
using TimeManagementApp.Core.Interfaces;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using TimeManagementApp.Core.Sql;
using System.Threading;
using TimeManagementApp.Core.Sql.Queries;
using TimeManagementApp.Core.Helpers;

namespace TimeManagementApp.Core.Services
{
    public class UserService : IUserService
    {
        string connectionString = Constants.connectionString;
        public async Task<bool> CheckIfUsernameExists(string username)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string sqlQuery = UserQueries.UsernameExistsQuery;
                int count = await connection.QuerySingleAsync<int>(sqlQuery, new { Username = username });

                return count > 0;
            }
        }

        public async Task<LoggedInUserDto> GetUserById(int userId)
        {
            try
            {
                LoggedInUserDto user = null;
                using(var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    user = await connection.QueryFirstAsync<LoggedInUserDto>(UserQueries.GetUserById, new { UserId = userId });
                    return user;       
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<DataResponseDto> LoginUser(LoginDto loginDetails)
        {
            try
            {
                LoggedInUserDto user = null;
                using(var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    user = await connection.QueryFirstAsync<LoggedInUserDto>(UserQueries.GetUserByUsername, new {  loginDetails.Username });
                    if (user == null)
                    {
                        return new DataResponseDto
                        {
                            IsSuccesfull = false,
                            Message = "Incorrect username / password",                
                        };
                    }  

                    //check if the supplied password is correct
                    var passwordResult = PasswordManager.VerifyPassword(loginDetails.Password, user.PasswordHash);

                    if(passwordResult)
                    {
                        return new DataResponseDto
                        {
                            IsSuccesfull = true,
                            Message = "Login succefull",
                            Data = user
                        };
                    }

                    return new DataResponseDto
                    {
                        IsSuccesfull = false,
                        Message = "Incorrect username / password",
                    };
                }
            }catch(Exception ex)
            {
                return new DataResponseDto
                {
                    IsSuccesfull = false,
                    Message = "Cannot Login users at the moment. Please try again later.",
                    Data = ex.Message
                };
            }
        }

        public async Task<DataResponseDto> RegisterUser(RegisterDto registrationDetail)
        {
            try
            {
                if (await CheckIfUsernameExists(registrationDetail.Username))
                {
                    return new DataResponseDto
                    {
                        IsSuccesfull = false,
                        Message = "The username is already taken. Please use a different username or login to an existing account",
                        Data = null
                    };
                }

                using(var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string sqlQuery = UserQueries.RegisterUserQuery;
                    await connection.ExecuteAsync(sqlQuery, new
                    {
                        registrationDetail.FirstName,
                        registrationDetail.LastName,
                        registrationDetail.Username,
                        registrationDetail.PasswordHash
                    });
                }

                return new DataResponseDto
                {
                    IsSuccesfull = true,
                    Message = "Registration successfull. Please login to access your account"                  
                };

            }catch(Exception ex)
            {
                return new DataResponseDto
                {
                    IsSuccesfull = false,
                    Message = "Cannot register users at this stage. Please try again later.",
                    Data = ex.Message
                };
            }
        }
    }
}
