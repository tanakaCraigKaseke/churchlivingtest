using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagementApp.Core.DataTransferObjects;
using TimeManagementApp.Core.Interfaces;
using TimeManagementApp.Core.Sql.Queries;

namespace TimeManagementApp.Core.Services
{
    public class SemesterService : ISemesterService
    {
        private readonly IUserService _userService;
        public const string connectionString = Constants.connectionString;

        public SemesterService(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<DataResponseDto> CreateNewSemester(NewSemesterDto newSemester)
        {
           try
            { 
                var user = await _userService.GetUserById(newSemester.UserId);
                if(user == null)
                {
                    return new DataResponseDto
                    {
                        IsSuccesfull = false,
                        Message = "User does not exist, please login to create a semester",                
                    };
                }

                using(var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    await connection.ExecuteAsync(SemesterQueries.CreateSemester, new
                    {
                        newSemester.UserId,
                        newSemester.Name,
                        newSemester.Weeks,
                        newSemester.StartDate
                    }); 

                    return new DataResponseDto
                    {
                        IsSuccesfull = true,
                        Message = "Succesfully added a semester"
                    };
                }

            }catch(Exception ex)
            {
                return new DataResponseDto
                {
                    IsSuccesfull = false,
                    Message = "Failed to create a semester,please try again later",
                    Data = ex.Message
                };
            }
        }

        public async Task<DataResponseDto> GetAllSemestersForUser(int userId)
        {
            try
            {
                var user = await _userService.GetUserById(userId);
                if (user == null)
                {
                    return new DataResponseDto
                    {
                        IsSuccesfull = false,
                        Message = "User does not exist, please login to create a semester",
                    };
                }  

                IEnumerable<ExistingSemesterDto> semester = null;
                using(var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    semester = await connection.QueryAsync<ExistingSemesterDto>(SemesterQueries.GetUserSemesters, new
                    {
                        UserId = userId
                    });

                    return new DataResponseDto
                    {
                        IsSuccesfull = true,
                        Message = "Succesfully returned data",
                        Data = semester ?? new List<ExistingSemesterDto>()
                    };
                    
                }
            }
            catch (Exception ex)
            {
                return new DataResponseDto
                {
                    IsSuccesfull = false,
                    Message = "Failed to fetch semesters semester,please try again later",
                    Data = ex.Message
                };
            }
        }
    }
}
