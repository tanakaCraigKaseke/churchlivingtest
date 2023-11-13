using System;
using System.Windows;
using System.Windows.Input;
using TimeManagementClassLibrary;

namespace TimeManagementApp
{
    public partial class StartSemester : Window
    {
        private readonly Semester currentSemester;

        private DBController db;

        public StartSemester(Semester semester)
        {
            InitializeComponent();
            currentSemester = semester;


            // Initialize the DBController instance
            db = new DBController();

            // Attach a handler to the DatePicker's PreviewKeyDown event
            datePicker.PreviewKeyDown += DatePicker_PreviewKeyDown;
        }
        private void DatePicker_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Disable manual input by setting e.Handled to true
            e.Handled = true;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool isWeeksValid = int.TryParse(weeksno.Text, out int numberOfWeeks);
            bool isDateValid = datePicker.SelectedDate.HasValue;

            if (isWeeksValid && isDateValid)
            {
                // Update the current semester properties
                currentSemester.NumberOfWeeks = numberOfWeeks;
                currentSemester.StartDate = datePicker.SelectedDate.Value;

                // Insert the semester information into the database using the db object
                db.InsertStartSemester(currentSemester); // Implement the InsertSemester method in DBController

                // Navigate to the AddModule window
                var addModuleWindow = new AddModule(currentSemester);
                addModuleWindow.Show();

                // Close the StartSemester window
                this.Close();
            }
            else
            {
                // Display error labels for invalid input
                if (!isWeeksValid)
                {
                    errorLabel.Visibility = Visibility.Visible; // Show weeks error label
                }
                else
                {
                    errorLabel.Visibility = Visibility.Collapsed; // Hide weeks error label
                }

                if (!isDateValid)
                {
                    dateErrorLabel.Visibility = Visibility.Visible; // Show date error label
                }
                else
                {
                    dateErrorLabel.Visibility = Visibility.Collapsed; // Hide date error label
                }
            }
        }


        private void MainHomeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the MainWindow
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