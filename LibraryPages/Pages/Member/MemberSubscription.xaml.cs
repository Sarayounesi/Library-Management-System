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
using Library_Manager;

namespace Library_Manager.Pages.Member
{
    /// <summary>
    /// Interaction logic for MemberSubscription.xaml
    /// </summary>
    public partial class MemberSubscription : Page
    {
        int days;
        Library_Manager.Member CurrentMember;
        public MemberSubscription()
        {
            InitializeComponent();
        }
        public MemberSubscription(Library_Manager.Member member)
        {
            InitializeComponent();
            CurrentMember = member;
            updateDays();
            
        }

        private void AddBook_btn_Click(object sender, RoutedEventArgs e)
        {
            if (DataBaseManager.MemberBalance(CurrentMember.Name) >= 30000)
            {
                DataBaseManager.UpdateSubscriptionWithDays(CurrentMember.Name, 30);
                DataBaseManager.UpdateMemberBalance(CurrentMember.Name ,30000);
                updateDays();
            }
            else
            {
                MessageBox.Show(String.Format("You Need {0} T More For Subscription!", 30000 - DataBaseManager.MemberBalance(CurrentMember.Name)), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        void updateDays()
        {
            days = DataBaseManager.CountRemainingDays(CurrentMember.Name);
            if (days >= 0)
            {
                txtDays.Text = days.ToString() + " Days";
            }
            else
            {
                txtDays.Text = (days * -1).ToString() + " Days";
                txtDays.Foreground = new SolidColorBrush(Colors.MediumVioletRed);
                txtselector.Text = "Passed";
            }
        }
    }
}
