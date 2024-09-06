using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Library_Manager.Pages.Member
{
    /// <summary>
    /// Interaction logic for MemberBook.xaml
    /// </summary>
    public partial class MemberBook : Page
    {
        public ObservableCollection<Book> books { get; set; }
        public DataTable data;

        Library_Manager.Member member;

        public MemberBook(Library_Manager.Member mem)
        {
            InitializeComponent();
            member = mem;
            books = new ObservableCollection<Book>();
            data = DataBaseManager.BookList();
            for (int i = 0; i < data.Rows.Count; i++)
                books.Add(new Book()
                {
                    Name = data.Rows[i][1].ToString(),
                    Author = data.Rows[i][2].ToString(),
                    Genre = data.Rows[i][3].ToString(),
                    PrintNumber = data.Rows[i][4].ToString(),
                    Count = (int)data.Rows[i][5]
                });

            DataContext = this;
        }

        private void borrow_btn(object sender, System.Windows.RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var obj = btn.DataContext as Book;
            int bookId = DataBaseManager.GetBookId(obj.Name);
            int memberId = DataBaseManager.GetMemberId(member.Name);
            DataBaseManager.BorrowBook(bookId, memberId);
        }
        public void Search()
        {
            string search = txtSearch.Text;
            if (!string.IsNullOrEmpty(search))
            {
                if (searchByName.IsChecked == true)
                {
                    books.Clear();
                    for (int i = 0; i < data.Rows.Count; i++)
                        if (data.Rows[i][1].ToString().ToLower().Contains(search.ToLower()))
                            books.Add(new Book()
                            {
                                Name = data.Rows[i][1].ToString(),
                                Author = data.Rows[i][2].ToString(),
                                Genre = data.Rows[i][3].ToString(),
                                PrintNumber = data.Rows[i][4].ToString(),
                                Count = (int)data.Rows[i][5]
                            });
                }
                if (searchByAuthor.IsChecked == true)
                {
                    books.Clear();
                    for (int i = 0; i < data.Rows.Count; i++)
                        if (data.Rows[i][2].ToString().ToLower().Contains(search.ToLower()))
                            books.Add(new Book()
                            {
                                Name = data.Rows[i][1].ToString(),
                                Author = data.Rows[i][2].ToString(),
                                Genre = data.Rows[i][3].ToString(),
                                PrintNumber = data.Rows[i][4].ToString(),
                                Count = (int)data.Rows[i][5]
                            });
                }
            }
            else
            {
                books.Clear();
                for (int i = 0; i < data.Rows.Count; i++)
                    books.Add(new Book()
                    {
                        Name = data.Rows[i][1].ToString(),
                        Author = data.Rows[i][2].ToString(),
                        Genre = data.Rows[i][3].ToString(),
                        PrintNumber = data.Rows[i][4].ToString(),
                        Count = (int)data.Rows[i][5]
                    });
            }
        }

        private void search_btn(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void Search_Change(object sender, TextChangedEventArgs e)
        {
            Search();
        }
    }
}
