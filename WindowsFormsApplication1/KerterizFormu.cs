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
    public partial class KerterizFormu : Form
    {
        public KerterizFormu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.ShowDialog();
            String secilenDizin = op.FileName.ToString();

            if (!secilenDizin.Equals(""))
            {
                Form1.xx.temizle();
                SaveLoad saveLoad = new SaveLoad();
                float[,] veriler = saveLoad.Load(secilenDizin);


                PointF p = new PointF();
                /* p.X = veriler[0, veriler.GetLength(1) - 2];
                 p.Y = veriler[0, veriler.GetLength(1) - 1];
                 */

                /* p.X = Form1.xx.Width / 2;
                 p.Y = Form1.xx.Height / 2;
                 */
                p.X = 0;
                p.Y = 0;
                //Form1.setVeriler((int)veriler[0, 0] / Form1.katSayi, (int)veriler[0, 1], (int)-veriler[0, 2], p);
                Form1.setVeriler((int)veriler[0, 0] / Form1.katSayi, (int)veriler[0, 1], (int)-veriler[0, 2], p);
                float kerterizAcisi = 1;
                float uzaklik = 1;
                for (int i = 1; i < veriler.GetLength(0); i++)
                {
                    //PointF p = new PointF();
                    /* p.X = veriler[i, veriler.GetLength(1) - 2];
                     p.Y = veriler[i, veriler.GetLength(1) - 1];*/

                    kerterizAcisi = -veriler[i, veriler.GetLength(1) - 2];
                    uzaklik = veriler[i, veriler.GetLength(1) - 1];

                    p.X =Form1.gemiler.ElementAt(0).merkez.X+ float.Parse((uzaklik
                        * Math.Cos((kerterizAcisi + 90) * Math.PI / 180)).ToString());


                    p.Y = Form1.gemiler.ElementAt(0).merkez.Y+ float.Parse((uzaklik
                        *-Math.Sin((kerterizAcisi + 90) * Math.PI / 180)).ToString());

                 /*   p.X = veriler[i, veriler.GetLength(1) - 2];
                    p.Y = veriler[i, veriler.GetLength(1) - 1];*/

                    //Form1.setVeriler((int)veriler[i, 0] / Form1.katSayi, (int)veriler[i, 1], (int)-veriler[i, 2], p);
                    Form1.setVeriler((int)veriler[i, 0] / Form1.katSayi, (int)veriler[i, 1], (int)-veriler[i, 2], p);
                }
            }
        }

        public void kerterizeGoreKonumlandir()
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "Data Files (*.txt)|*.txt";
            sv.DefaultExt = "txt";
            sv.AddExtension = true;
            sv.ShowDialog();
            String s = sv.FileName;

            if (!s.Equals(""))
            {
                SaveLoad saveLoad = new SaveLoad();
                saveLoad.Save(s, Form1.gemiler);
            }
        }
    }
}
