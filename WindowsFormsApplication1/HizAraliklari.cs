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
    public partial class HizAraliklari : Form
    {
        public HizAraliklari()
        {
            InitializeComponent();
        }

        private void HizAraliklari_Load(object sender, EventArgs e)
        {
            groupBox5.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form1.gemiler.ElementAt(0).gemiHizAraliklari = getter();
        }
        public int[,] getter()
        {
            int[,] araliklar = new int[4,2];
            araliklar[0, 0] = (int)numericUpDown1.Value;
            araliklar[0, 1] = (int)numericUpDown2.Value;
            araliklar[1, 0] = (int)numericUpDown6.Value;
            araliklar[1, 1] = (int)numericUpDown5.Value;
            araliklar[2, 0] = (int)numericUpDown4.Value;
            araliklar[2, 1] = (int)numericUpDown3.Value;
            araliklar[3, 0] = (int)numericUpDown8.Value;
            araliklar[3, 1] = (int)numericUpDown7.Value;
            return araliklar;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox5.Enabled = !groupBox5.Enabled;
        }

        public bool isChecked()
        {
            return checkBox1.Checked;
        }

    }
}
