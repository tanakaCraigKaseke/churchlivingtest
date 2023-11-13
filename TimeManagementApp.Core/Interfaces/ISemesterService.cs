using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagementApp.Core.DataTransferObjects;

namespace TimeManagementApp.Core.Interfaces
{
    public interface ISemesterService
    {
        Task<DataResponseDto> CreateNewSemester(NewSemesterDto newSemester);
        Task<DataResponseDto> GetAllSemestersForUser(int userId);

    }
}
