using System.Windows;
using System.Windows.Controls;

namespace Library_Manager.Pages.Admin
{
    /// <summary>
    /// Interaction logic for AdminBooks.xaml
    /// </summary>
    public partial class AdminBooks : Page
    {
        AdminPanel StartWindow;
        public AdminBooks()
        {
            InitializeComponent();
        }

        public AdminBooks(AdminPanel StartAdminPage)
        {
            InitializeComponent();
            StartWindow = StartAdminPage;
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Universal.AddBookPage());
        }
    }
}
