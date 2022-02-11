using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private double summa(double[] data)
        {
            double rez = 0;
            for (int i = 0; i < data.Length; i++)
                rez += data[i];
            return rez;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Form2 main = this.Owner as Form2;
            double[] temperature = new double[main.lb.Length];
            double[] pressure = new double[main.lb.Length];

            for (int i = 0; i < temperature.Length; i++)
            {
                string[] st = main.lb[i].Split(' ');
                temperature[i] = Double.Parse(st[0]);
                pressure[i] = Double.Parse(st[1]);
            }
            for (int i = 0; i < temperature.Length; i++)
                chart1.Series[0].Points.AddXY(temperature[i], pressure[i]);

            double[] tp = new double[temperature.Length];
            double[] tt = new double[temperature.Length];

            for (int i = 0; i < tp.Length; i++)
                tp[i] = temperature[i] * pressure[i];

            for (int i = 0; i < tt.Length; i++)
                tt[i] = Math.Pow(temperature[i], 2);

            double k = (summa(temperature) * summa(pressure) - temperature.Length * summa(tp)) / (Math.Pow(summa(temperature), 2) - temperature.Length * summa(tt));
            double b = (summa(temperature) * summa(tp) - summa(tt) * summa(pressure)) / (Math.Pow(summa(temperature), 2) - temperature.Length * summa(tt));


            for (int i = 0; i < temperature.Length; i++)
                chart1.Series[1].Points.AddXY(temperature[i], k * temperature[i] + b);
        }
        
    }
}
