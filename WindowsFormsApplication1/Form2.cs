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
            InitializeComponent();
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            int guvenli_alan=0;
            int hiz;
            int yon;
            Point p = new Point();
           
            guvenli_alan = Convert.ToInt32(textBox1.Text);
            hiz = Convert.ToInt32(textBox2.Text);
            yon = Convert.ToInt32(textBox3.Text);
            p.X = Convert.ToInt32(textBox4.Text);
            p.Y = Convert.ToInt32(textBox5.Text);           
            Form1.setVeriler(guvenli_alan, hiz, yon,p);
            
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (Form1.gemiler.Count > 0)
            {
                textBox1.ReadOnly = true;
            }                 
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
