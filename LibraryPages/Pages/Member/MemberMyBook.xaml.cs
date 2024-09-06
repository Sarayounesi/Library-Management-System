using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Controls;

namespace Library_Manager.Pages.Member
{
    /// <summary>
    /// Interaction logic for MemberMyBook.xaml
    /// </summary>
    public partial class MemberMyBook : Page
    {
        public ObservableCollection<Book> books { get; set; }
        Library_Manager.Member member;

        public MemberMyBook(Library_Manager.Member mem)
        {
            InitializeComponent();
            member = mem;
            books = new ObservableCollection<Book>();
            DataTable data = DataBaseManager.MyBooks(member.Name);
            for (int i = 0; i < data.Rows.Count; i++)
                books.Add(new Book() { Name = data.Rows[i][0].ToString(), Author = data.Rows[i][1].ToString(), Genre = data.Rows[i][2].ToString(), PrintNumber = data.Rows[i][3].ToString(), Count = int.Parse(data.Rows[i][4].ToString()) });

            DataContext = this;
        }

        private void return_btn(object sender, System.Windows.RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var obj = btn.DataContext as Book;
            int bookId = DataBaseManager.GetBookId(obj.Name);
            int memberId = DataBaseManager.GetMemberId(member.Name);
            DataBaseManager.ReturnBook(bookId, memberId);

            this.NavigationService.Navigate(new Pages.Member.MemberMyBook(member));
        }
    }
}
