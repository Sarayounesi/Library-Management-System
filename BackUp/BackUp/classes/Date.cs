using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library_Manager
{
    public struct Date
    {
        int Year;
        int Month;
        int Day;
        public Date(int year, int month, int day)
        {
            Month = month;
            Year = year;
            Day = day;
        }

        public static DateTime DateToDateTime(Date DateToConvert)
        {
            return new DateTime(DateToConvert.Year, DateToConvert.Month, DateToConvert.Day);
        }

        public static Date DateTimeToDate(DateTime DateTimeToConvert)
        {
            string sDate = DateTimeToConvert.ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            int dy = int.Parse(datevalue.Day.ToString());
            int mn = int.Parse(datevalue.Month.ToString());
            int yy = int.Parse(datevalue.Year.ToString());
            Date CurrentDate = new Date(yy, mn, dy);
            return CurrentDate;
        }

        public static Date GetCurrentDate()
        {
            string sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            int dy = int.Parse(datevalue.Day.ToString());
            int mn = int.Parse(datevalue.Month.ToString());
            int yy = int.Parse(datevalue.Year.ToString());
            Date CurrentDate = new Date(yy, mn, dy);
            return CurrentDate;
        }

        public static Date AddDays(Date StartDate, int days)
        {
            DateTime StartDateTime = DateToDateTime(StartDate);
            DateTime EndDateTime = StartDateTime.AddDays(days);
            return DateTimeToDate(EndDateTime);
        }
    }
}