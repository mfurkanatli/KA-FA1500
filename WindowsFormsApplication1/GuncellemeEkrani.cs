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
    public partial class GuncellemeEkrani : Form
    {
       
        public GuncellemeEkrani()
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

        public void temizle()
        {
            comboBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = ""; 
        }
        public void kendiGemimizIcinGoster()
        {
           // label5.Visible = false;
          //  label4.Visible = false;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;

           // label6.Visible = true;
            textBox1.ReadOnly = false;

        }
        public void hedefGemiIcinGoster()
        {
        //    label5.Visible = true;
         //   label4.Visible = true;
            textBox4.ReadOnly = false;
            textBox5.ReadOnly = false;

            //label6.Visible = false;
            textBox1.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //int index = listBox1.SelectedIndex;
            int index = comboBox1.SelectedIndex;
            if (index >= 0 && index < AnaEkran.gemiler.Count)
            {

                AnaEkran.gemiler.ElementAt(index).yedek_emniyet_alani = (Convert.ToInt32(textBox1.Text));
                AnaEkran.gemiler.ElementAt(index).yedek_hiz = float.Parse(textBox2.Text);
                if (Convert.ToInt32(textBox3.Text) > 0)
                {
                    AnaEkran.gemiler.ElementAt(index).rota = (-Convert.ToInt32(textBox3.Text));
                }
                else
                {
                    AnaEkran.gemiler.ElementAt(index).rota = Convert.ToInt32(textBox3.Text);
                }
                AnaEkran.gemiler.ElementAt(index).kerterizAcisi = Convert.ToInt32(textBox4.Text);
                AnaEkran.gemiler.ElementAt(index).bizimGemiyeUzaklik = float.Parse(textBox5.Text);

                AnaEkran.xx.Yenile();
                MessageBox.Show("Degisim Onaylandi");
            }
        }

        private void gemileriGetir()
        {
            //listBox1.Items.Clear();
            comboBox1.Items.Clear();
            for (int i = 0; i < AnaEkran.gemiler.Count; i++)
            {
                //listBox1.Items.Add("Gemi "+Form1.gemiler.IndexOf(Form1.gemiler.ElementAt(i)));
                if (i == 0)
                {

                    comboBox1.Items.Add("Kendi Gemimiz");
                }
                else
                {

                    comboBox1.Items.Add("Gemi " + AnaEkran.gemiler.IndexOf(AnaEkran.gemiler.ElementAt(i)));
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index >= 0 && AnaEkran.gemiler.Count() > index)
            {
                textBox1.Text = AnaEkran.gemiler.ElementAt(index).emniyet_alani + "";
                if (index >= 1)
                    textBox1.ReadOnly = true;
                else
                    textBox1.ReadOnly = false;
                textBox2.Text = AnaEkran.gemiler.ElementAt(index).yedek_hiz + "";
                textBox3.Text = Math.Abs(AnaEkran.gemiler.ElementAt(index).rota) + "";
                textBox4.Text = AnaEkran.gemiler.ElementAt(index).merkez.X + "";
                textBox5.Text = AnaEkran.gemiler.ElementAt(index).merkez.Y + "";
            }
        }

      
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex!=0)
            {
                DialogResult result = MessageBox.Show("Gemiyi Silmek Istediginizden Emin Misiniz ??", "Onaylama", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    
                    //Form1.gemiler.RemoveAt(listBox1.SelectedIndex);
                    AnaEkran.gemiler.ElementAt(comboBox1.SelectedIndex).pb.Dispose();
                    AnaEkran.gemiler.RemoveAt(comboBox1.SelectedIndex);
                    gemileriGetir();
                    AnaEkran.xx.Yenile();
                    comboBox1.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Kendi Gemimiz Silinemez");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            if (index >= 0 && AnaEkran.gemiler.Count() > index)
            {
                textBox1.Text = AnaEkran.gemiler.ElementAt(index).yedek_emniyet_alani + "";
                if (index >= 1)
                {
                    
                    hedefGemiIcinGoster();
                }

                else
                { 
                    kendiGemimizIcinGoster();
                }
                   
                textBox2.Text = AnaEkran.gemiler.ElementAt(index).yedek_hiz + "";
                textBox3.Text = Math.Abs(AnaEkran.gemiler.ElementAt(index).rota) + "";
                textBox4.Text = AnaEkran.gemiler.ElementAt(index).kerterizAcisi + "";
                textBox5.Text = AnaEkran.gemiler.ElementAt(index).bizimGemiyeUzaklik + "";
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            gemileriGetir();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
