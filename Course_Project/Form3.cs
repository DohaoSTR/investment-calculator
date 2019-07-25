using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Data.SqlClient;
using System.Net;
using System.Collections.Generic;
using Course_Project.Models;
using Course_Project.Models_2;


namespace Course_Project
{
    public partial class Form3 : Form
    {
		public static List<double> ListCourse { get; private set; } = new List<double>();
		public static decimal CurrentCourse { get; private set; }
        public static string NameInvest { get; private set; }
        public Form3()
        {
            InitializeComponent();
        }
        private void GraphCoursePaint()
        {
            Graph graph = new Graph();
            label2.Text = "ДОЛЛАР США на " + DateTime.Now.ToShortDateString() + " : " + graph.TakeData("USD") + " рублей";
            chart1.Series["ДОЛЛАР США"].Points.DataBindXY(graph.ListTimeCourse , graph.ListCourse);

            label3.Text = "ЕВРО на " + DateTime.Now.ToShortDateString() + " : " + graph.TakeData("EUR") + " рублей";
            chart2.Series["ЕВРО"].Points.DataBindXY(graph.ListTimeCourse, graph.ListCourse);

            label4.Text = "КИТАЙСКИЙ ЮАНЬ на " + DateTime.Now.ToShortDateString() + " : " + graph.TakeData("CNY") + " рублей";
            chart3.Series["КИТАЙСКИЙ ЮАНЬ"].Points.DataBindXY(graph.ListTimeCourse, graph.ListCourse);

            label5.Text = "ИНДИЙСКАЯ РУПИЯ на " + DateTime.Now.ToShortDateString() + " : " + graph.TakeData("INR") + " рублей";
            chart4.Series["ИНДИЙСКАЯ РУПИЯ"].Points.DataBindXY(graph.ListTimeCourse, graph.ListCourse);

            label6.Text = "БИТКОИН на " + DateTime.Now.ToShortDateString() + " : " + graph.TakeData("BTC") + " рублей";
            chart5.Series["БИТКОИН"].Points.DataBindXY(graph.ListTimeCourse, graph.ListCourse);

            label7.Text = "ЭФИРИУМ на " + DateTime.Now.ToShortDateString() + " : " + graph.TakeData("ETH") + " рублей";
            chart6.Series["ЭФИРИУМ"].Points.DataBindXY(graph.ListTimeCourse, graph.ListCourse);

            label8.Text = "ЛАЙТКОИН на " + DateTime.Now.ToShortDateString() + " : " + graph.TakeData("LTC") + " рублей";
            chart7.Series["ЛАЙТКОИН"].Points.DataBindXY(graph.ListTimeCourse, graph.ListCourse);

            label9.Text = "DASH COIN на " + DateTime.Now.ToShortDateString() + " : " + graph.TakeData("DASH") + " рублей";
            chart8.Series["DASH COIN"].Points.DataBindXY(graph.ListTimeCourse, graph.ListCourse);

            label10.Text = "ЗОЛОТО на " + DateTime.Now.ToShortDateString() + " : " + graph.TakeData("XAU") + " рублей";
            chart9.Series["ЗОЛОТО"].Points.DataBindXY(graph.ListTimeCourse, graph.ListCourse);

            label11.Text = "СЕРЕБРО на " + DateTime.Now.ToShortDateString() + " : " + graph.TakeData("XAG") + " рублей";
            chart10.Series["СЕРЕБРО"].Points.DataBindXY(graph.ListTimeCourse, graph.ListCourse);

            label12.Text = "ПЛАТИНА на " + DateTime.Now.ToShortDateString() + " : " + graph.TakeData("XPT") + " рублей";
            chart11.Series["ПЛАТИНА"].Points.DataBindXY(graph.ListTimeCourse, graph.ListCourse);

            label13.Text = "ПАЛЛАДИЙ на " + DateTime.Now.ToShortDateString() + " : " + graph.TakeData("XPD") + " рублей";
            chart12.Series["ПАЛЛАДИЙ"].Points.DataBindXY(graph.ListTimeCourse, graph.ListCourse);

            label14.Text = "Apple на " + DateTime.Now.ToShortDateString() + " : " + graph.TakeData("AAPL") + " рублей";
            chart13.Series["APPLE"].Points.DataBindXY(graph.ListTimeCourse, graph.ListCourse);

            label15.Text = "Tesla на " + DateTime.Now.ToShortDateString() + " : " + graph.TakeData("Tesla") + " рублей";
            chart14.Series["TESLA"].Points.DataBindXY(graph.ListTimeCourse, graph.ListCourse);

            label16.Text = "Facebook на " + DateTime.Now.ToShortDateString() + " : " + graph.TakeData("Facebook") + " рублей";
            chart15.Series["FACEBOOK"].Points.DataBindXY(graph.ListTimeCourse, graph.ListCourse);

            label17.Text = "Toyota на " + DateTime.Now.ToShortDateString() + " : " + graph.TakeData("Toyota") + " рублей";
            chart16.Series["TOYOTA"].Points.DataBindXY(graph.ListTimeCourse, graph.ListCourse);
        }
        private void Button1_MouseClick(object sender, MouseEventArgs e)
        {
            contextMenuStrip1.Show(button1, new Point(e.X, e.Y));
        }
        private void GoToAccToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            Close();
        }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DesignForms.ButtonExit_Click();
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.ShowDialog();
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            MoneyOperation money = new MoneyOperation();
            label18.Text = "";
            label19.Text = "";
            timer1.Enabled = true;
            timer1.Interval = 1000;

