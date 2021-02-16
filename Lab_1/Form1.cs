using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_1._1
{
    public partial class Form1 : Form
    {

        public string tempStr;
        public string tempNumber;
        public bool flag;

        public Form1()
        {
            flag = false;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                textBox1.Text = "0";
            }
            Button btn = (Button)sender;
            if (textBox1.Text == "0") textBox1.Text = btn.Text;
            else textBox1.Text += btn.Text;

        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            tempNumber = textBox1.Text;
            tempStr = btn.Text;
            flag = true;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (textBox1.Text != "0") textBox1.Text = Convert.ToString(Convert.ToDouble(textBox1.Text) * -1);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            double dNumber1, dNumber2, result = 0;
            dNumber1 = Convert.ToDouble(tempNumber);
            dNumber2 = Convert.ToDouble(textBox1.Text);
            switch (tempStr)
            {
                case "+":
                    {
                        result = dNumber1 + dNumber2;
                        break;
                    }
                case "-":
                    {
                        result = dNumber1 - dNumber2;
                        break;
                    }
                case "/":
                    {
                        result = dNumber1 / dNumber2;
                        break;
                    }
                case "*":
                    {
                        result = dNumber1 * dNumber2;
                        break;
                    }
                case "%":
                    {
                        result = dNumber1 % dNumber2;
                        break;
                    }
                case "//":
                    {
                        result = Math.Floor(dNumber1 / dNumber2);
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
            tempStr = "=";
            flag = true;
            textBox1.Text = result.ToString();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Contains(",")) textBox1.Text += ",";
        }
    }
    
}
  
