using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace Library_Manager
{
    public class DataBaseManager
    {
        static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\University\Term 4\AP\Programming\Project\Library-project\Library-project\Library Manager\DataBase\LibraryDataBase.mdf;Integrated Security=True;Connect Timeout=30;MultipleActiveResultSets=True");
        static string command;
        static DataBaseManager()
        {
        }

        public static void addMemberBalance(string Name, int amount)
        {
            con.Open();
            command = String.Format("UPDATE tblMembers SET Balance = Balance + '{0}' WHERE Name = '{1}'", amount, Name);
            SqlCommand com = new SqlCommand(command, con);
            com.BeginExecuteNonQuery();
            Thread.Sleep(2000);
            con.Close();
        }
        public static bool isMemberExists(string name, string email, string phoneNumber)
        {
            DataTable data = MemberList();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][1].ToString() == name || data.Rows[i][2].ToString() == email || data.Rows[i][3].ToString() == phoneNumber)
                {
                    System.Windows.MessageBox.Show("Same info exists !!");
                    return false;
                }
            }
            return true;
        }
        public static bool isEmployeeExists(string name, string email, string phoneNumber)
        {
            DataTable data = EmpList();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][1].ToString() == name || data.Rows[i][2].ToString() == email || data.Rows[i][3].ToString() == phoneNumber)
                {
                    System.Windows.MessageBox.Show("Same info exists !!");
                    return false;
                }
            }
            return true;
        }
        public static void AddMember(Member MemberToAdd)
        {
            con.Open();
            command = String.Format("INSERT INTO tblMembers (Name, Email, PhoneNumber, Password, SignDate, SubscriptionDate, ImageFileName, Balance) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}', '{6}', '{7}');", MemberToAdd.Name, MemberToAdd.Email, MemberToAdd.PhoneNumber, MemberToAdd.Password, Date.DateToDateTime(MemberToAdd.SignDate).ToString(), Date.DateToDateTime(MemberToAdd.ExtensionDate).ToString(), MemberToAdd.ImageFileName, 0);
            SqlCommand com = new SqlCommand(command, con);
            com.BeginExecuteNonQuery();
            Thread.Sleep(2000);
            con.Close();
        }
        public static DataTable MemberList()
        {
            command = "SELECT * from tblMembers";
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            con.Close();
            return table;
        }
        public static DataTable EmpList()
        {
            con.Open();
            command = "SELECT * from tblEmployees";
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            con.Close();
            return table;
        }
        public static DataTable BookList()
        {
            con.Open();
            command = "SELECT * from tblBooks";
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            con.Close();
            return table;
        }
        public static bool isBookExists(Book BookToAdd)
        {
            DataTable data = BookList();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i][1].ToString() == BookToAdd.Name || data.Rows[i][4].ToString() == BookToAdd.PrintNumber)
                {
                    System.Windows.MessageBox.Show("Book con NOT be Added !\nSame info exists!!");
                    return false;
                }
            }
            return true;
        }
        public static void AddBook(Book BookToAdd)
        {
            con.Open();
            command = String.Format("INSERT INTO tblBooks (BookName, Author, Genre, PrintNumber, Count) VALUES ('{0}','{1}','{2}','{3}','{4}');", BookToAdd.Name, BookToAdd.Author, BookToAdd.Genre, BookToAdd.PrintNumber, BookToAdd.Count);
            SqlCommand com = new SqlCommand(command, con);
            com.BeginExecuteNonQuery();
            Thread.Sleep(2000);
            con.Close();
        }
        public static void AddEmployee(Employee EmployeeToAdd)
        {
            con.Open();
            command = String.Format("INSERT INTO tblEmployees (Name, Email, PhoneNumber, Password, Salary, ImageFileName, Balance) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}');", EmployeeToAdd.Name, EmployeeToAdd.Email, EmployeeToAdd.PhoneNumber, EmployeeToAdd.Password, EmployeeToAdd.Salary, EmployeeToAdd.ImageFileName, EmployeeToAdd.Balance);
            SqlCommand com = new SqlCommand(command, con);
            com.BeginExecuteNonQuery();
            Thread.Sleep(2000);
            con.Close();
        }
        public static void DeleteEmployee(string Name)
        {
            con.Open();
            command = String.Format("DELETE FROM tblEmployees WHERE Name='{0}'", Name);
            SqlCommand com = new SqlCommand(command, con);
            com.BeginExecuteNonQuery();
            Thread.Sleep(2000);
            con.Close();
        }
        public static void DeleteMember(string Name)
        {
            DataTable data = MyBooks(Name);
            int MemberId = GetMemberId(Name);
            for (int i = 0; i < data.Rows.Count; i++)
            {
                int bookId = GetBookId(data.Rows[i][0].ToString());
                ReturnBook(bookId, MemberId);
            }
            con.Open();
            command = String.Format("DELETE FROM tblMembers WHERE Name='{0}'", Name);
            SqlCommand com = new SqlCommand(command, con);
            com.BeginExecuteNonQuery();
            Thread.Sleep(2000);
            con.Close();
        }
        public static int Payment()
        {
            con.Open();
            command = "SELECT SUM(Salary) AS Total FROM tblEmployees;";
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            con.Close();
            return int.Parse(table.Rows[0][0].ToString());
        }
        public static void PayAllSalaries()
        {
            int Id;
            int Pay = Payment();
            DataTable data = EmpList();
            con.Open();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                Id = int.Parse(data.Rows[i][0].ToString());
                command = String.Format("UPDATE tblEmployees SET Balance = Balance + Salary WHERE Id = '{0}'", Id);
                SqlCommand com = new SqlCommand(command, con);
                com.BeginExecuteNonQuery();
            }
            Thread.Sleep(2000);
            command = String.Format("UPDATE tblAdmin SET Budget = Budget - {0} WHERE Id = '1'", Pay);
            SqlCommand com1 = new SqlCommand(command, con);
            com1.BeginExecuteNonQuery();
            Thread.Sleep(2000);
            con.Close();
        }
        public static void AddBudget(int amount)
        {
            con.Open();
            command = String.Format("UPDATE tblAdmin SET Budget = Budget + {0} WHERE Id = '1'", amount);
            SqlCommand com1 = new SqlCommand(command, con);
            com1.BeginExecuteNonQuery();
            Thread.Sleep(2000);
            con.Close();
        }
        public static List<User> GetLateSubscriptionMembers()
        {
            List<User> MemberList = new List<User>();
            con.Open();
            command = "SELECT DISTINCT tblMembers.Name,tblMembers.Email,tblMembers.PhoneNumber, tblMembers.SubscriptionDate FROM tblMembers";
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (isLate(DateTime.Parse(table.Rows[i][3].ToString())))
                {
                    MemberList.Add(new User(table.Rows[i][0].ToString(), table.Rows[i][1].ToString(), table.Rows[i][2].ToString()));
                }
            }
            con.Close();
            return MemberList;
        }
        public static List<User> GetLateReturnMembers()
        {
            List<User> MemberList = new List<User>();
            con.Open();
            command = "SELECT DISTINCT tblMembers.Name,tblMembers.Email,tblMembers.PhoneNumber, tblLibraryManagment.EndDate FROM tblLibraryManagment INNER JOIN tblBooks ON tblLibraryManagment.BookID = tblBooks.Id INNER JOIN tblMembers ON tblLibraryManagment.MemberID = tblMembers.Id;";
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (isLate(DateTime.Parse(table.Rows[i][3].ToString())))
                {
                    MemberList.Add(new User(table.Rows[i][0].ToString(), table.Rows[i][1].ToString(), table.Rows[i][2].ToString()));
                }
            }
            con.Close();
            return MemberList;
        }
        public static int GetMemberId(string Name)
        {
            con.Open();
            command = String.Format("SELECT * from tblMembers WHERE Name='{0}'", Name);
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            con.Close();
            return int.Parse(table.Rows[0][0].ToString());
        }
        public static int GetBookId(string Name)
        {
            con.Open();
            command = String.Format("SELECT * from tblBooks WHERE BookName='{0}'", Name);
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            con.Close();
            return int.Parse(table.Rows[0][0].ToString());
        }
        public static int CountBorrowedBooks(int Id)
        {
            command = String.Format("SELECT COUNT (Id) from tblLibraryManagment WHERE MemberID='{0}'", Id);
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return int.Parse(table.Rows[0][0].ToString());
        }
        public static int CountBook(int Id)
        {
            command = String.Format("SELECT * from tblBooks WHERE Id='{0}'", Id);
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return int.Parse(table.Rows[0][5].ToString());
        }
        public static bool IsAllowedToBorrow(int BookId, int MemberId)
        {
            con.Open();
            int Count;
            if (CountBorrowedBooks(MemberId) < 5)
            {
                if (CountBook(BookId) > 0)
                {

                    command = String.Format("SELECT COUNT (Id) from tblLibraryManagment WHERE MemberID='{0}' AND BookId='{1}'", MemberId, BookId);
                    SqlDataAdapter adapter = new SqlDataAdapter(command, con);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    Count = int.Parse(table.Rows[0][0].ToString());
                    if (Count != 0)
                    {
                        con.Close();
                        return false;
                    }
                    con.Close();
                    return true;
                }
                con.Close();
                return false;
            }
            con.Close();
            return false;
        }
        public static void InsertToLibraryManagment(int BookId, int MemberId)
        {
            con.Open();
            command = String.Format("INSERT INTO tblLibraryManagment (MemberId, BookId, StartDate, EndDate) VALUES ('{0}','{1}','{2}','{3}');", MemberId, BookId, DateTime.Now.ToString(), DateTime.Now.AddDays(7).ToString());
            SqlCommand com = new SqlCommand(command, con);
            com.BeginExecuteNonQuery();
            Thread.Sleep(2000);
            con.Close();
        }
        public static DataTable MyBooks(string name)
        {
            int memberId = GetMemberId(name);
            con.Open();
            command = string.Format("SELECT tblBooks.BookName, tblBooks.Author, tblBooks.Genre, tblBooks.PrintNumber, tblBooks.Count, tblLibraryManagment.MemberID FROM tblLibraryManagment INNER JOIN tblBooks ON tblLibraryManagment.BookID = tblBooks.Id WHERE MemberID='{0}'", memberId);
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable data = new DataTable();
            adapter.Fill(data);
            con.Close();
            return data;
        }
        public static Book bookInfo(int id)
        {
            DataTable data = BookList();
            for (int i = 0; i < data.Rows.Count; i++)
                if (int.Parse(data.Rows[i][0].ToString()) == id)
                    return new Book(data.Rows[i][1].ToString(), data.Rows[i][2].ToString(), data.Rows[i][3].ToString(), data.Rows[i][4].ToString(), int.Parse(data.Rows[i][5].ToString()));
            return null;
        }
        public static void CountUpdate(int BookId)
        {
            con.Open();
            command = String.Format("UPDATE tblBooks SET Count = Count - 1 WHERE Id = '{0}'", BookId);
            SqlCommand com = new SqlCommand(command, con);
            com.BeginExecuteNonQuery();
            Thread.Sleep(2000);
            con.Close();
        }
        public static bool BorrowBook(int BookId, int MemberId)
        {
            if (IsAllowedToBorrow(BookId, MemberId))
            {
                InsertToLibraryManagment(BookId, MemberId);
                CountUpdate(BookId);
                return true;
            }
            return false;
        }
        public static void ReturnBook(int BookId, int MemberId)
        {
            bool flag = true;
            con.Open();
            command = String.Format("SELECT MemberId, EndDate, tblMembers.Balance FROM tblLibraryManagment INNER JOIN tblMembers ON tblLibraryManagment.MemberID = tblMembers.Id WHERE BookID = '{0}' AND MemberID='{1}';", BookId, MemberId);
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (isLate(DateTime.Parse(table.Rows[i][1].ToString())))
                {
                    if (int.Parse(table.Rows[i][2].ToString()) < 20000)
                    {
                        flag = false;
                    }
                }
            }
            if (flag)
            {
                command = String.Format("DELETE FROM tblLibraryManagment WHERE BookId = '{0}' AND MemberId='{1}'", BookId, MemberId);
                SqlCommand com1 = new SqlCommand(command, con);
                com1.BeginExecuteNonQuery();

                command = String.Format("UPDATE tblBooks SET Count = Count + 1 WHERE Id = '{0}'", BookId);
                SqlCommand com2 = new SqlCommand(command, con);
                com2.BeginExecuteNonQuery();
            }
            Thread.Sleep(2000);
            con.Close();
        }
        public static DataTable memberInfo(string Name)
        {
            con.Open();
            command = String.Format("SELECT * FROM tblMembers WHERE Name = '{0}'", Name);
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable data = new DataTable();
            adapter.Fill(data);
            con.Close();
            return data;
        }
        public static void AddBookCount(string BookName, int Amount)
        {
            con.Open();
            command = String.Format("UPDATE tblBooks SET Count = Count + '{0}' WHERE BookName = '{1}'", Amount, BookName);
            SqlCommand com = new SqlCommand(command, con);
            com.BeginExecuteNonQuery();
            Thread.Sleep(2000);
            con.Close();
        }
        public static void updateMember(Member member)
        {
            con.Open();
            command = String.Format("UPDATE tblMembers SET Email='{0}', PhoneNumber='{1}', Password='{2}',ImageFileName='{3}' WHERE Name = '{4}'", member.Email, member.PhoneNumber, member.Password, member.ImageFileName, member.Name);
            SqlCommand com = new SqlCommand(command, con);
            com.BeginExecuteNonQuery();
            Thread.Sleep(2000);
            con.Close();
        }
        public static void updateEmployee(Employee emp)
        {
            con.Open();
            command = String.Format("UPDATE tblEmployees SET Email='{0}', PhoneNumber='{1}', Password='{2}',ImageFileName='{3}' WHERE Name = '{4}'", emp.Email, emp.PhoneNumber, emp.Password, emp.ImageFileName, emp.Name);
            SqlCommand com = new SqlCommand(command, con);
            com.BeginExecuteNonQuery();
            Thread.Sleep(2000);
            con.Close();
        }
        public static int LibraryBudget()
        {
            command = String.Format("SELECT * from tblAdmin");
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return int.Parse(table.Rows[0][1].ToString());
        }
        public static int MemberBalance(string name)
        {
            command = String.Format("SELECT * from tblMembers WHERE Name='{0}'", name);
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return int.Parse(table.Rows[0][8].ToString());
        }
        public static int BalanceEmployee(string name)
        {
            command = String.Format("SELECT * from tblEmployees WHERE Name='{0}'", name);
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return int.Parse(table.Rows[0][7].ToString());
        }
        public static bool isLate(DateTime End)
        {
            if (DateTime.Compare(End, DateTime.Now) < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int CountRemainingDays(string name)
        {
            command = String.Format("SELECT * from tblMembers WHERE Name = '{0}'", name);
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DateTime subscriptiondate = DateTime.Parse(table.Rows[0][6].ToString());
            int numberofDays = (subscriptiondate - DateTime.Now).Days;
            con.Close();
            return numberofDays;
        }
        public static DateTime GetSubscriptionDate(string name)
        {
            command = String.Format("SELECT * from tblMembers WHERE Name = '{0}'", name);
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            DateTime subscriptiondate = DateTime.Parse(table.Rows[0][6].ToString());
            return subscriptiondate;
        }
        public static void UpdateSubscriptionWithDays(string name, int days)
        {
            DateTime Subscriptiondate = GetSubscriptionDate(name);
            con.Open();
            command = String.Format("UPDATE tblMembers SET SubscriptionDate = '{0}' WHERE Name = '{1}'", Subscriptiondate.AddDays(days).ToString(), name);
            SqlCommand com = new SqlCommand(command, con);
            com.BeginExecuteNonQuery();
            Thread.Sleep(2000);
            con.Close();
        }
        public static void UpdateMemberBalance(string name, int RemoveAmount)
        {
            con.Open();
            command = String.Format("UPDATE tblMembers SET Balance = Balance - {0} WHERE Name = '{1}'", RemoveAmount, name);
            SqlCommand com = new SqlCommand(command, con);
            com.BeginExecuteNonQuery();
            Thread.Sleep(2000);
            con.Close();
        }
        public static void AddMemberBalance(string name, int RemoveAmount)
        {
            con.Open();
            command = String.Format("UPDATE tblMembers SET Balance = Balance + {0} WHERE Name = '{1}'", RemoveAmount, name);
            SqlCommand com = new SqlCommand(command, con);
            com.BeginExecuteNonQuery();
            Thread.Sleep(2000);
            con.Close();
        }
        public static DataTable GetBorrowedBooks()
        {
            con.Open();
            command = String.Format("SELECT DISTINCT tblBooks.BookName,tblBooks.Author,tblBooks.Genre, tblBooks.PrintNumber, tblBooks.Count FROM tblLibraryManagment INNER JOIN tblBooks ON tblLibraryManagment.BookID = tblBooks.Id");
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            con.Close();
            return table;
        }
        public static DataTable GetAvailableBooks()
        {
            con.Open();
            command = String.Format("SELECT DISTINCT tblBooks.BookName,tblBooks.Author,tblBooks.Genre, tblBooks.PrintNumber, tblBooks.Count FROM tblBooks WHERE Count != '0'");
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            con.Close();
            return table;
        }
    }
}
