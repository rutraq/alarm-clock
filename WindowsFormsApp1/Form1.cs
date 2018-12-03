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
        List<string> alarm = new List<string>();
        int numbers_of_alarms = 0;
        string lb_name, pn_name;
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            if (numbers_of_alarms == 0)
            {
                button3.Visible = true;
            }
            string minutes = Convert.ToString(numericUpDown2.Value);
            if (minutes.Length == 1)
            {
                alarm.Add(Convert.ToString(numericUpDown1.Value) + ":0" + minutes);
            }
            else
            {
                alarm.Add(Convert.ToString(numericUpDown1.Value) + ":" + minutes);
            }
            numbers_of_alarms++;
            if (numbers_of_alarms < 10)
            {
                lb_name = "label" + Convert.ToString(numbers_of_alarms + 1);
                var find_label = this.Controls.Find(lb_name, true).FirstOrDefault();
                find_label.Text = alarm[numbers_of_alarms - 1];
                pn_name = "panel" + Convert.ToString(numbers_of_alarms + 2);
                var find_panel = this.Controls.Find(pn_name, true).FirstOrDefault();
                find_panel.Visible = true;
            }
            else
            {
                MessageBox.Show("Количество будильников достигло максимального количества");
            }
        }
    }
}
