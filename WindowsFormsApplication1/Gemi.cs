using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
namespace WindowsFormsApplication1
{
   public class Gemi
    {       
        public  float emniyet_alani;
        public float hiz;
        public float yedek_hiz;
        public float yedek_emniyet_alani;
        public float bizimGemiyeUzaklik;
        public float kerterizAcisi;
        public int rota;
        public double tcpa, dcpa;
        public PointF merkez;
        public PointF cizimPoint;
        public PictureBox pb;
        Form1 f1;

        public Gemi(float emniyet_alani, float hiz, int rota, PointF merkez,Form1 _f1)
        {
            f1 = _f1;
            this.emniyet_alani = emniyet_alani;
            this.yedek_emniyet_alani = emniyet_alani;
            this.hiz = hiz;
            this.yedek_hiz = hiz;
            this.rota = -rota;
            this.merkez = merkez;
            /*pb = new PictureBox();
            pb.Size = new Size(23, 23);
            
            //pb.ImageLocation = @"gemi.png";
            pb.SizeMode= PictureBoxSizeMode.StretchImage;
            pb.SendToB ack();
            pictureBoxHareketettiir();      
            pb.Show();           
            //f1.Controls.Add(pb);*/
        }
        public void gemiPictureBoxEkle()
        {
            pb = new PictureBox();
            pb.Size = new Size(23, 23);

            //pb.ImageLocation = @"gemi.png";
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxHareketettiir();
            pb.Show();
            pb.Image = WindowsFormsApplication1.Properties.Resources.gemi;
            f1.Controls.Add(pb);
        }
        public void pictureBoxHareketettiir()
        {
            /* pb.Left = (int) merkez.X + Form1.xx.Width / 2-pb.Width/2;
             pb.Top = (int ) merkez.Y + Form1.xx.Height / 2-pb.Height/2;*/

            pb.Left = (int)merkez.X - pb.Width / 2;
            pb.Top = (int)merkez.Y - pb.Height / 2;
        }

        private Bitmap RotateImageByAngle(Image oldBitmap, float angle)
        {
            var newBitmap = new Bitmap(oldBitmap.Width, oldBitmap.Height);
            newBitmap.SetResolution(oldBitmap.HorizontalResolution, oldBitmap.VerticalResolution);
            var graphics = Graphics.FromImage(newBitmap);
            graphics.TranslateTransform((float)oldBitmap.Width / 2, (float)oldBitmap.Height / 2);
            graphics.RotateTransform(angle);
            graphics.TranslateTransform(-(float)oldBitmap.Width / 2, -(float)oldBitmap.Height / 2);
            graphics.DrawImage(oldBitmap, new Point(0, 0));
            return newBitmap;
        }

        public float getemniyet_alani()
        {
            return this.emniyet_alani;
        }
        public void setemniyet_alani(int emniyet_alani)
        {
            this.emniyet_alani = emniyet_alani;
        }

        public float getHiz()
        {
            return this.hiz;
        }
        public void setHiz(int hiz)
        {
            this.hiz = hiz;
        }

        public int getrota()
        {
            return this.rota;
        }
        public void setrota(int rota)
        {
            this.rota = rota;
        }

        public void gemiIlerle(int rota, int hiz)
        {

        }
        public static Point cpaHesapla(Gemi g1, Gemi g2)
        {
            Point sonuc = new Point(0,0);
            return sonuc;
        }
        public static double tcpaHesapla(Gemi g1,Gemi g2)
        {
            double sonuc = 0;
            sonuc = Math.Sqrt(Math.Pow((g1.merkez.X - g2.merkez.X), 2) + Math.Pow((g1.merkez.Y - g2.merkez.Y), 2));
            return sonuc;
        }
        public static double dcpaHesapla(Gemi g1,Gemi g2)//her gemide tcpa ,dcpa hesabı olması gerekir.Ancak bu hesapların her gemi icin ayrı ayrı olusturulmasına gerek olmadığı icin static tanimladik
        {
            double sonuc = 0;
            return sonuc;
        }

        
    }
}
