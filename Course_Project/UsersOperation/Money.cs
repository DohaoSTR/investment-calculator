using Course_Project.ModelDB;
using Course_Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course_Project.Models_2
{
    class Money : User
    {
        public decimal RefreshCount()
        {
            DataBase dataBase = new DataBase();
            dataBase.ConnectionString();
            var moneyCount = from u in dataBase.MoneyCountDBs
                             where u.Id_Account == Id
                             select u;
            return MoneyCount = moneyCount.First().MoneyCount;
        }
        public void RefreshTable()
        {
            DataBase dataBase = new DataBase();
            dataBase.ConnectionString();
            var query = from u in dataBase.MoneyCountDBs
                        where u.Id_Account == Id
                        select u;
            foreach (MoneyCountDB u in query)
            { u.MoneyCount = MoneyCount; }
            dataBase.Db.SubmitChanges();
        }
        public void UpdateCurrentCourse()
        {
            DataBase dataBase = new DataBase();
            dataBase.ConnectionString();
            var courseDBs = from u in dataBase.СurrentCourseDBs
                              where u.Id_Account == Id
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
            var query = from u in dataBase.CurrentDBs
                        where u.Id_Account == Id
                        select u;
            var query2 = from u in dataBase.СurrentCourseDBs
                         where u.Id_Operation == query.First().Id_Operation
                         select u;
            if (query.Any() != true || query2.Any() != true)
                return;

            decimal percentProfit;
            decimal initialAmountUnit = query.First().InitialInvest / query2.First().InitialCourse;
            decimal newCurrentMoney = initialAmountUnit * query2.First().CurrentCourse;

            if (query.First().InitialInvest > newCurrentMoney)
            {
                percentProfit = ((newCurrentMoney / query.First().InitialInvest) - 1) * 100;
            }
            else if (Convert.ToDecimal(query.First().InitialInvest) < newCurrentMoney)
            {
                percentProfit = (1 - (Convert.ToDecimal(query.First().InitialInvest) / newCurrentMoney)) * 100;
            }
            else
                percentProfit = 0;

            foreach (CurrentDB u in query)
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
            var query = from u in dataBase.CurrentDBs
                        where u.Id_Account == Id
                        select u;
            if (query.Any() != true)
                return;
            if (query.First().DateCloseInvest <= DateTime.Now)
            {
                MoneyCount += query.First().CurrentMoney;
                var query2 = from u in dataBase.MoneyCountDBs
                             where u.Id_Account == Id
                             select u;
                foreach (MoneyCountDB u in query2)
                { u.Id_Account = Id; u.MoneyCount = MoneyCount; }
                CompletedDB completedDB = new CompletedDB
                {
                    Id_Account = Id,
                    Id_Operation = query.First().Id_Operation,
                    NameInvest = query.First().NameInvest,
                    ProfitMoney = query.First().CurrentMoney,
                    DateOpenInvest = query.First().DateOpenInvest,
                    DateCloseInvest = DateTime.Now,
                    PercentProfit = query.First().PercentProfit,
                    InitialInvest = query.First().InitialInvest
                };
                dataBase.CompletedDBs.InsertOnSubmit(completedDB);
                dataBase.Db.SubmitChanges();
                var deleteCurrent = from u in dataBase.CurrentDBs
                                    where u.Id_Account == Id
                                    select u;
                dataBase.CurrentDBs.DeleteOnSubmit(deleteCurrent.First());
                dataBase.Db.SubmitChanges();
                var deleteCurrentCourse = from u in dataBase.СurrentCourseDBs
                                    where u.Id_Account == Id
                                    select u;
                dataBase.СurrentCourseDBs.DeleteOnSubmit(deleteCurrentCourse.First());
                dataBase.Db.SubmitChanges();
                MessageBox.Show("У одной из ваших инвестиций кончился срок вложения!");
            }
        }
        public void Withdraw(TextBox textBox1)
        {
            DataBase dataBase = new DataBase();
            dataBase.ConnectionString();
            var query = from u in dataBase.CurrentDBs
                        where u.Id_Operation == Convert.ToInt32(textBox1.Text)
                        select u;
            if (query.Any() == true)
            {
                MoneyCount += query.First().CurrentMoney;
                var query2 = from u in dataBase.MoneyCountDBs
                             where u.Id_Account == Id
                             select u;
                foreach (MoneyCountDB u in query2)
                { u.MoneyCount = MoneyCount; }
                CompletedDB completedDB = new CompletedDB
                {
                    Id_Account = Id,
                    Id_Operation = query.First().Id_Operation,
                    NameInvest = query.First().NameInvest,
                    ProfitMoney = query.First().CurrentMoney,
                    DateOpenInvest = query.First().DateOpenInvest,
                    DateCloseInvest = DateTime.Now,
                    PercentProfit = query.First().PercentProfit,
                    InitialInvest = query.First().InitialInvest
                };
                dataBase.CompletedDBs.InsertOnSubmit(completedDB);
                dataBase.Db.SubmitChanges();
                var deleteCurrent = from u in dataBase.CurrentDBs
                                    where u.Id_Account == Id
                                    select u;
                dataBase.CurrentDBs.DeleteOnSubmit(deleteCurrent.First());
                dataBase.Db.SubmitChanges();
                var deleteCurrentCourse = from u in dataBase.СurrentCourseDBs
                                          where u.Id_Account == Id
                                          select u;
                dataBase.СurrentCourseDBs.DeleteOnSubmit(deleteCurrentCourse.First());
                dataBase.Db.SubmitChanges();
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
            var query = from u in dataBase.CurrentDBs
                        where u.Id_Operation == Convert.ToInt32(textBox2.Text)
                        select u;
            if (DateTime.Parse(dateTimePicker1.Text) <= DateTime.Now)
            {
                MessageBox.Show("Неправильно введена дата!");
            }
            else if (query.Any() == true)
            {
                foreach (CurrentDB u in query)
                { u.DateCloseInvest = DateTime.Parse(dateTimePicker1.Text);  }
                dataBase.Db.SubmitChanges();
                MessageBox.Show("Срок вложения успешно изменен!");
            }
            else
            {
                MessageBox.Show("Номер операции введен неверно!");
            }
        }
    }
}
