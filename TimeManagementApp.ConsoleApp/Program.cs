using Autofac;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeManagementApp.Core.DataTransferObjects;
using TimeManagementApp.Core.Helpers;
using TimeManagementApp.Core.Interfaces;
using TimeManagementApp.Core.Services;

namespace TimeManagementApp.ConsoleApp
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<SemesterService>().As<ISemesterService>().InstancePerLifetimeScope();
            builder.RegisterType<ModuleService>().As<IModuleService>().InstancePerLifetimeScope();
            builder.RegisterType<UserState>().SingleInstance();

            var container = builder.Build();

            var userService = container.Resolve<IUserService>();
            var userState = container.Resolve<UserState>();
            var semesterService = container.Resolve<ISemesterService>();
            var moduleService = container.Resolve<IModuleService>();

            await RegisterUser(userService);
            await LoginUser(userService, userState);
            await CreateASemester(semesterService, userState);
            await GetSemestersForLoggedInUser(semesterService, userState);
            await CreateNewModule(moduleService, userState);
            await LogHoursForModule(moduleService);

            Console.ReadLine();
        }
         
        private static async Task LogHoursForModule(IModuleService moduleService)
        {
            try
            {
                var log = new LogHoursDto
                {
                    ModuleId = 1,
                    Date = DateTime.Now,
                    HoursSpent = 2,
                };

                var response = await moduleService.LogHoursForModule(log);

                if (response.IsSuccesfull)
                {
                    Console.WriteLine(response.Message);
                }else
                {
                    Console.WriteLine(response.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
               
                throw;
            }
        }

        private static async Task CreateNewModule(IModuleService moduleService, UserState userState)
        {
            try
            {
                var newModule = new AddModuleDto
                {
                    UserId = userState.loggedInUser.UserId,
                    SemesterId = 1,
                    Name = "Test module?",
                    Code = "PROG6211",
                    Credits = 15,
                    HoursPerWeek = 10,
                };

                var response = await moduleService.CreateModule(newModule);

                if (response.IsSuccesfull)
                {
                    Console.WriteLine(response.Message);
                }else
                {
                    Console.WriteLine($"{response.Message}");   
                }            
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public static async Task RegisterUser(IUserService userService)
        {
            
            try
            {
                var register = new RegisterDto
                {
                    Username = "tanaka@test.com",
                    FirstName = "Tanaka Kaseke",
                    LastName = "Lasele",
                    PasswordHash = PasswordManager.GeneratePasswordHash("Welcome123")
                };

 
                var response = await userService.RegisterUser(register);

                Console.WriteLine(response.Message);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static async Task LoginUser(IUserService userService, UserState userState)
        {
            try
            {
                var loginDetails = new LoginDto
                {
                    Username = "tanaka@test.com",
                    Password = "Welcome123"
                };
                var response = await userService.LoginUser(loginDetails);

                Console.WriteLine(response.Message);

                if(response.IsSuccesfull)
                {
                    userState.IsLoggedIn = true;
                    userState.loggedInUser = (LoggedInUserDto)response.Data;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }  
        }

        public static async Task CreateASemester(ISemesterService semesterService, UserState userStae)
        {
            try
            {
                var newSemester = new NewSemesterDto
                {
                    UserId = userStae.loggedInUser.UserId,
                    Name = "New Semester",
                    StartDate = DateTime.Now,
                    Weeks = 15
                };

                var response = await semesterService.CreateNewSemester(newSemester);

                if(response.IsSuccesfull)
                {
                    Console.WriteLine(response.Message);
                }else
                {
                    Console.WriteLine(response.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static async Task GetSemestersForLoggedInUser(ISemesterService semesterService, UserState state)
        {
            try
            {
                var response = await semesterService.GetAllSemestersForUser(state.loggedInUser.UserId);
                if(response.IsSuccesfull)
                {
                    Console.WriteLine(response.Message);
                    var data = (List<ExistingSemesterDto>)response.Data;
                    Console.WriteLine(data.Count.ToString());
                }else
                {
                    Console.WriteLine(response.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
  