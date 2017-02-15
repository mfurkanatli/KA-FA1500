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
        
        Point merkez;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(Point merkez)
        {
            InitializeComponent();
            this.merkez = merkez;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int guvenli_alan;
            int hiz;
            int yon;
            guvenli_alan = Convert.ToInt32(textBox1.Text);
            hiz = Convert.ToInt32(textBox2.Text);
            yon = Convert.ToInt32(textBox3.Text);
            Form1.getVeriler(guvenli_alan, hiz, yon,merkez);
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
                
        }
    }
}
