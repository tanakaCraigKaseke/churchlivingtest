using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TimeManagementClassLibrary;

namespace TimeManagementApp
{
    public partial class DisplayModules : Window
    {
        private readonly Semester currentSemester;
        private readonly List<Module> modules; // Add this field

        // Constructor that takes both Semester and List<Module>
        
        public DisplayModules(Semester semester, List<Module> modules)
        {
            InitializeComponent();
            currentSemester = semester;
            this.modules = modules;

            // Calculate self-study hours for each module and bind to the ListView
            foreach (var module in modules)
            {
                module.SelfStudyHours = module.CalculateSelfStudyHours(currentSemester.NumberOfWeeks);
                module.SelfStudyHoursRemaining = module.CalculateRemainingSelfStudyHours();

                
            }


            // Set the ListView's ItemSource to the modules list sorted by module code
            moduleData.ItemsSource = modules.OrderBy(module => module.Code);

            // Show the hidden week number column
            moduleData.Columns[3].Width = 100; // Adjust the index and width as needed
        }

        private void AddModuleMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the AddModule window with the current semester
            var addModuleWindow = new AddModule(currentSemester);
            addModuleWindow.Show();

            // Close the DisplayModules window
            this.Close();
        }

        private void RecordHoursMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Pass the list of modules from the current semester to the RecordHours window
            var recordHoursWindow = new RecordHours(currentSemester, modules);
                recordHoursWindow.ShowDialog(); // Show as a dialog

            // Close the DisplayModules window
            this.Close();
        }

        private void MainHomeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the "MainWindow" or your main home window
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Close the application
            Application.Current.Shutdown();
        }
    }
}