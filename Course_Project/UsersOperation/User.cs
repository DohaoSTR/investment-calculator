using Course_Project.Models;
using System;
using System.Linq;
using System.Windows.Forms;
using Course_Project.ModelDB;

namespace Course_Project.Models_2
{
    class User
    {
        public static int Id { get; private set; }
        public static decimal MoneyCount { get; protected set; }
        public void Registration(TextBox textBox1, TextBox textBox2, TextBox textBox3, TextBox textBox4, Form2 form2)
        {
            DataBase dataBase = new DataBase();
            dataBase.ConnectionString();
            var reLoginCheck = from u in dataBase.LogPassDBs
                               where u.Login == textBox1.Text
                               select new { reLogin = true };
            if (textBox1.Text.Length < 3)
                MessageBox.Show("Логин должен быть длинной больше трех символов!");
            else if (textBox2.Text.Length < 5 && textBox3.Text.Length < 5)
                MessageBox.Show("Пароль должен быть длинной больше пяти символов!");
            else if (textBox2.Text != textBox3.Text)
                MessageBox.Show("Пароли не совпадают!");
            else if (reLoginCheck.Any() == true)
                MessageBox.Show("Такой логин уже существует!");
            else if (string.IsNullOrEmpty(textBox4.Text) == true)
                MessageBox.Show("Введите количество денежных средств!");
            else
            {
                LogPassDB logPassDB = new LogPassDB { Login = textBox1.Text, Password = textBox2.Text };
                dataBase.LogPassDBs.InsertOnSubmit(logPassDB);
                dataBase.Db.SubmitChanges();
                Id = logPassDB.Id_Account;

                MoneyCountDB moneyCountDB = new MoneyCountDB { Id_Account = Id, MoneyCount = Convert.ToDecimal(textBox4.Text) };
                dataBase.MoneyCountDBs.InsertOnSubmit(moneyCountDB);
                dataBase.Db.SubmitChanges();

                MessageBox.Show("Регистрация успешна");
                Form1 f1 = new Form1();
                form2.Close();
                f1.Show();
            }
        }
        public void Authorization(TextBox textBox1, TextBox textBox2, Form1 form1)
        {
            DataBase dataBase = new DataBase();
            dataBase.ConnectionString();
            var authorization = from u in dataBase.LogPassDBs
                                where u.Login == textBox1.Text && u.Password == textBox2.Text
                                select u;
            if (authorization.Any() != true)
                MessageBox.Show("Данные введены неверно!");
            else
            {
                Id = authorization.First().Id_Account;
                Form3 f3 = new Form3();
                form1.Hide();
                f3.Show();
            }
        }
    }
}
