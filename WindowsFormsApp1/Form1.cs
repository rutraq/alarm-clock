using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Speech.Recognition;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        static List<string> alarm = new List<string>();
        static int numbers_of_alarms = 0;
        static string lb_name, check_name, last_alarm;
        int a, b, otv, number_of_questions = 0, yours_otv;
        Random rnd = new Random();
        static Label l;
        static Panel p;
        Choices comands = new Choices();
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        public Form1()
        {
            InitializeComponent();
            if (Convert.ToString(DateTime.Now.Minute).Length == 1)
            {
                panel3.Controls[0].Text = Convert.ToString(DateTime.Now.Hour) + ":0" + Convert.ToString(DateTime.Now.Minute);
            }
            else
            {
                panel3.Controls[0].Text = Convert.ToString(DateTime.Now.Hour) + ":" + Convert.ToString(DateTime.Now.Minute);
            }
            for (int i = 0; i < 24; i++)
            {
                for (int j = 0; j < 60; j++)
                {
                    if (i < 10)
                    {
                        if (j < 10)
                        {
                            comands.Add("Поставь будильник на 0" + Convert.ToString(i) + " 0" + Convert.ToString(j));
                        }
                        else
                        {
                            comands.Add("Поставь будильник на 0" + Convert.ToString(i) + " " + Convert.ToString(j));
                        }
                    }
                    else
                    {
                        if (j < 10)
                        {
                            comands.Add("Поставь будильник на " + Convert.ToString(i) + " 0" + Convert.ToString(j));
                        }
                        else
                        {
                            comands.Add("Поставь будильник на " + Convert.ToString(i) + " " + Convert.ToString(j));
                        }
                    }
                }
            }
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ru-Ru");
            sre = new SpeechRecognitionEngine(ci);

            sre.SetInputToDefaultAudioDevice();

            sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);

            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(comands);

            Grammar g = new Grammar(gb);
            sre.LoadGrammar(g);
        }
        static void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Confidence > 0.42)
            {
                Form1 f = new Form1();
                MessageBox.Show(e.Result.Text);
                l.Visible = false;
                p.Visible = false;
                int length = e.Result.Text.Length;
                alarm.Add(Convert.ToString(e.Result.Text[length - 5]) + Convert.ToString(e.Result.Text[length - 4]) + ":" + Convert.ToString(e.Result.Text[length - 2]) + Convert.ToString(e.Result.Text[length - 1]));
                if (numbers_of_alarms == 0)
                {
                    f.button3.Visible = true;
                }
                //if (f.comands)
                //string minutes = Convert.ToString(f.numericUpDown2.Value);
                //if (minutes.Length == 1)
                //{
                //    f.alarm.Add(Convert.ToString(numericUpDown1.Value) + ":0" + minutes);
                //}
                //else
                //{
                //    alarm.Add(Convert.ToString(numericUpDown1.Value) + ":" + minutes);
                //}
                numbers_of_alarms++;
                if (numbers_of_alarms < 10)
                {
                    lb_name = "label" + Convert.ToString(numbers_of_alarms + 1);
                    var find_label = f.Controls.Find(lb_name, true).FirstOrDefault();
                    find_label.Text = alarm[numbers_of_alarms - 1];
                    find_label.Visible = true;
                    check_name = "checkBox" + Convert.ToString(numbers_of_alarms);
                    var find_check = f.Controls.Find(check_name, true).FirstOrDefault();
                    find_check.Visible = true;
                }
                else
                {
                    MessageBox.Show("Количество будильников достигло максимального количества");
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            p = panel2;
            l = label13;
            label13.Visible = true;
            sre.RecognizeAsync(RecognizeMode.Single);
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
                numbers_of_alarms -= number_for_delete;
            }
            else
            {
                MessageBox.Show("Не выбран ни один будильник для удаления");
            }
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (richTextBox1.Text.Length != 0))
            {
                yours_otv = Convert.ToInt32(richTextBox1.Text);
                richTextBox1.Clear();
                if (yours_otv != otv)
                {
                    if (number_of_questions == 1)
                    {
                        MessageBox.Show("Не верный ответ");
                        a = rnd.Next(-20, 20);
                        b = rnd.Next(-20, 20);
                        otv = a + b;
                        panel4.Controls[1].Text = Convert.ToString(a) + " + " + Convert.ToString(b);
                    }
                    else if (number_of_questions == 2)
                    {
                        MessageBox.Show("Не верный ответ");
                        a = rnd.Next(-20, 20);
                        b = rnd.Next(-20, 20);
                        otv = a - b;
                        panel4.Controls[1].Text = Convert.ToString(a) + " - " + Convert.ToString(b);
                    }
                    else
                    {
                        MessageBox.Show("Не верный ответ");
                        a = rnd.Next(-10, 10);
                        b = rnd.Next(-10, 10);
                        otv = a * b;
                        panel4.Controls[1].Text = Convert.ToString(a) + " * " + Convert.ToString(b);
                    }
                }
                else
                {
                    if (number_of_questions == 1)
                    {
                        a = rnd.Next(-20, 20);
                        b = rnd.Next(-20, 20);
                        otv = a - b;
                        panel4.Controls[1].Text = Convert.ToString(a) + " - " + Convert.ToString(b);
                        number_of_questions++;
                    }
                    else if (number_of_questions == 2)
                    {
                        a = rnd.Next(-10, 10);
                        b = rnd.Next(-10, 10);
                        otv = a * b;
                        panel4.Controls[1].Text = Convert.ToString(a) + " * " + Convert.ToString(b);
                        number_of_questions++;
                    }
                    else
                    {
                        (new System.Media.SoundPlayer(@"j.wav")).Stop();
                        panel4.Visible = false;
                        number_of_questions = 0;
                        timer1.Enabled = true;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string check_hours, check_minutes;
            if (Convert.ToString(DateTime.Now.Minute).Length == 1)
            {
                check_hours = Convert.ToString(DateTime.Now.Hour);
                check_minutes = "0" + Convert.ToString(DateTime.Now.Minute);
                panel3.Controls[0].Text = check_hours + ":" + check_minutes; 
            }
            else
            {
                check_hours = Convert.ToString(DateTime.Now.Hour);
                check_minutes = Convert.ToString(DateTime.Now.Minute);
                panel3.Controls[0].Text = check_hours + ":" + check_minutes;
            }
            foreach (String time in alarm)
            {
                if (time.Length == 5)
                {
                    string hours = Convert.ToString(time[0]) + Convert.ToString(time[1]);
                    string minutes = Convert.ToString(time[3]) + Convert.ToString(time[4]);
                    if ((hours == check_hours) && (minutes == check_minutes) && (time != last_alarm))
                    {
                        (new System.Media.SoundPlayer(@"j.wav")).PlayLooping();
                        a = rnd.Next(-20, 20);
                        b = rnd.Next(-20, 20);
                        otv = a + b;
                        number_of_questions++;
                        panel4.Controls[1].Text = Convert.ToString(a) + " + " + Convert.ToString(b);
                        panel4.Visible = true;
                        last_alarm = time;
                        timer1.Enabled = false;
                    }
                }
                else
                {
                    string hours = Convert.ToString(time[0]);
                    string minutes = Convert.ToString(time[2]) + Convert.ToString(time[3]);
                    if ((hours == check_hours) && (minutes == check_minutes) && (time != last_alarm))
                    {
                        (new System.Media.SoundPlayer(@"j.wav")).PlayLooping();
                        a = rnd.Next(-20, 20);
                        b = rnd.Next(-20, 20);
                        otv = a + b;
                        number_of_questions++;
                        panel4.Controls[1].Text = Convert.ToString(a) + " + " + Convert.ToString(b);
                        panel4.Visible = true;
                        last_alarm = time;
                        timer1.Enabled = false;
                    }
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
