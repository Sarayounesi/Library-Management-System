using System.Windows;

namespace Library_Manager
{
    /// <summary>
    /// Interaction logic for MemberPanel.xaml
    /// </summary>
    public partial class MemberPanel : Window
    {
        Member member;

        public MemberPanel(Member mem)
        {
            InitializeComponent();
            member = mem;
            memberFrame.Content = new Pages.Universal.Home("Member", member.Name);
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
            memberFrame.Content = new Pages.Universal.Home("Member", member.Name);
        }

        private void edit_member_btn(object sender, RoutedEventArgs e)
        {
            memberFrame.Content = new Pages.Member.editMember();
        }

        private void books_btn(object sender, RoutedEventArgs e)
        {
            memberFrame.Content = new Pages.Member.MemberBook();
        }

        private void mybooks_btn(object sender, RoutedEventArgs e)
        {
            memberFrame.Content = new Pages.Member.MemberMyBook();
        }

        private void subs_btn(object sender, RoutedEventArgs e)
        {
            memberFrame.Content = new Pages.Member.MemberSubscription();
        }

        private void bud_btn(object sender, RoutedEventArgs e)
        {
            memberFrame.Content = new Pages.Member.MemberWallet();
        }
    }
}
