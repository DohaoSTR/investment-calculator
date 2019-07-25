using Course_Project.Models_2;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Course_Project.ModelDB;

namespace Course_Project.Models
{
    class Investment : User
    {
        public void Pattern(TextBox textBox1, TextBox textBox2, DateTimePicker dateTimePicker1, Form5 form5)
        {
            DataBase dataBase = new DataBase();
            dataBase.ConnectionString();

            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrWhiteSpace(textBox1.Text) ||
              string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Введите количество инвестируемых средств!");
            }
            else if (Convert.ToDecimal(textBox1.Text) > MoneyCount)
            {
                MessageBox.Show("У вас нет такого количества денег!");
            }
            else if (Convert.ToDateTime(dateTimePicker1.Text) <= DateTime.Now)
            {
                MessageBox.Show("Дата введена неверно!");
            }
            else
            {
                double moneyInvest = Convert.ToDouble(textBox1.Text);
                DateTime dateInvest = DateTime.Parse(dateTimePicker1.Text);
                int dateValue = Convert.ToInt32((dateInvest - DateTime.Now).TotalDays);
                double rateInvest = Convert.ToDouble(textBox2.Text) / 100;

                double[] valueArray = new double[dateValue];

                int j = 0;
                for (int i = Form3.ListCourse.Count - dateValue; i <= Form3.ListCourse.Count - 1; i++)
                {
                    j += 1;
                    double profit = moneyInvest * Form3.ListCourse[i] / Convert.ToDouble(Form3.CurrentCourse);

                    if (profit > moneyInvest)
                    {
                        valueArray[j - 1] = profit - moneyInvest;
                    }
                    else if (profit < moneyInvest)
                    {
                        valueArray[j - 1] = moneyInvest - profit;
                    }
                    else
                        valueArray[j - 1] = 0;
                }
                DialogResult dialogResult = MessageBox.Show(InvestmentMethod.InvestMethodsАnalytics(moneyInvest, dateValue, valueArray, rateInvest), "Вы точно хотите вложить деньги?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    decimal costInvest = Convert.ToDecimal(textBox1.Text);
                    DateTime dateCloseInvest = Convert.ToDateTime(dateTimePicker1.Text);
                    MoneyCount -= costInvest;
                    var query = from u in dataBase.MoneyCountDBs
                                where u.Id_Account == Id
                                select u;
                    foreach (MoneyCountDB u in query)
                    { u.MoneyCount = MoneyCount; }
                    CurrentDB currentDB = new CurrentDB
                    {
                        Id_Account = Id,
                        NameInvest = Form3.NameInvest,
                        CurrentMoney = costInvest,
                        DateOpenInvest = DateTime.Now,
                        DateCloseInvest = dateCloseInvest,
                        PercentProfit = 0,
                        InitialInvest = costInvest
                    };                 
                    dataBase.CurrentDBs.InsertOnSubmit(currentDB);
                    CurrentCourseDB currentCourseDB = new CurrentCourseDB
                    { Id_Account = Id, InitialCourse = Form3.CurrentCourse, CurrentCourse = Form3.CurrentCourse, Id_Operation = currentDB.Id_Operation};
                    dataBase.СurrentCourseDBs.InsertOnSubmit(currentCourseDB);
                    dataBase.Db.SubmitChanges();
                    MessageBox.Show("Инвестиция успешна");
                    form5.Close();
                }
                else if (dialogResult == DialogResult.No)
                {
                    textBox1.Text = "Количество инвестируемых рублей";
                    textBox2.Text = "Процент желаемой прибыли";
                    dateTimePicker1.Text = DateTime.Now.ToShortDateString();
                    textBox1.ForeColor = Color.WhiteSmoke;
                    textBox2.ForeColor = Color.WhiteSmoke;
                }
            }
        }
    }
}
