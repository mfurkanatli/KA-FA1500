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
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        static public void yaz()
        {

        }
        static int katSayi = 10;
        static List<Gemi> gemiler = new List<Gemi>();

        private void Form1_DoubleClick(object sender, EventArgs e)
        {

            Point merkez = new Point(Control.MousePosition.X - this.Location.X, Control.MousePosition.Y - this.Location.Y);
      //      Console.WriteLine((Control.MousePosition.X - this.Location.X) + ";" + (Control.MousePosition.Y - this.Location.Y )+ "");
            Form2 form2 = new Form2(merkez);
            form2.Show();

        }
        public static void getVeriler(int guvenli_alan, int hiz, int yon, Point merkez)
        {
            gemiler.Add(new Gemi(guvenli_alan*katSayi, hiz, yon, merkez));
          //  Console.WriteLine(a+";"+ b+";"+ c+"---"+merkez.X+";"+merkez.Y+"");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gemiler.Count;i++ )
            {
                gemiCiz(gemiler.ElementAt(i));
            }
            Timer timer = new Timer();
            int timeOut = 5000;
            Gemi karsiGemi = gemiler.ElementAt(0);
            Gemi bizimGemi = gemiler.ElementAt(1);
                timer.Start();
            timer.Interval = 1000;
            while (timer.Interval < timeOut && !carpistiMi())
            {
                Console.WriteLine(timer.Interval + "");
                karsiGemi.merkez.X += Convert.ToInt32(karsiGemi.hiz 
                    * Math.Cos((karsiGemi.yon + 90) * Math.PI / 180));
                karsiGemi.merkez.Y += Convert.ToInt32(karsiGemi.hiz 
                    * -Math.Sin((karsiGemi.yon + 90) * Math.PI / 180));

                bizimGemi.merkez.X += Convert.ToInt32(bizimGemi.hiz 
                    * Math.Cos((bizimGemi.yon + 90) * Math.PI / 180));
                bizimGemi.merkez.Y += Convert.ToInt32(bizimGemi.hiz 
                    * -Math.Sin((bizimGemi.yon + 90) * Math.PI / 180));

                gemiCiz(bizimGemi);
                gemiCiz(karsiGemi);
            }

            timer.Stop();
        }

        private bool carpistiMi()
        {
            Gemi karsiGemi = gemiler.ElementAt(0);
            Gemi bizimGemi = gemiler.ElementAt(1);
            if (Math.Pow((karsiGemi.merkez.X - bizimGemi.merkez.X), 2) + 
                Math.Pow((karsiGemi.merkez.Y - bizimGemi.merkez.Y), 2) <= Math.Pow(bizimGemi.guvenli_alan,2))
            {
                MessageBox.Show("Çarpıştı");
                return true;
            }
            return false;
        }

        private void gemiCiz(Gemi gemi)
        {
            
            int r = 500;
            int x = gemi.merkez.X+Convert.ToInt32(r * Math.Cos((gemi.yon+90) * Math.PI / 180));
            int y = gemi.merkez.Y+Convert.ToInt32(r * -Math.Sin((gemi.yon+90) * Math.PI / 180));
            Console.WriteLine(Math.Sin(gemi.yon * Math.PI / 180) + "");
            Point hedef = new Point(x, y);
            Graphics g = this.CreateGraphics();
            
            Pen cevre = new Pen(Color.Red,1);
            Pen yol = new Pen(Color.Blue, 1);
            g.DrawEllipse(cevre, gemi.merkez.X - gemi.guvenli_alan/2,
                gemi.merkez.Y - gemi.guvenli_alan/2, gemi.guvenli_alan, gemi.guvenli_alan);
            g.DrawLine(yol, gemi.merkez, hedef);
            
        }
    }
}
