
using System.Windows;
using TimeManagementClassLibrary;

namespace TimeManagementApp
{
    public partial class AddModule : Window
    {
        private readonly Semester currentSemester;

        private DBController db;

        public AddModule(Semester semester)
        {
            InitializeComponent();
            currentSemester = semester;

            // Initialize the DBController instance
            db = new DBController();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create a new Module object and populate its properties from the UI
            var module = new Module
            {
                Code = moduleCode.Text.ToUpper(), // Convert to uppercase
                Name = moduleName.Text,
            };

            // Validate Module Code
            if (!Validation.IsValidModuleCode(module.Code))
            {
                moduleCodeError.Text = "Enter a code with 4 letters and 4 digits.";
                return;
            }
            else
            {
                moduleCodeError.Text = string.Empty; // Clear error message
            }

            // Validate Name
            if (string.IsNullOrWhiteSpace(module.Name))
            {
                moduleNameError.Text = "Enter a module name."; // Display error message
                return;
            }
            else
            {
                moduleNameError.Text = string.Empty; // Clear error message
            }

            // Validate and set Credits
            if (int.TryParse(creditNumber.Text, out int credits))
            {
                module.Credits = credits;
                creditNumberError.Text = string.Empty; // Clear error message
            }
            else
            {
                creditNumberError.Text = "Please enter a valid integer for credits.";
                return;
            }

            // Validate and set Class Hours per Week
            if (int.TryParse(classHours.Text, out int classHoursPerWeek))
            {
                module.ClassHoursPerWeek = classHoursPerWeek;
                classHoursError.Text = string.Empty; // Clear error message
            }
            else
            {
                classHoursError.Text = "Please enter a valid integer for class hours.";
                return;
            }

            // Add the module to the current semester
            currentSemester.Modules.Add(module);

            // Insert the module into the database
            //db.InsertModuleInformation(module);

            // Clear the input fields for the next module
            moduleCode.Clear();
            moduleName.Clear();
            creditNumber.Clear();
            classHours.Clear();

            // Clear the error messages
            moduleCodeError.Text = string.Empty;
            moduleNameError.Text = string.Empty;
            creditNumberError.Text = string.Empty;
            classHoursError.Text = string.Empty;

            // Show a message box to indicate that data is saved
            MessageBox.Show("Data saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void MainHomeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the MainWindow
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void DisplayMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the DisplayModules window and pass the current semester's modules
            var displayModulesWindow = new DisplayModules(currentSemester, currentSemester.Modules); // Pass current semester's modules
            displayModulesWindow.Show();

            // Close the AddModule window
            this.Close();

        }

            private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Close the application
            Application.Current.Shutdown();
        }

        
    }
}