            GraphCoursePaint();

            label1.Text = "Денежные средства: " + money.RefreshCount() + " рублей.";
            money.UpdateCurrentCourse();
        }
        private void Form3_Activated(object sender, EventArgs e)
        {
            MoneyOperation money = new MoneyOperation();
            label1.Text = "Денежные средства: " + money.RefreshCount() + " рублей.";
            money.UpdateCurrentCourse();
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            label18.Text = DateTime.Now.ToLongTimeString();
            label19.Text = DateTime.Now.ToShortDateString();
        }
        private void MenuSelectTab_Click(object sender, EventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "button5":
                    tabControl1.SelectTab(tabPage1);
                    break;
                case "button6":
                    tabControl1.SelectTab(tabPage2);
                    break;
                case "button7":
                    tabControl1.SelectTab(tabPage3);
                    break;
                case "button8":
                    tabControl1.SelectTab(tabPage4);
                    break;
            }
        }
        private void Button_MouseLeave(object sender, EventArgs e)
        {
            DesignForms.Button_MouseLeave(sender, e);
        }
        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            DesignForms.Button_MouseMove(sender, e);
        }
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            DesignForms.ButtonExit_Click();
        }
        private void ButtonTurn_Click(object sender, EventArgs e)
        {
            WindowState = DesignForms.ButtonTurn_Click();
        }
        private void AllInvestButton_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            Graph graph = new Graph();
            switch (((Button)sender).Name)
            {
                case "button9":
                    NameInvest = "USD";
                    graph.TakeData(NameInvest);
                    CurrentCourse = graph.CurrentCourse;
                    ListCourse.AddRange(graph.ListCourse);
                    f5.ShowDialog();
                    break;
                case "button10":
                    NameInvest = "EUR";
                    graph.TakeData(NameInvest);
                    CurrentCourse = graph.CurrentCourse;
                    ListCourse.AddRange(graph.ListCourse);
                    f5.ShowDialog();
                    break;
                case "button11":
                    NameInvest = "CNY";
                    graph.TakeData(NameInvest);
                    CurrentCourse = graph.CurrentCourse;
                    ListCourse.AddRange(graph.ListCourse);
                    f5.ShowDialog();
                    break;
                case "button12":
                    NameInvest = "INR";
                    graph.TakeData(NameInvest);
                    CurrentCourse = graph.CurrentCourse;
                    ListCourse.AddRange(graph.ListCourse);
                    f5.ShowDialog();
                    break;
                case "button13":
                    NameInvest = "BTC";
                    graph.TakeData(NameInvest);
                    CurrentCourse = graph.CurrentCourse;
                    ListCourse.AddRange(graph.ListCourse);
                    f5.ShowDialog();
                    break;
                case "button14":
                    NameInvest = "ETH";
                    graph.TakeData(NameInvest);
                    CurrentCourse = graph.CurrentCourse;
                    ListCourse.AddRange(graph.ListCourse);
                    f5.ShowDialog();
                    break;
                case "button15":
                    NameInvest = "LTC";
                    graph.TakeData(NameInvest);
                    CurrentCourse = graph.CurrentCourse;
                    ListCourse.AddRange(graph.ListCourse);
                    f5.ShowDialog();
                    break;
                case "button16":
                    NameInvest = "DASH";
                    graph.TakeData(NameInvest);
                    CurrentCourse = graph.CurrentCourse;
                    ListCourse.AddRange(graph.ListCourse);
                    f5.ShowDialog();
                    break;
                case "button17":
                    NameInvest = "XAU";
                    graph.TakeData(NameInvest);
                    CurrentCourse = graph.CurrentCourse;
                    ListCourse.AddRange(graph.ListCourse);
                    f5.ShowDialog();
                    break;
                case "button18":
                    NameInvest = "XAG";
                    graph.TakeData(NameInvest);
                    CurrentCourse = graph.CurrentCourse;
                    ListCourse.AddRange(graph.ListCourse);
                    f5.ShowDialog();
                    break;
                case "button19":
                    NameInvest = "XPT";
                    graph.TakeData(NameInvest);
                    CurrentCourse = graph.CurrentCourse;
                    ListCourse.AddRange(graph.ListCourse);
                    f5.ShowDialog();
                    break;
                case "button20":
                    NameInvest = "XPD";
                    graph.TakeData(NameInvest);
                    CurrentCourse = graph.CurrentCourse;
                    ListCourse.AddRange(graph.ListCourse);
                    f5.ShowDialog();
                    break;
                case "button21":
                    NameInvest = "AAPL";
                    graph.TakeData(NameInvest);
                    CurrentCourse = graph.CurrentCourse;
                    ListCourse.AddRange(graph.ListCourse);
                    f5.ShowDialog();
                    break;
                case "button22":
                    NameInvest = "Tesla";
                    graph.TakeData(NameInvest);
                    CurrentCourse = graph.CurrentCourse;
                    ListCourse.AddRange(graph.ListCourse);
                    f5.ShowDialog();
                    break;
                case "button23":
                    NameInvest = "Facebook";
                    graph.TakeData(NameInvest);
                    CurrentCourse = graph.CurrentCourse;
                    ListCourse.AddRange(graph.ListCourse);
                    f5.ShowDialog();
                    break;
                case "button24":
                    NameInvest = "Toyota";
                    graph.TakeData(NameInvest);
                    CurrentCourse = graph.CurrentCourse;
                    ListCourse.AddRange(graph.ListCourse);
                    f5.ShowDialog();
                    break;
            }
        }
    }
}
