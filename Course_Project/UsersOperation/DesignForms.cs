using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course_Project.Models
{
    class DesignForms
    {
        public static void Button_MouseMove(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            button.ForeColor = Color.FromArgb(78, 184, 206);
        }
        public static void Button_MouseLeave(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.ForeColor = Color.White;
        }
        public static void ButtonExit_Click()
        {
            Application.Exit();
        }
        public static FormWindowState ButtonTurn_Click()
        {
            return FormWindowState.Minimized;
        }
    }
}
