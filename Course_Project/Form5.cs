using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;
using Course_Project.Models;
using Course_Project.Models_2;

namespace Course_Project
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            MoneyOperation moneyOperation = new MoneyOperation();
            moneyOperation.Investment(textBox1, textBox2, dateTimePicker1 , this);
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
        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
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