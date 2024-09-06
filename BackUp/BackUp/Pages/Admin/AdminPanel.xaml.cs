using System.Windows;

namespace Library_Manager
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public AdminPanel()
        {
            InitializeComponent();
            adminFrame.Content = new Pages.Universal.Home("Admin");
            DataBaseManager.BorrowBook(1, 1);
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
            adminFrame.Content = new Pages.Universal.Home("Admin");
        }

        private void emp_btn(object sender, RoutedEventArgs e)
        {
            adminFrame.Content = new Pages.Universal.employees();
        }

        private void books_btn(object sender, RoutedEventArgs e)
        {
            adminFrame.Content = new Pages.Admin.AdminBooks();
        }

        private void budget_btn(object sender, RoutedEventArgs e)
        {
            adminFrame.Content = new Pages.Admin.adminBank();
        }
    }
}
