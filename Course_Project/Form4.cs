using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using Course_Project.Models;
using Course_Project.Models_2;
using System.Linq;
using System.Collections.Generic;

namespace Course_Project
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        private void InitTables()
        {
            DataBase dataBase = new DataBase();
            dataBase.ConnectionString();
            dataGridView1.DataSource = dataBase.CurrentDBs.Where(x => x.Id_Account == User.Id).Select(x => x);
            dataGridView2.DataSource = dataBase.CompletedDBs.Where(x => x.Id_Account == User.Id).Select(x => x);
        }                 
        private void Form4_Load(object sender, EventArgs e)
        {
            MoneyOperation money = new MoneyOperation();
            money.CalculatePercentProfit();
            money.RefreshTable();
            money.InvestmentTimeIsUp();
            InitTables();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            MoneyOperation money = new MoneyOperation();
            money.WithdrawButton(textBox1);
            InitTables();

        }
        private void Button2_Click(object sender, EventArgs e)
        {
            MoneyOperation money = new MoneyOperation();
            money.ChangeTime(textBox2, dateTimePicker1);
            textBox2.Text = "Номер операции";
            dateTimePicker1.Value = DateTime.Now;
            InitTables();
        }
        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void TextBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();

            textBox1.ForeColor = Color.FromArgb(78, 184, 206);
            textBox2.ForeColor = Color.WhiteSmoke;
        }
        private void TextBox2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();

            textBox2.ForeColor = Color.FromArgb(78, 184, 206);
            textBox1.ForeColor = Color.WhiteSmoke;
        }    
        private void Button_MouseLeave(object sender, EventArgs e)
        {
            DesignForms.Button_MouseLeave(sender, e);
        }
        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            DesignForms.Button_MouseMove(sender, e);
        }
        private void ButtonTurn_Click(object sender, EventArgs e)
        {
            WindowState = DesignForms.ButtonTurn_Click();
        }
        private void MoveForm_MouseDown(object sender, MouseEventArgs e)
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }
    }    
}
