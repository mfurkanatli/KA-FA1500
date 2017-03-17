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
        public  int emniyet_alani;
        public int hiz;
        public int rota;
        public double tcpa, dcpa;
        public PointF merkez;
        public PointF cizimPoint;
        public PictureBox pb;
        

        public Gemi(int emniyet_alani, int hiz, int rota, PointF merkez)
        {
            
            this.emniyet_alani = emniyet_alani;
            this.hiz = hiz;
            this.rota = -rota;
            this.merkez = merkez;
            pb = new PictureBox();
            pb.Size = new Size(15, 15);
            pb.ImageLocation = "gemi.png";
            pb.SizeMode= PictureBoxSizeMode.StretchImage;
            pb.SendToBack();
            pictureBoxHareketettiir();      
            pb.Show();            
            
            Form1.xx.Controls.Add(pb);
        }

        public void pictureBoxHareketettiir()
        {
            pb.Left = (int) merkez.X + Form1.xx.Width / 2-pb.Width/2;
            pb.Top = (int ) merkez.Y + Form1.xx.Height / 2-pb.Height/2;
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

        public int getemniyet_alani()
        {
            return this.emniyet_alani;
        }
        public void setemniyet_alani(int emniyet_alani)
        {
            this.emniyet_alani = emniyet_alani;
        }

        public int getHiz()
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
