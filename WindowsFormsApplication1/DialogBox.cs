using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class DialogBox : Form
    {
        public DialogBox()
        {
            InitializeComponent();
        }
        int min,  sabit,  max;
        public DialogBox(int min, int sabit, int max)
        {
            InitializeComponent();
            if (min<=0)
            {
                button1.Visible = false;
                label4.Visible = true;
            }
            else
            {
                label1.Text = "Düşürülecek Hız :";
                button1.Visible = true;
                label4.Visible = false;
            }
            if(min>0)
            label1.Text += min;
            label2.Text += sabit;
            label3.Text += max;
            this.min = min;
            this.sabit = sabit;
            this.max = max;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AnaEkran.gemiler.ElementAt(0).yedek_hiz = min;
            this.Close();
        }

        private void DialogBox_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AnaEkran.gemiler.ElementAt(0).yedek_hiz = sabit;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AnaEkran.gemiler.ElementAt(0).yedek_hiz = max;
            this.Close();
        }
    }
}
