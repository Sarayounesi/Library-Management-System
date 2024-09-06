using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Library_Manager
{
    /// <summary>
    /// Interaction logic for LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        public LogInPage()
        {
            InitializeComponent();
        }

        private void account_btn(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignUpPage());
        }

        private void login_btn(object sender, RoutedEventArgs e)
        {
            DataTable empData = DataBaseManager.EmpList();
            DataTable memData = DataBaseManager.MemberList();

            string email = txtEmail.Text.ToLower();
            string pass = txtPassword.Password;
            bool isMatch = false;

            if (email == "admin" && pass == "AdminLib123")
            {
                var admin = new AdminPanel();
                admin.Show();
                isMatch = true;
                MainWindow main = Application.Current.MainWindow as MainWindow;
                if (main != null)
                {
                    main.Close();
                }
            }
            if (!isMatch)
            {
                for (int i = 0; i < empData.Rows.Count; i++)
                {
                    if (empData.Rows[i][2].ToString().ToLower() == email && empData.Rows[i][4].ToString() == pass)
                    {
                        var emp = new EmployeePanel(new Employee());
                        emp.Show();
                        isMatch = true;
                        MainWindow main = Application.Current.MainWindow as MainWindow;
                        if (main != null)
                        {
                            main.Close();
                        }
                    }
                }
                if (!isMatch)
                {
                    for (int i = 0; i < memData.Rows.Count; i++)
                    {
                        if (memData.Rows[i][2].ToString().ToLower() == email && memData.Rows[i][4].ToString() == pass)
                        {
                            var emp = new MemberPanel(new Member(memData.Rows[i][1].ToString(), memData.Rows[i][3].ToString()
                                             , memData.Rows[i][2].ToString(), memData.Rows[i][4].ToString(), new Date(2000, 2, 3), memData.Rows[i][6].ToString()));
                            emp.Show();
                            isMatch = true;
                            MainWindow main = Application.Current.MainWindow as MainWindow;
                            if (main != null)
                            {
                                main.Close();
                            }
                        }
                    }
                }
            }
            if (!isMatch)
            {
                MessageBox.Show("This combination of email and password does not exist !!");
            }
        }
    }
}
