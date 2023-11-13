using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementApp.Core.Sql.Queries
{
    public static class SemesterQueries
    {
        public const string CreateSemester = @"
						   INSERT INTO [dbo].[Semesters]
							(
								UserId,
								Name,
								Weeks,
								StartDate,
								CreatedAt,
								UpdatedAt
							)
							VALUES 
							(
								@UserId,
								@Name,
								@Weeks,
								@StartDate,
								GETDATE(),
								GETDATE()
						)";

		public const string GetUserSemesters = @"
												SELECT  
												SemesterId,
												Name,
												Weeks,
												StartDate
												FROM [dbo].[Semesters]
												WHERE UserId = @UserId";
    }
}
