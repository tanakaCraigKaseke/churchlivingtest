using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementApp.Core.Sql.Queries
{
    public static class ModuleQueries
    {
        public const string AddModuleQuery = @"INSERT INTO [dbo].[Modules]
                                            (UserId, SemesterId, Name, Code, Credits, HoursPerWeek, CreatedAt, UpdatedAt)
                                            OUTPUT INSERTED.ModuleId
                                            VALUES
                                            (@UserId, @SemesterId, @Name, @Code, @Credits, @HoursPerWeek, GETDATE(), GETDATE())
                                            ";

        public const string AddModuleLog = @"INSERT INTO [dbo].[ModuleLogs]
                                            (ModuleId, HoursSpent, Date, CreatedAt, UpdatedAt)
                                            OUTPUT INSERTED.ModuleLogId
                                            VALUES
                                            (@ModuleId, @HoursSpent, @Date, GETDATE(), GETDATE())";
    }
}
