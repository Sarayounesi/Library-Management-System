using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace Library_Manager
{
    public class DataBaseManager
    {
        static SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=E:\University\Term 4\AP\Programming\Project\Library-project\Library-project\Library Manager\DataBase\LibraryDataBase.mdf;Integrated Security = True; Connect Timeout = 30");
        static SqlConnection con2 = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=E:\University\Term 4\AP\Programming\Project\Library-project\Library-project\Library Manager\DataBase\LibraryDataBase.mdf;Integrated Security = True; Connect Timeout = 30");
        static string command;
        static DataBaseManager()
        {
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
            command = String.Format("INSERT INTO tblMembers (Name, Email, PhoneNumber, Password, SignDate, SubscriptionDate, ImageFileName) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}', '{6}');", MemberToAdd.Name, MemberToAdd.Email, MemberToAdd.PhoneNumber, MemberToAdd.Password, Date.DateToDateTime(MemberToAdd.SignDate).ToString(), Date.DateToDateTime(MemberToAdd.ExtensionDate).ToString(), MemberToAdd.ImageFileName);
            SqlCommand com = new SqlCommand(command, con);
            com.BeginExecuteNonQuery();
            Thread.Sleep(1000);
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
            Thread.Sleep(1000);
            con.Close();
        }
        public static void AddEmployee(Employee EmployeeToAdd)
        {
            con.Open();
            command = String.Format("INSERT INTO tblEmployees (Name, Email, PhoneNumber, Password, Salary, ImageFileName, Balance) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}');", EmployeeToAdd.Name, EmployeeToAdd.Email, EmployeeToAdd.PhoneNumber, EmployeeToAdd.Password, EmployeeToAdd.Salary, EmployeeToAdd.ImageFileName, EmployeeToAdd.Balance);
            SqlCommand com = new SqlCommand(command, con);
            com.BeginExecuteNonQuery();
            Thread.Sleep(1000);
            con.Close();
        }
        public static void DeleteEmployee(string Name)
        {
            con.Open();
            command = String.Format("DELETE FROM tblBooks WHERE BookName='{0}'", Name);
            SqlCommand com = new SqlCommand(command, con);
            com.BeginExecuteNonQuery();
            Thread.Sleep(1000);
            con.Close();
        }
        public static int Payment()
        {
            con.Open();
            command = "SELECT SUM(Salary) FROM tblEmployees;";
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            con.Close();
            return int.Parse(table.Rows[0][0].ToString());
        }
        public static void PayAllSalaries()
        {
            int Salary;
            int Balance;
            int Id;
            DataTable data = EmpList();
            con.Open();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                Salary = int.Parse(data.Rows[i][5].ToString());
                Balance = int.Parse(data.Rows[i][7].ToString());
                Id = int.Parse(data.Rows[i][0].ToString());
                Balance += Salary;
                command = String.Format("UPDATE tblEmployees SET Balance = '{0}' WHERE Id = '{1}'", Balance, Id);
                SqlCommand com = new SqlCommand(command, con);
                com.BeginExecuteNonQuery();
            }
            Thread.Sleep(1000);
            con.Close();
        }
        public static List<Member> GetLateReturnMembers()
        {
            List<Member> MemberList = new List<Member>();
            con.Open();
            command = "SELECT * from tblLibraryManager";
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            /// Late Ha ro berizim to Memberlist
            con.Close();
            return MemberList;
        }
        public static int GetMemberId(string Name)
        {
            con.Open();
            command = String.Format("SELECT * from tblMembers WHERE Name='{0}'",Name);
            SqlDataAdapter adapter = new SqlDataAdapter(command, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            con.Close();
            return int.Parse(table.Rows[0][0].ToString());
        }
        public static int GetBookId(string Name)
        {
            con.Open();
            command = String.Format("SELECT * from tblBooks WHERE Name='{0}'", Name);
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
            command = String.Format("INSERT INTO tblLibraryManagment (MemberId, BookId) VALUES ('{0}','{1}');", MemberId, BookId);
            SqlCommand com = new SqlCommand(command, con);
            com.BeginExecuteNonQuery();
            con.Close();
        }
        public static void CountUpdate(int BookId)
        {
            con2.Open();
            //int newCount = CountBook(BookId) - 1;
            //Thread.Sleep(1000);
            command = String.Format("UPDATE tblBooks SET Count = Count - 1 WHERE Id = '{0}'", BookId);
            SqlCommand com = new SqlCommand(command, con2);
            com.BeginExecuteNonQuery();
            Thread.Sleep(1000);
            con2.Close();
        }
        public static bool BorrowBook(int BookId, int MemberId)
        {
            if (IsAllowedToBorrow(BookId,MemberId))
            {
                InsertToLibraryManagment(BookId, MemberId);
                CountUpdate(BookId);
                return true;
            }
            return false;
        }
    }
}
