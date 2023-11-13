using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TimeManagementApp.Core.DataTransferObjects;
using TimeManagementApp.Core.Interfaces;
using Dapper;
using TimeManagementApp.Core.Sql.Queries;
using System.Reflection;

namespace TimeManagementApp.Core.Services
{
    public class ModuleService : IModuleService
    {
        public string ConnectionString { get; set; } = Constants.connectionString;
        private readonly IUserService _userService;

        public ModuleService(IUserService userService)
        {

            _userService = userService;

        }
        public async Task<DataResponseDto> CreateModule(AddModuleDto newModule)
        {
			try
			{
                var user = await _userService.GetUserById(newModule.UserId);
                if(user == null)
                {
                    return new DataResponseDto
                    {
                        IsSuccesfull = false,
                        Message = "User is not authenticated"
                    };
                }
                using (var connection = new SqlConnection(Constants.connectionString))
                {
                    await connection.OpenAsync();
                    var parameters = new
                    { 
                        newModule.UserId,
                        newModule.SemesterId,
                        newModule.Name,
                        newModule.Code,
                        newModule.Credits,
                        newModule.HoursPerWeek,
                    };

                    var newModuleId = await connection.QuerySingleAsync<int>(ModuleQueries.AddModuleQuery, parameters);

                    return new DataResponseDto
                    {
                        IsSuccesfull = true,
                        Message = "Succesfully added new module",
                        Data = newModuleId
                    };
                } 
            }
			catch (Exception ex)
			{

                return new DataResponseDto
                {
                    IsSuccesfull = false,
                    Message = "Could not create module. Please try again later",
                    Data = ex.Message
                };
			}
        }

        public async Task<DataResponseDto> LogHoursForModule(LogHoursDto log)
        {
            try
            {
                var module = await GetModuleById(log.ModuleId);

                if(module == null)
                {
                   return new DataResponseDto
                    {
                        IsSuccesfull = false,
                        Message = "The selected module does not exist",

                    };
                }
                using (var connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();

                    var parameters = new
                    {
                        log.ModuleId,
                        log.HoursSpent,
                        log.Date
                    };

                    var newlog = await connection.QuerySingleAsync<int>(ModuleQueries.AddModuleLog, parameters);

                    return new DataResponseDto
                    {
                        IsSuccesfull = true,
                        Message = $"Log succesfully: {newlog}"
                    };

                }
            }
            catch (Exception ex)
            {
                return new DataResponseDto
                {
                    IsSuccesfull = false,
                    Message = "Failed to update. Please try again later",
                    Data = ex.Message
                };
            }
        }

        public async Task<ModuleResponseDto> GetModuleById(int moduleId)
        {
            ModuleResponseDto moduleResponseDto = null;

            using(var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();

                var query = @"SELECT 
                                *
                                FROM [dbo].[Modules]
                                WHERE ModuleId =  @ModuleId";
                moduleResponseDto = await connection.QueryFirstAsync<ModuleResponseDto>(query, new {ModuleId = moduleId});

                return moduleResponseDto;
            }
        }

        public async Task<DataResponseDto> GetModulesForLoggedInUser(int userId, DateTime selectedDateRange)
        { 
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();

                    DateTime currentDate = selectedDateRange;
                    DayOfWeek currentDayOfWeek = currentDate.DayOfWeek;
                    DateTime startOfWeek = currentDate.AddDays(-(int)currentDayOfWeek);
                    DateTime endOfWeek = startOfWeek.AddDays(6);
                    string startOfWeekFormatted = startOfWeek.ToString("yyyy-MM-dd");
                    string endOfWeekFormatted = endOfWeek.ToString("yyyy-MM-dd");

                    var query = @"
                            SELECT
                                M.ModuleId,
                                M.Name,
                                M.Code,
                                M.Credits,
                                M.HoursPerWeek,
                                S.Name as SemesterName,
                                S.Weeks,
                                S.StartDate,
                                COALESCE(SUM(ML.HoursSpent), 0) AS TotalHoursSpent,
                                (M.HoursPerWeek - COALESCE(SUM(ML.HoursSpent), 0)) AS HoursRemaining,
                                ((M.Credits * 10) / S.Weeks) - M.HoursPerWeek AS SelfStudyHoursPerWeek
                            FROM
                                [dbo].[Modules] AS M
                            LEFT JOIN [dbo].[Semesters] AS S ON M.SemesterId = S.SemesterId
                            LEFT JOIN
                                [dbo].[ModuleLogs] AS ML ON M.ModuleId = ML.ModuleId
                                AND ML.Date >= @StartDate -- Replace 'start_date' with the start date of your week
                                AND ML.Date <= @EndDate   -- Replace 'end_date' with the end date of your week
                            WHERE M.UserId = @UserId
                            GROUP BY
                                M.ModuleId,
                                M.Name,
                                M.Code,
                                M.Credits,
                                M.HoursPerWeek,
                                S.Name,
                                S.Weeks,
                                S.StartDate;
                            ";

                    var parameters = new
                    {
                        StartDate = startOfWeekFormatted,
                        EndDate = endOfWeekFormatted,
                        UserId = userId,
                    };

                    var response = await connection.QueryAsync<UserModuleDto>(query, parameters);

                    return new DataResponseDto
                    {
                        IsSuccesfull = true,
                        Message = "Succesfully retrived modules",
                        Data = response
                    };
                }
            }
            catch (Exception ex)
            {
                return new DataResponseDto
                {
                    IsSuccesfull = false,
                    Message = "Failed to update. Please try again later",
                    Data = ex.Message
                };
            }
        }
    }
}
  