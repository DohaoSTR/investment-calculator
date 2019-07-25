using System;
using System.Drawing;
using System.Windows.Forms;
using Course_Project.Models;
using Course_Project.Models_2;

namespace Course_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.Authorization(textBox1,textBox2, this);
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            Hide();
            f2.Show();
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
        private void MoveForm_MouseDown(object sender, MouseEventArgs e)
        {
            Capture = false;
            Message m = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            WndProc(ref m);
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
            textBox2.PasswordChar = '•';

            textBox2.ForeColor = Color.FromArgb(78, 184, 206);
            textBox1.ForeColor = Color.WhiteSmoke;
        }
    }
}
