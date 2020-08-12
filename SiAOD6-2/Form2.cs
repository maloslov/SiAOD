using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiAOD6_2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == 8)
                e.Handled = false;
            else e.Handled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int digit;
            if(textBox1.Text.Length > 0)
            {
                digit = Convert.ToInt32(textBox1.Text);
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Text = textBox1.Text;
            this.Close();
        }
    }
}
