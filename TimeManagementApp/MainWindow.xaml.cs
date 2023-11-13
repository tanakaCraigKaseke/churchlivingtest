using System.Windows;
using TimeManagementClassLibrary;

namespace TimeManagementApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartSemester_Click(object sender, RoutedEventArgs e)
        {
            // Create a new Semester object
            var semester = new Semester();

            // Navigate to the StartSemester window with the Semester object
            var startSemesterWindow = new StartSemester(semester);
            startSemesterWindow.Show();
            this.Close();
        }

        private void ExitApp_Click(object sender, RoutedEventArgs e)
        {
            // Close the application when the Exit button is clicked
            Application.Current.Shutdown();
        }
    }
}
