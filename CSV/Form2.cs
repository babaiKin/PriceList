using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            //TopMost = true;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "PriceListPass")
            {
                Form3 form = new Form3();
                form.Show();
                this.Close();
            }
            else
                MessageBox.Show("ERROR!! \nWrong password...");
        }
    }
}
