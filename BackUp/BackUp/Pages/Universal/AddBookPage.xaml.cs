using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Library_Manager.Pages.Universal
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class AddBookPage : Page
    {
        public AddBookPage()
        {
            InitializeComponent();
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                Book NewBook = new Book(txtBookName.Text, txtBookNumber.Text, txtAuthor.Text, txtGener.Text, int.Parse(txtCount.Text));
                if (DataBaseManager.isBookExists(NewBook))
                {
                    DataBaseManager.AddBook(NewBook);
                    NavigationService.Navigate(new Pages.Admin.AdminBooks());
                }
            }
        }
        bool ValidateFields()
        {
            if (txtBookName.Text == "")
            {
                System.Windows.MessageBox.Show("Book name Can NOT be null!");
                return false;
            }
            if (txtAuthor.Text == "")
            {
                System.Windows.MessageBox.Show("Author Can NOT be null!");
                return false;
            }
            if (txtGener.Text == "")
            {
                System.Windows.MessageBox.Show("Gener Can NOT be null!");
                return false;
            }
            if (txtBookNumber.Text == "")
            {
                System.Windows.MessageBox.Show("Author Can NOT be null!");
                return false;
            }
            if (txtCount.Text == "")
            {
                System.Windows.MessageBox.Show("Count Can NOT be null!");
                return false;
            }
            else
            {
                try
                {
                    int.Parse(txtBookNumber.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Count is not in correct format!");
                    return false;
                }
            }
            return true;
        }
    }
}
