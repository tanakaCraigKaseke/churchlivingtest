using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagementApp.Core.DataTransferObjects;

namespace TimeManagementApp.Core.Interfaces
{
    public interface IModuleService
    {
        Task<DataResponseDto> CreateModule(AddModuleDto newModule);

        Task<DataResponseDto> LogHoursForModule(LogHoursDto log);

        Task<ModuleResponseDto> GetModuleById(int  moduleId); 
        Task<DataResponseDto> GetModulesForLoggedInUser(int userId, DateTime selectedDate);
    }
}
