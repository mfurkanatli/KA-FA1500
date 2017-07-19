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
    public partial class GemiGirdileriEkrani : Form
    {
        
        public GemiGirdileriEkrani()
        {
            InitializeComponent();
            hideItems();
        }


        public void hideItems()
        {
            textBox4.Visible = false;
            textBox5.Visible = false;
            label1.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
        }
        private void showItems()
        {             
            textBox4.Visible = true;
            textBox5.Visible = true;
            label1.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            int guvenli_alan=0;
            int hiz;
            int yon;
            PointF p = new PointF();
           
            guvenli_alan = Convert.ToInt32(textBox1.Text);
            hiz = Convert.ToInt32(textBox2.Text);
            yon = Convert.ToInt32(textBox3.Text);

            button1.Text = "Hedef Gemiyi Oluştur";
            showItems();

            /*  p.X = float.Parse(textBox4.Text);
              p.Y = float.Parse(textBox5.Text);           
              */

            /* p.X = Form1.gemiler.ElementAt(0).merkez.X + float.Parse((uzaklik
                         * Math.Cos((Form1.gemiler.ElementAt(0).rota + kerterizAcisi + 90) * Math.PI / 180)).ToString());


             p.Y = Form1.gemiler.ElementAt(0).merkez.Y + float.Parse((uzaklik
                 * -Math.Sin((Form1.gemiler.ElementAt(0).rota + kerterizAcisi + 90) * Math.PI / 180)).ToString());
                 */

            //if

            if (AnaEkran.gemiler.Count > 0)
            {

                float uzaklik = float.Parse(textBox5.Text);

                float kerterizAcisi = -float.Parse(textBox4.Text);

                p.X = AnaEkran.gemiler.ElementAt(0).merkez.X + float.Parse((uzaklik
                        * Math.Cos((AnaEkran.gemiler.ElementAt(0).rota + kerterizAcisi + 90) * Math.PI / 180)).ToString());

                p.Y = AnaEkran.gemiler.ElementAt(0).merkez.Y + float.Parse((uzaklik
                    * -Math.Sin((AnaEkran.gemiler.ElementAt(0).rota + kerterizAcisi + 90) * Math.PI / 180)).ToString());


                AnaEkran.setVeriler(0, hiz, yon, p);

                AnaEkran.gemiler.Last().bizimGemiyeUzaklik = uzaklik;
                AnaEkran.gemiler.Last().kerterizAcisi = kerterizAcisi;
            }
            else
            {
                p.X = AnaEkran.xx.Width / 2;
                p.Y = AnaEkran.xx.Height / 2;

                AnaEkran.setVeriler(guvenli_alan, hiz, yon, p);

                textBox1.ReadOnly = true;
                textBox1.Text = "0";
            }

            AnaEkran.xx.olceklendir();
            AnaEkran.xx.Yenile();
            // this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (AnaEkran.gemiler.Count > 0)
            {
                textBox1.ReadOnly = true;
            }    
                         
        }
         
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
