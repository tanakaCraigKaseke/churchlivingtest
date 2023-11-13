using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TimeManagementClassLibrary;

namespace TimeManagementApp
{
    public partial class RecordHours : Window
    {
        // Add properties for selected date and selected module code
        public DateTime SelectedDate { get; set; }
        public string SelectedModuleCode { get; set; }

        private readonly List<Module> modules;

        private readonly Semester currentSemester;

        // Constructor that takes both Semester and List<Module>
        public RecordHours(Semester semester, List<Module> modules)
        {
            InitializeComponent();
            currentSemester = semester;
            this.modules = modules;

            // Populate the ComboBox with module codes
            moduleCodeComboBox.ItemsSource = modules;
        }

        private void DatePicker_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Disable manual input by setting e.Handled to true
            e.Handled = true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve selected module and hours spent from UI
            Module selectedModule = (Module)moduleCodeComboBox.SelectedItem;
            if (selectedModule == null)
            {
                MessageBox.Show("Please select a module.");
                return;
            }

            if (DateTime.TryParse(datePicker.Text, out DateTime date) &&
                Validation.ValidateHoursSpent(hoursTextBox.Text, out double hoursSpent))
            {
                if (Validation.ValidateStudyDate(date, currentSemester.StartDate, currentSemester.NumberOfWeeks))
                {
                    // Calculate the week number
                    int weekNumber = CalculateWeekNumber(date, currentSemester.StartDate);

                    // Create a new HoursRecord object and add it to the selected module
                    HoursRecord hoursRecord = new HoursRecord
                    {
                        Date = date,
                        Module = selectedModule,
                        HoursWorked = hoursSpent
                    };

                    selectedModule.HoursRecords.Add(hoursRecord);

                    // Update the week number for the module
                    selectedModule.WeekNumber = weekNumber;

                    // Clear the input fields
                    datePicker.SelectedDate = null;
                    moduleCodeComboBox.SelectedItem = null;
                    hoursTextBox.Clear();

                    // Clear any previous error message
                    errorLabel.Content = "";
                }
                else
                {
                    // Display an error message
                    errorLabel.Content = "Selected date is not within the semester date range.";
                }
            }
            else
            {
                // Display an error message for hours spent validation
                errorLabel.Content = "Invalid hours spent.";
            }
        }


        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            // Calculate remaining self-study hours for each module
            foreach (var module in modules)
            {
                module.SelfStudyHoursRemaining = module.CalculateRemainingSelfStudyHours();
            }

            // Navigate back to the "DisplayModules" window
            var displayModulesWindow = new DisplayModules(currentSemester, modules);
            displayModulesWindow.Show();

            // Close the "RecordHours" window
            this.Close();
        }

        private int CalculateWeekNumber(DateTime studyDate, DateTime semesterStartDate)
        {
            TimeSpan timeSpan = studyDate - semesterStartDate;
            int days = timeSpan.Days;
            int weekNumber = (days / 7) + 1;
            return weekNumber;
        }


    }
}