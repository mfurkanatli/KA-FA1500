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
            PointF p = new PointF();
           
            guvenli_alan = Convert.ToInt32(textBox1.Text);
            hiz = Convert.ToInt32(textBox2.Text);
            yon = Convert.ToInt32(textBox3.Text);

            /*  p.X = float.Parse(textBox4.Text);
              p.Y = float.Parse(textBox5.Text);           
              */

           /* p.X = Form1.gemiler.ElementAt(0).merkez.X + float.Parse((uzaklik
                        * Math.Cos((Form1.gemiler.ElementAt(0).rota + kerterizAcisi + 90) * Math.PI / 180)).ToString());


            p.Y = Form1.gemiler.ElementAt(0).merkez.Y + float.Parse((uzaklik
                * -Math.Sin((Form1.gemiler.ElementAt(0).rota + kerterizAcisi + 90) * Math.PI / 180)).ToString());
                */
            
            //if
            
            if (Form1.gemiler.Count > 0)
            {

                float uzaklik = float.Parse(textBox5.Text);
                float kerterizAcisi = -float.Parse(textBox4.Text);

                p.X = Form1.gemiler.ElementAt(0).merkez.X + float.Parse((uzaklik
                        * Math.Cos((Form1.gemiler.ElementAt(0).rota + kerterizAcisi + 90) * Math.PI / 180)).ToString());

                p.Y = Form1.gemiler.ElementAt(0).merkez.Y + float.Parse((uzaklik
                    * -Math.Sin((Form1.gemiler.ElementAt(0).rota + kerterizAcisi + 90) * Math.PI / 180)).ToString());


                Form1.setVeriler(0, hiz, yon, p);

                Form1.gemiler.Last().bizimGemiyeUzaklik = uzaklik;
                Form1.gemiler.Last().kerterizAcisi = kerterizAcisi;
            }
            else
            {
                p.X = Form1.xx.Width / 2;
                p.Y = Form1.xx.Height / 2;

                Form1.setVeriler(guvenli_alan, hiz, yon, p);

                textBox1.ReadOnly = true;
                textBox1.Text = "0";
            }

            Form1.xx.olceklendir();
            Form1.xx.Yenile();
            // this.Close();
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
