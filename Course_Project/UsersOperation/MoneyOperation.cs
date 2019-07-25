using Course_Project.ModelDB;
using Course_Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course_Project.Models_2
{
    class MoneyOperation
    {
        private void Withdraw(IQueryable<CurrentDB> dataCurrent)
        {
            DataBase dataBase = new DataBase();
            dataBase.ConnectionString();           
            User.MoneyCount += dataCurrent.First().CurrentMoney;
            var moneyCount = from u in dataBase.MoneyCountDBs
                         where u.Id_Account == User.Id
                         select u;
            foreach (MoneyCountDB u in moneyCount)
            { u.MoneyCount = User.MoneyCount; }
            CompletedDB completedDB = new CompletedDB
            {
                Id_Account = User.Id,
                Id_Operation = dataCurrent.First().Id_Operation,
                NameInvest = dataCurrent.First().NameInvest,
                ProfitMoney = dataCurrent.First().CurrentMoney,
                DateOpenInvest = dataCurrent.First().DateOpenInvest,
                DateCloseInvest = DateTime.Now,
                PercentProfit = dataCurrent.First().PercentProfit,
                InitialInvest = dataCurrent.First().InitialInvest
            };
            dataBase.CompletedDBs.InsertOnSubmit(completedDB);
            var deleteCurrent = from u in dataBase.CurrentDBs
                                where u.Id_Account == User.Id
                                select u;
            dataBase.CurrentDBs.DeleteOnSubmit(deleteCurrent.First());
            var deleteCurrentCourse = from u in dataBase.СurrentCourseDBs
                                      where u.Id_Account == User.Id
                                      select u;
            dataBase.СurrentCourseDBs.DeleteOnSubmit(deleteCurrentCourse.First());
            dataBase.Db.SubmitChanges();            
        }
        public decimal RefreshCount()
        {
            DataBase dataBase = new DataBase();          
            dataBase.ConnectionString();
            var dataMoneyCount = from u in dataBase.MoneyCountDBs
                             where u.Id_Account == User.Id
                             select u;
            return User.MoneyCount = dataMoneyCount.First().MoneyCount;
        }
        public void RefreshTable()
        {
            DataBase dataBase = new DataBase();
            dataBase.ConnectionString();
            var dataMoneyCount = from u in dataBase.MoneyCountDBs
                        where u.Id_Account == User.Id
                        select u;
            foreach (MoneyCountDB u in dataMoneyCount)
            { u.MoneyCount = User.MoneyCount; }
            dataBase.Db.SubmitChanges();
        }
        public void UpdateCurrentCourse()
        {
            DataBase dataBase = new DataBase();
            dataBase.ConnectionString();
            var courseDBs = from u in dataBase.СurrentCourseDBs
                              where u.Id_Account == User.Id
                              select u;
            var nameInvest = from u in dataBase.CurrentDBs
                             where u.Id_Operation == courseDBs.First().Id_Operation
                             select u.NameInvest;
            Graph graph = new Graph();
            if (courseDBs.Any() == true && nameInvest.Any() == true)
            {
                foreach (CurrentCourseDB u in courseDBs)
                {
                    u.CurrentCourse = graph.TakeData(nameInvest.First());
                }
                dataBase.Db.SubmitChanges();
            }
        }
        public void CalculatePercentProfit()
        {
            DataBase dataBase = new DataBase();
            dataBase.ConnectionString();
            var dataCurrent = from u in dataBase.CurrentDBs
                        where u.Id_Account == User.Id
                        select u;
            var dataCurrentCourse = from u in dataBase.СurrentCourseDBs
                         where u.Id_Operation == dataCurrent.First().Id_Operation
                         select u;
            if (dataCurrent.Any() != true || dataCurrentCourse.Any() != true)
                return;

            decimal percentProfit;
            decimal initialAmountUnit = dataCurrent.First().InitialInvest / dataCurrentCourse.First().InitialCourse;
            decimal newCurrentMoney = initialAmountUnit * dataCurrentCourse.First().CurrentCourse;

            if (dataCurrent.First().InitialInvest > newCurrentMoney)
            {
                percentProfit = ((newCurrentMoney / dataCurrent.First().InitialInvest) - 1) * 100;
            }
            else if (Convert.ToDecimal(dataCurrent.First().InitialInvest) < newCurrentMoney)
            {
                percentProfit = (1 - (Convert.ToDecimal(dataCurrent.First().InitialInvest) / newCurrentMoney)) * 100;
            }
            else
                percentProfit = 0;

            foreach (CurrentDB u in dataCurrent)
            {
                u.PercentProfit = percentProfit;
                u.CurrentMoney = newCurrentMoney;
            }
            dataBase.Db.SubmitChanges();          
        }
        public void InvestmentTimeIsUp()
        {
            DataBase dataBase = new DataBase();
            dataBase.ConnectionString();
            var dataCurrent = from u in dataBase.CurrentDBs
                        where u.Id_Account == User.Id
                        select u;
            if (dataCurrent.Any() != true)
                return;
            if (dataCurrent.First().DateCloseInvest <= DateTime.Now)
            {
                Withdraw(dataCurrent);
                MessageBox.Show("У одной из ваших инвестиций кончился срок вложения!");
            }
        }     
        public void WithdrawButton(TextBox textBox1)
        {
            DataBase dataBase = new DataBase();
            dataBase.ConnectionString();
            var dataCurrent = from u in dataBase.CurrentDBs
                        where u.Id_Operation == Convert.ToInt32(textBox1.Text)
                        select u;
            if (dataCurrent.Any() == true)
            {
                Withdraw(dataCurrent);
                MessageBox.Show("Деньги успешно выведены!");
            }
            else
                MessageBox.Show("Данной операции не существует!");
            textBox1.Text = "Номер операции";
        }
        public void ChangeTime(TextBox textBox2, DateTimePicker dateTimePicker1)
        {
            DataBase dataBase = new DataBase();
            dataBase.ConnectionString();
            var dataCurrent = from u in dataBase.CurrentDBs
                        where u.Id_Operation == Convert.ToInt32(textBox2.Text)
                        select u;
            if (DateTime.Parse(dateTimePicker1.Text) <= DateTime.Now)
            {
                MessageBox.Show("Неправильно введена дата!");
            }
            else if (dataCurrent.Any() == true)
            {
                foreach (CurrentDB u in dataCurrent)
                { u.DateCloseInvest = DateTime.Parse(dateTimePicker1.Text);  }
                dataBase.Db.SubmitChanges();
                MessageBox.Show("Срок вложения успешно изменен!");
            }
            else
            {
                MessageBox.Show("Номер операции введен неверно!");
            }
        }
        public void Investment(TextBox textBox1, TextBox textBox2, DateTimePicker dateTimePicker1, Form5 form5)
        {
            DataBase dataBase = new DataBase();
            dataBase.ConnectionString();

            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrWhiteSpace(textBox1.Text) ||
              string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Введите количество инвестируемых средств!");
            }
            else if (Convert.ToDecimal(textBox1.Text) > User.MoneyCount)
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
                InvestmentMethod investmentMethod = new InvestmentMethod();
                DialogResult dialogResult = MessageBox.Show(investmentMethod.InvestMethodsАnalytics(moneyInvest, dateValue, valueArray, rateInvest), "Вы точно хотите вложить деньги?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    decimal costInvest = Convert.ToDecimal(textBox1.Text);
                    DateTime dateCloseInvest = Convert.ToDateTime(dateTimePicker1.Text);
                    User.MoneyCount -= costInvest;
                    var dataMoneyCount = from u in dataBase.MoneyCountDBs
                                where u.Id_Account == User.Id
                                select u;
                    foreach (MoneyCountDB u in dataMoneyCount)
                    { u.MoneyCount = User.MoneyCount; }
                    CurrentDB currentDB = new CurrentDB
                    {
                        Id_Account = User.Id,
                        NameInvest = Form3.NameInvest,
                        CurrentMoney = costInvest,
                        DateOpenInvest = DateTime.Now,
                        DateCloseInvest = dateCloseInvest,
                        PercentProfit = 0,
                        InitialInvest = costInvest
                    };
                    dataBase.CurrentDBs.InsertOnSubmit(currentDB);
                    CurrentCourseDB currentCourseDB = new CurrentCourseDB
                    { Id_Account = User.Id, InitialCourse = Form3.CurrentCourse, CurrentCourse = Form3.CurrentCourse, Id_Operation = currentDB.Id_Operation };
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
