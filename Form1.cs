using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Collections.Specialized;
using System.Collections;

namespace SimulationLab16
{
    public partial class Form1 : Form
    {
        private const double dT = 0.005f;
        private const double Mu = 0.1d;
        private const double Xi = 0.02d;
        private double W = 0;
        private double price;
        private Random random = new Random(Guid.NewGuid().GetHashCode());
        private double cost;
        private double roubles = 200;
        private int currency = 0;

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1000;
            Roubles.Text = roubles.ToString();
        }

        private double GetNumber()
        {
            return Math.Sqrt(-2 * Math.Log(random.NextDouble())) * Math.Cos(2 * Math.PI * random.NextDouble());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            cost = (float)inputPrice.Value;
            chart1.Series[0].Points.AddXY(0, cost);
            price = cost;
            currency = 0;
            Currency.Text = currency.ToString();
            roubles = 200;
            Roubles.Text = roubles.ToString();
            timer1.Start();
        }

        private void Buy_Click(object sender, EventArgs e)
        {
            if (roubles >= cost)
            {
                roubles -= cost;
                Roubles.Text = roubles.ToString();
                currency++;
                Currency.Text = currency.ToString();
            }
        }

        private void Sell_Click(object sender, EventArgs e)
        {
            if (currency > 0)
            {
                roubles += cost;
                currency--;
                Roubles.Text = roubles.ToString();
                Currency.Text = currency.ToString();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisY.Minimum = Math.Round(cost - 2, 4);
            W += Math.Sqrt(dT) * GetNumber();
            price *= Math.Exp((Mu - Xi * Xi / 2) * dT + Xi * W);
            chart1.Series[0].Points.AddXY(0, price);
            cost = (float)price;
        }
    }
}
