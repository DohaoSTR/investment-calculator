using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Course_Project.Models
{
    class Graph
    {
        public List<double> ListCourse { get; private set; } = new List<double>();
        public List<DateTime> ListTimeCourse { get; private set; } = new List<DateTime>();
        public decimal CurrentCourse { get; private set; }

        private void ClearLists()
        {
            ListCourse.Clear();
            ListTimeCourse.Clear();
        }
        private string TakeHtml(string linkGraph)
        {
            string htmlCode = "";
            WebClient wc = new WebClient();
            try
            {
                htmlCode = wc.DownloadString(linkGraph);
            }
            catch (WebException) { }
            return htmlCode;
        }
        private decimal Currency(string linkGraph)
        {
            ClearLists();
            foreach (Match match in Regex.Matches(TakeHtml(linkGraph), @", .(\d+), (.*?)]"))
            {
                DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(match.Groups[1].Value)).DateTime;
                ListTimeCourse.Add(dateTime);
                ListCourse.Add(Convert.ToDouble(match.Groups[2].Value.Replace(".", ",")));
                CurrentCourse = Convert.ToDecimal(match.Groups[2].Value.Replace(".", ","));
            }
            return CurrentCourse;
        }
        private decimal Crypto(string linkGraph)
        {
            ClearLists();
            foreach (Match match in Regex.Matches(TakeHtml(linkGraph), @".new Date.'(.*?)'., (.*?)]"))
            {
                DateTime dateTime = DateTime.ParseExact(match.Groups[1].Value, "yyyy-M-dd HH:mm:ss.fff", null);
                ListTimeCourse.Add(dateTime);
                ListCourse.Add(Convert.ToDouble(match.Groups[2].Value.Replace(".", ",")));
                CurrentCourse = Convert.ToDecimal(match.Groups[2].Value.Replace(".", ","));
            }
            return CurrentCourse;
        }
        private decimal Metals(string linkGraph, int numMetal)
        {
            ClearLists();
            Regex r = new Regex("date'>(.*?)<td>(.*?)<td>(.*?)<td>(.*?)<td>(.*?)<tr>");

            Match m = r.Match(TakeHtml(linkGraph));
            while (m.Success)
            {
                ListTimeCourse.Add(Convert.ToDateTime(m.Groups[1].Value));
                ListCourse.Add(Convert.ToDouble(m.Groups[numMetal].Value.Replace(".", ",")));
                m = m.NextMatch();
            }
            ListCourse.Reverse();
            ListTimeCourse.Reverse();
            CurrentCourse = Convert.ToDecimal(ListCourse[ListCourse.Count - 2]);
            return CurrentCourse;
        }
        private decimal Stock(string linkGraph)
        {
            ClearLists();
            foreach (Match match in Regex.Matches(TakeHtml(linkGraph), @";\d+.n(.*?);(.*?);"))
            {
                DateTime dateTime = new DateTime(1970, 1, 1).AddSeconds(Convert.ToInt32(match.Groups[1].Value));
                ListTimeCourse.Add(dateTime);
                CurrentCourse = Convert.ToDecimal(match.Groups[2].Value.Replace(".", ",")); // * TakeData("USD");
                ListCourse.Add(Convert.ToDouble(CurrentCourse));
            }
            return CurrentCourse;
        }
        public decimal TakeData(string nameGraph)
        {
            switch (nameGraph)
            {
                case "USD":
                    Currency("https://www.alta.ru/currency/graph_frame/?min=18&max=18");
                    break;
                case "EUR":
                    Currency("https://www.alta.ru/currency/graph_frame/?code[]=978&min=2005&max=2019");
                    break;
                case "CNY":
                    Currency("https://www.alta.ru/currency/graph_frame/?code[]=156&min=2005&max=2019");
                    break;
                case "INR":
                    Currency("https://www.alta.ru/currency/graph_frame/?code[]=356&min=2006&max=2019");
                    break;
                case "BTC":
                    Crypto("https://creditpower.ru/currency/crypto/btcrur/1year/");
                    break;
                case "ETH":
                    Crypto("https://creditpower.ru/currency/crypto/ethrur/1year/");
                    break;
                case "LTC":
                    Crypto("https://creditpower.ru/currency/crypto/ltcrur/1year/");
                    break;
                case "DASH":
                    Crypto("https://creditpower.ru/currency/crypto/dashrur/1year/");
                    break;
                case "XAU":
                    Metals("https://mfd.ru/centrobank/preciousmetals/?left=0&right=-1&from=25.11.2018&till=",2);
                    break;
                case "XAG":
                    Metals("https://mfd.ru/centrobank/preciousmetals/?left=0&right=-1&from=25.11.2018&till=", 3);
                    break;
                case "XPT":
                    Metals("https://mfd.ru/centrobank/preciousmetals/?left=0&right=-1&from=25.11.2018&till=", 4);
                    break;
                case "XPD":
                    Metals("https://mfd.ru/centrobank/preciousmetals/?left=0&right=-1&from=25.11.2018&till=", 5);
                    break;
                case "AAPL":
                    Stock("https://fortrader.org/quotes/aapl");
                    break;
                case "Tesla":
                    Stock("https://fortrader.org/quotes/tesla");
                    break;
                case "Facebook":
                    Stock("https://fortrader.org/quotes/facebook");
                    break;
                case "Toyota":
                    Stock("https://fortrader.org/quotes/toyota");
                    break;
            }
            return CurrentCourse;
        }
    }
}
