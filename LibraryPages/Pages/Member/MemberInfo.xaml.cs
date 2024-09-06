using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Library_Manager.Pages.Member
{
    public partial class MemberInfo : Page
    {
        public ObservableCollection<Book> books { get; set; }
        Library_Manager.Employee employee;
        public MemberInfo(DataTable data, Library_Manager.Employee employee)
        {
            InitializeComponent();
            this.employee = employee;
            txtName.Text = data.Rows[0][1].ToString();
            txtEmail.Text = data.Rows[0][2].ToString();
            txtPhoneNumber.Text = data.Rows[0][3].ToString().Substring(2);
            txtPreNumber.Text = data.Rows[0][3].ToString().Substring(0, 2);
            txtStartDate.Text = data.Rows[0][5].ToString();
            txtEndDate.Text = data.Rows[0][6].ToString();
            remainingDates.Text = (DateTime.Parse(txtEndDate.Text) - DateTime.Now).Days.ToString() + " Days";
            if ((DateTime.Parse(txtEndDate.Text) - DateTime.Now).Days <= 0)
                remainingDates.Foreground = new SolidColorBrush(Colors.MediumVioletRed);

            books = new ObservableCollection<Book>();
            DataTable data2 = DataBaseManager.MyBooks(txtName.Text);
            if (data2.Rows.Count != 0)
                for (int i = 0; i < data.Rows.Count; i++)
                    books.Add(new Book() { Name = data2.Rows[i][0].ToString(), Author = data2.Rows[i][1].ToString(), Genre = data2.Rows[i][2].ToString(), PrintNumber = data2.Rows[i][3].ToString(), Count = int.Parse(data2.Rows[i][4].ToString()) });

            DataContext = this;
        }

        private void delete_btn(object sender, System.Windows.RoutedEventArgs e)
        {
            if (txtPassword.Password == employee.Password)
            {
                DataBaseManager.DeleteMember(txtName.Text);
                MessageBox.Show("Deleted !");
            }
            else
                MessageBox.Show("Wrong Password !");
        }
    }
}
