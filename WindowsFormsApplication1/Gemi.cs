using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication1
{
   public class Gemi
    {
       
      public  int emniyet_alani;
      public int hiz;
      public int rota;
      public double tcpa, dcpa;
      public Point merkez;
        public Gemi(int emniyet_alani, int hiz, int rota, Point merkez)
        {
            this.emniyet_alani = emniyet_alani;
            this.hiz = hiz;
            this.rota = -rota;
            this.merkez = merkez;           
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
