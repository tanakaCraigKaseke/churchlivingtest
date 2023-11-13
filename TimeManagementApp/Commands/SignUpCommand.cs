using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TimeManagementApp.Dto;
using TimeManagementApp.ViewModels;

namespace TimeManagementApp.Commands
{
    internal class SignUpCommand : BaseCommandAsync
    {

        private readonly SignUpVM _signUpModel;

        public SignUpCommand(SignUpVM signUpVM) 
        { 
            _signUpModel = signUpVM;
        }
        protected override async Task ExecuteAsync(object parameter)
        {
            string salt = "2o/+y6PQOL1Yj1NZ07vkeQ==";
            var user = new UserDto
            {
                Name = _signUpModel.Name,
                Surname = _signUpModel.Surname,
                Username = _signUpModel.Username,
                PasswordHash = CreatePasswordHash(_signUpModel.Password, out salt)
            };

            var db = new DBController();
            await  db.InsertUserAsync(user);

        }

        public static string CreatePasswordHash(string password, out string salt)
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] saltBytes = new byte[32];
                rng.GetBytes(saltBytes);
                salt = Convert.ToBase64String(saltBytes);

                using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
                {
                    byte[] hashBytes = pbkdf2.GetBytes(32); // 32 bytes for a 256-bit hash
                    return Convert.ToBase64String(hashBytes);
                }
            }
        }
    }
}
