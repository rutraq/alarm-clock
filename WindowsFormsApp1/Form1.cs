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
        string lb_name, check_name;
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
            List<string> alarms_for_delete = new List<string>();
            int number_for_delete = 0;
            foreach (var ch in Controls.OfType<CheckBox>())
            {
                if (ch.Checked)
                {
                    number_for_delete++;
                    for (int i = 0; i < alarm.Count; i++)
                    {
                        if (alarm[Convert.ToInt32(Convert.ToString(ch.Name[8])) - number_for_delete] == alarm[i])
                        {
                            alarm.RemoveAt(i);
                        }
                    }
                    alarms_for_delete.Add(ch.Name);
                }
            }
            if (number_for_delete != 0)
            {
                foreach (var ch in Controls.OfType<CheckBox>())
                {
                    if (ch.Checked)
                    {
                        foreach (var lb in Controls.OfType<Label>())
                        {
                            if (Convert.ToInt32(Convert.ToString(lb.Name[5])) > 0)
                            {
                                lb.Visible = false;
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
                for (int i = 0; i < alarm.Count; i++)
                {
                    foreach (var lb in Controls.OfType<Label>())
                    {
                        if (Convert.ToInt32(Convert.ToString(lb.Name[5])) == (i + 2))
                        {
                            lb.Text = alarm[i];
                            lb.Visible = true;
                        }
                    }
                    foreach (var ch in Controls.OfType<CheckBox>())
                    {
                        if (Convert.ToInt32(Convert.ToString(ch.Name[8])) == (i + 1))
                        {
                            ch.Visible = true;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Не выбран ни один будильник для удаления");
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
                find_label.Visible = true;
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
