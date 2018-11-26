using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            Panel pn = new Panel();
            pn.Width = 694;
            pn.Height = 469;
            pn.Top = 0;
            pn.Left = 0;
            Label lb = new Label();
            Font fn = new Font("Times New Roman", 14);
            lb.Font = fn;
            string minutes = Convert.ToString(numericUpDown2.Value);
            if (minutes.Length == 1)
            {
                lb.Text = Convert.ToString(numericUpDown1.Value) + ":0" + minutes;
            }
            else
            {
                lb.Text = Convert.ToString(numericUpDown1.Value) + ":" + minutes;
            }
            int lf = 70;
            lb.Left = lf;
            lb.Top = 20;
            lb.Width = 60;
            pn.Controls.Add(lb);
            CheckBox check = new CheckBox();
            lf += lb.Width;
            check.Left = lf;
            check.Top = 20;
            pn.Controls.Add(check);
            this.Controls.Add(pn);
        }
    }
}
