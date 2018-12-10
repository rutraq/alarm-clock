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
        string lb_name, pn_name, check_name;
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int number_for_delete = 0;
            foreach (var ch in Controls.OfType<CheckBox>())
            {
                if (ch.Checked)
                {
                    foreach (var pn in Controls.OfType<Panel>())
                    {
                        if ((pn.Name == "panel3") || (pn.Name == "panel4") || (pn.Name == "panel5") || (pn.Name == "panel7") || (pn.Name == "panel8") || (pn.Name == "panel9") || (pn.Name == "panel10") || (pn.Name == "panel11"))
                        {
                            pn.Visible = false; 
                        }
                    }
                    foreach (var che in Controls.OfType<CheckBox>())
                    {
                        che.Checked = false;
                        che.Visible = false;
                    }
                    break;
                }
            }
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
                check_name = "checkBox" + Convert.ToString(numbers_of_alarms);
                var find_check = this.Controls.Find(check_name, true).FirstOrDefault();
                find_check.Visible = true;
            }
            else
            {
                MessageBox.Show("Количество будильников достигло максимального количества");
            }
        }
    }
}
