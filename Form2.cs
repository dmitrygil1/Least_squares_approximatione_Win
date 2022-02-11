using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void открытьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                openFileDialog1.ShowDialog();
                FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);

                string str = sr.ReadLine();
                while (str != null)
                {
                    listBox1.Items.Add(str);
                    str = sr.ReadLine();
                }

                sr.Close();
                fs.Close();
                button2.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Вы не выбрали файл, попробуйте еще раз", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private double summa(double[] data)
        {
            double rez = 0;
            for (int i = 0; i < data.Length; i++)
                rez += data[i];
            return rez;
        }
        public string[] lb;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                double[] temperature = new double[listBox1.Items.Count];
                double[] pressure = new double[listBox1.Items.Count];

                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    string stroka = Convert.ToString(listBox1.Items[i]);
                    string[] str_mas = stroka.Split(' ');
                    
                        temperature[i] = double.Parse(str_mas[0]);
                        pressure[i] = double.Parse(str_mas[1]);
                    
                }

                double[] tp = new double[temperature.Length];
                double[] tt = new double[temperature.Length];

                for (int i = 0; i < tp.Length; i++)
                    tp[i] = temperature[i] * pressure[i];

                for (int i = 0; i < tt.Length; i++)
                    tt[i] = Math.Pow(temperature[i], 2);

                double k = (summa(temperature) * summa(pressure) - temperature.Length * summa(tp)) / (Math.Pow(summa(temperature), 2) - temperature.Length * summa(tt));
                double b = (summa(temperature) * summa(tp) - summa(tt) * summa(pressure)) / (Math.Pow(summa(temperature), 2) - temperature.Length * summa(tt));

                listBox2.Items.Add(k);
                listBox3.Items.Add(b);

                int point_summary = listBox1.Items.Count;
                lb = new string[listBox1.Items.Count];
                for (int i = 0; i < listBox1.Items.Count; i++)
                    lb[i] = Convert.ToString(listBox1.Items[i]);

                listBox1.Items.Clear();

                if (checkBox1.Checked == true)
                {
                    for (int i = 0; i < point_summary; i++)
                    {
                        double y = k * temperature[i] + b;
                      
                        double osh = Math.Abs(pressure[i] - y);
                        string tab = string.Format("{0:f2}\t{1:f2}\t{2:f4}\t{3:f4}", temperature[i], pressure[i], y, osh);
                        listBox1.Items.Add(tab);
                    }
                }
                else
                    for (int i = 0; i < point_summary; i++)
                    {
                        double y = k * temperature[i] + b;
                        
                        double osh = Math.Abs(pressure[i] - y);
                        string tab = string.Format("{0:f2}\t{1:f2}\t{2:f4}", temperature[i], pressure[i], y);
                        listBox1.Items.Add(tab);
                    }
                button1.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Невозможно обработать данные - выбран неверный файл, либо формат введенных с клавиатуры данных не соответствует требуемым.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 chart = new Form3();
            chart.Owner = this;
            chart.ShowDialog();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 screensaver = new Form1();
            screensaver.ShowDialog();
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Для начала работы с программой необходимо открыть текстовый документ при помощи меню «Старт», либо ввести данные вручную, используя соответствующий пункт меню.\n\nПо открытии документа или введению информации, привести расчет данных в действие, нажав кнопку «Выполнить».\n\nРезультат работы программы можно посмотреть на графике, нажав соответствующую кнопку, а также сохранить в текстовый файл, выбрав подпункт «Сохранить файл» в меню «Старт».", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void сохранитьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.ShowDialog();
                FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);

                for (int i = 0; i < lb.Length; i++)
                    sw.WriteLine(listBox1.Items[i]);

                sw.Close();
                fs.Close();
            }
            catch
            {
                MessageBox.Show("Файл не сохранен", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string tab = string.Format("{0} {1}", textBox1.Text, textBox2.Text);
            listBox1.Items.Add(tab);
            textBox1.Clear();
            textBox2.Clear();
        }

        private void ввестиВручнуюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вводите значения температуры и давления в строки t и p соответственно. Подтверждайте свой выбор нажатием одноименной кнопки.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            button3.Enabled = true;
            textBox1.ReadOnly = false;
            textBox2.ReadOnly = false;
            button2.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            textBox1.Clear();
            textBox2.Clear();
        } 
    }
}
