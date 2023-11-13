using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementApp.Core.Sql.Queries
{
    public static class UserQueries
    {
        public const string UsernameExistsQuery = @"
                                                    SELECT 
                                                    COUNT(*)
                                                    FROM Users 
                                                    WHERE Username = @Username
                                                   ";

        public const string RegisterUserQuery = @"
                                                 INSERT INTO Users (FirstName, LastName, Username, PasswordHash, CreatedAt, UpdatedAt) 
                                                 VALUES (@FirstName, @LastName, @Username, @PasswordHash, GETDATE(), GETDATE()) 
                                                ";

        public const string GetUserByUsername = @"
                                                    SELECT  
                                                    UserId,
                                                    FirstName,
                                                    LastName,
                                                    Username,
                                                    PasswordHash
                                                    FROM [dbo].[Users]
                                                    WHERE Username = @Username
                                                ";

        public const string GetUserById = @"
                                                    SELECT  
                                                    UserId,
                                                    FirstName,
                                                    LastName,
                                                    Username,
                                                    PasswordHash
                                                    FROM [dbo].[Users]
                                                    WHERE UserId = @UserId
                                          ";
    }
}
