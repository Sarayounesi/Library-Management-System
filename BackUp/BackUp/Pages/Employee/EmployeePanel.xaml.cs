using System.Windows;

namespace Library_Manager
{
    /// <summary>
    /// Interaction logic for EmployeePanel.xaml
    /// </summary>
    public partial class EmployeePanel : Window
    {
        Employee employee;
        public EmployeePanel(Employee emp)
        {
            InitializeComponent();
            employeeFrame.Content = new Pages.Universal.Home("Employee");
            employee = emp;
        }
        private void exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void drag(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void home_btn(object sender, RoutedEventArgs e)
        {
            employeeFrame.Content = new Pages.Universal.Home("Employee");
        }

        private void edit_emp_btn(object sender, RoutedEventArgs e)
        {
            employeeFrame.Content = new Pages.Employee.editEmployee();
        }

        private void books_btn(object sender, RoutedEventArgs e)
        {
            employeeFrame.Content = new Pages.Employee.EmployeeBooks();
        }

        private void members_btn(object sender, RoutedEventArgs e)
        {
            employeeFrame.Content = new Pages.Universal.members();
        }

        private void budget_btn(object sender, RoutedEventArgs e)
        {
            employeeFrame.Content = new Pages.Employee.wallet();
        }
    }
}
