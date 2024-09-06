using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library_Manager.Pages.Member
{
    /// <summary>
    /// Interaction logic for MemberWallet.xaml
    /// </summary>
    public partial class MemberWallet : Page
    {
        int Budget;
        Library_Manager.Member CurrentMember;
        public MemberWallet()
        {
            InitializeComponent();
        }
        public MemberWallet(Library_Manager.Member member)
        {
            InitializeComponent();
            CurrentMember = member;
            UpdatePrice();
        }
        public void UpdatePrice()
        {
            Budget = DataBaseManager.MemberBalance(CurrentMember.Name);
            txtBudget.Text = String.Format("{0:n0}", Budget);
        }

        private void btn_add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (txtAmount.Text != "")
            {
                DataBaseManager.AddMemberBalance(CurrentMember.Name ,int.Parse(txtAmount.Text));
                UpdatePrice();
            }
            else
            {
                MessageBox.Show("Put amount of Money!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
