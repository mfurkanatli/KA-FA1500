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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            gemileriGetir();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void gemileriGetir()
        {
            listBox1.Items.Clear();
            for (int i = 0; i < Form1.gemiler.Count; i++)
            {
                listBox1.Items.Add("Gemi "+Form1.gemiler.IndexOf(Form1.gemiler.ElementAt(i)));
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index >= 0 && Form1.gemiler.Count < index)
            {
                textBox1.Text = Form1.gemiler.ElementAt(index).emniyet_alani + "";
                textBox2.Text = Form1.gemiler.ElementAt(index).hiz + "";
                textBox3.Text = Form1.gemiler.ElementAt(index).rota + "";
                textBox4.Text = Form1.gemiler.ElementAt(index).merkez.X + "";
                textBox5.Text = Form1.gemiler.ElementAt(index).merkez.Y + "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result= MessageBox.Show("Gemiyi Silmek Istediginizden Emin Misiniz ??", "Onaylama", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                Form1.gemiler.RemoveAt(listBox1.SelectedIndex);
                gemileriGetir();
            }
        }
    }
}
