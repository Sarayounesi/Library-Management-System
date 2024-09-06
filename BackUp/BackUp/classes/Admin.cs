using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Library_Manager
{
    public class Admin : User
    {
        /// <summary>
        /// The List Of Employees
        /// </summary>
        public List<Employee> employeeList
        {
            get => default;
            set
            {
            }
        }

        public void fireEmployee()
        {
            throw new System.NotImplementedException();
        }

        public List<Book> paySalary()
        {
            throw new System.NotImplementedException();
        }
    }
}