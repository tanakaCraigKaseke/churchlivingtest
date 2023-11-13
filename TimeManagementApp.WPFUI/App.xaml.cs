using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TimeManagementApp.Core.Interfaces;
using TimeManagementApp.Core.Services;
using TimeManagementApp.WPFUI.Interfaces;
using TimeManagementApp.WPFUI.State;
using TimeManagementApp.WPFUI.ViewModels;

namespace TimeManagementApp.WPFUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private IContainer container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ContainerBuilder();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<SemesterService>().As<ISemesterService>().InstancePerLifetimeScope();
            builder.RegisterType<ModuleService>().As<IModuleService>().InstancePerLifetimeScope();
            builder.RegisterType<NavigationState>().As<INavigationState>().SingleInstance();
      
            builder.RegisterType<MainWindowVM>().AsSelf().InstancePerDependency();
            builder.RegisterType<SignInVM>().AsSelf().InstancePerDependency();
            builder.RegisterType<MainPageVM>().AsSelf().InstancePerDependency();


            container = builder.Build();

            var navigationState = container.Resolve<INavigationState>();
            navigationState.CurrentViewModel = container.Resolve<SignInVM>();

            MainWindow = new MainWindow
            {  
                DataContext = container.Resolve<MainWindowVM>() 
            };
            MainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Dispose of the container when the application exits
            container.Dispose();
            base.OnExit(e); 
        }
    }
}
