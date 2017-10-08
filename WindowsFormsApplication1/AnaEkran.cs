using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace WindowsFormsApplication1
{
    public partial class AnaEkran : Form
    {
       
        Graphics cizim;
        Gemi karsiGemi;
        Gemi bizimGemi;
        Graphics g;
        public static int katSayi = 10;
        public static List<Gemi> gemiler = new List<Gemi>();
        bool veriOnayla = false;
        //int jenarasyon = 50;
        bool catismaVar = false;
        bool catismadanKaciliyor = true;
        int gosterilecekAlternatifYolSayisi = 10;
        Random rnd = new Random();
        List<PointF> points = new List<PointF>(); //altarnatif yol noktaları
        List<List<PointF>> alternatifYollar = new List<List<PointF>>();
        List<Rota> alternatifRotalar = new List<Rota>();
        int hizOran = 10000;
        int trackBarEskiDeger;
        public AnaEkran()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.WindowState = FormWindowState.Maximized;
            g = this.CreateGraphics();
            cizim = CreateGraphics();

        }
        static public void yaz()
        {

        }

        //Double click yerine manuel koordinat girilecek
        private void Form1_DoubleClick(object sender, EventArgs e)
        {


            /*
            Point merkez = new Point(Control.MousePosition.X - this.Location.X, Control.MousePosition.Y - this.Location.Y);
            //      Console.WriteLine((Control.MousePosition.X - this.Location.X) + ";" + (Control.MousePosition.Y - this.Location.Y )+ "");
            Pen pen = new Pen(Color.Black, 3);

            cizim.DrawLine(pen, merkez.X, merkez.Y - 30, merkez.X + 10, merkez.Y - 10);
            cizim.DrawLine(pen, merkez.X + 10, merkez.Y - 30, merkez.X, merkez.Y - 10);
            Form2 form2 = new Form2();
            form2.Show();*/

        }
        
        public static void setVeriler(int emniyet_alani, float hiz, int rota, PointF merkez)
        {
            gemiler.Add(new Gemi(emniyet_alani , hiz, rota, merkez,xx));
            gemiler.Last().gemiPictureBoxEkle();
            if (gemiler.Count>1)
            {
                gemiler.ElementAt(gemiler.Count - 1).pb.Image = Properties.Resources.gemi3;
            }
             
        }

        public GemiGirdileriEkrani gemiGirdileriEkrani = new GemiGirdileriEkrani();
        public GuncellemeEkrani guncellemeEkrani = new GuncellemeEkrani();
        public HizAraliklari hizForm = new HizAraliklari();
        private void Form1_Load(object sender, EventArgs e)
        {
            xx = this;
            //xx.DoubleBuffered = true;
            // label1.Text = "";
            label1.Visible = false;
            progressBar1.Visible = false;

            //tableLayoutPanel1.Height = (int);
            panel4.Height = (int)(xx.Height * 0.9);
            /* panel1.Height = (int)(xx.Height * 0.5);           

             panel2.Height = (int)(xx.Height * 0.5);            
             panel3.Height = (int)(xx.Height * 0.5);
             panel5.Height =(int)(xx.Height * 0.5);            
             panel4.Height =(int)(xx.Height * 0.9);*/

            /* panel2.Anchor = panel1.;
             panel3.Anchor = panel2.Anchor;
             //panel4.Anchor = AnchorStyles.Top;
             panel5.Anchor = panel5.Anchor;*/

            tabControl1.Parent = panel2;
            tabControl1.TabPages[0].Text = "Gemi Girdileri";
            tabControl1.TabPages[1].Text = "Hız Aralıkları";

            gemiGirdileriEkrani.TopLevel = false;
            gemiGirdileriEkrani.BringToFront();
            gemiGirdileriEkrani.WindowState = FormWindowState.Maximized;
            gemiGirdileriEkrani.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            gemiGirdileriEkrani.Parent = tabControl1.TabPages[0];
            gemiGirdileriEkrani.Text = "Gemi " + gemiler.Count();
            gemiGirdileriEkrani.Show();

            hizForm.TopLevel = false;
            hizForm.BringToFront();
            //hizForm.WindowState = FormWindowState.Maximized;
            hizForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            hizForm.Parent = tabControl1.TabPages[1];
            hizForm.Text = "Hızlar";
            hizForm.Show();

            //tableLayoutPanel1.SetRow(tabControl1,2);
            /*
            tabControl1.Parent = panel2;
            tabControl1.Size = panel2.Size;
            tabControl1.BringToFront();
            */
            guncellemeEkrani.TopLevel = false;
            guncellemeEkrani.BringToFront();
            guncellemeEkrani.WindowState = FormWindowState.Maximized;
            guncellemeEkrani.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            guncellemeEkrani.Parent = panel5;
            guncellemeEkrani.Text = "Gemi " + gemiler.Count();
            guncellemeEkrani.Show();
            

            trackBar1.SetRange(20, 200);
            trackBar1.TickFrequency = 10;
            trackBar1.SmallChange = 10;
            trackBar1.LargeChange = 0;
            trackBarEskiDeger = trackBar1.Value;
            label15.Text = "Ölçek : 1 mil = " + trackBar1.Value + " px";

            /* PictureBox pb = new PictureBox();

             pb.Width = 50;
             pb.Height = 50;

             //pb.Image = Image.FromFile("gemi.png");
             pb.BackgroundImageLayout = ImageLayout.Stretch;
             pb.ImageLocation = "gemi.png";

             pb.Left = this.Width / 2;
             pb.Top =   this.Height / 2;
             //pb.Show();
             this.Controls.Add(pb);*/
            //
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(gemiler.Count>1)
            {

                //MessageBox.Show("Veriler Onaylandı");
                VeriOnaylamaDialogBox veriOnayBox = new VeriOnaylamaDialogBox();
                veriOnayBox.ShowDialog();
                 
                veriOnayla = true;
                trackBar1.Enabled = false;
                if(hizForm.isChecked())
                    AnaEkran.gemiler.ElementAt(0).gemiHizAraliklari = hizForm.getter();

                bizimGemi = gemiler.ElementAt(0);
                karsiGemi = gemiler.ElementAt(1);

                Yenile();           
            }
            else
            {
                MessageBox.Show("Yeterli Sayıda Gemi Girmediniz");
            }
            
        }

    
        public void gemiKonumlandir(Gemi _gemi)
        {

            int w = this.Width;
            int h = this.Height;
            _gemi.merkez.X = w / 2;
            _gemi.merkez.Y = h / 2;
        }
        public float ikiNoktaArasiUzaklikHesabi(Gemi _bizimGemi, Gemi _karsiGemi)
        {
            return (float)Math.Sqrt(Math.Pow(_bizimGemi.merkez.X - _karsiGemi.merkez.X, 2) + 
                Math.Pow(_bizimGemi.merkez.Y - _karsiGemi.merkez.Y, 2));
        }
        public void olceklendir()
        {
            Gemi gemi;
            if (gemiler.Count > 0)
            {
                double kerteriz = 0;
                gemiKonumlandir(gemiler.ElementAt(0));
                gemiler.ElementAt(0).emniyet_alani = gemiler.ElementAt(0).yedek_emniyet_alani * trackBar1.Value;
                for (int i = 0; i < gemiler.Count; i++)
                {
                    gemi = gemiler.ElementAt(i);
                    gemi.hiz = gemi.yedek_hiz * (1.0f * timer1.Interval / hizOran) * trackBar1.Value;

                    if (i > 0)
                    {
                        //gemi.bizimGemiyeUzaklik = ikiNoktaArasiUzaklikHesabi(gemiler.ElementAt(0), gemi) / trackBarEskiDeger;
                        //kerteriz = kerterizHesabi(gemiler.ElementAt(0), gemi);
                        kerteriz = gemi.kerterizAcisi;
                        gemi.merkez.X = gemiler.ElementAt(0).merkez.X + (float)(gemi.bizimGemiyeUzaklik *
                            trackBar1.Value * Math.Cos((gemiler.ElementAt(0).rota + kerteriz + 90) * Math.PI / 180));

                        gemi.merkez.Y = gemiler.ElementAt(0).merkez.Y + (float)(gemi.bizimGemiyeUzaklik *
                            trackBar1.Value * -Math.Sin((gemiler.ElementAt(0).rota + kerteriz + 90) * Math.PI / 180));

                    }
                }
                //alternatifYollariOlceklendir();
            }
        }
        public double kerterizHesabi(Gemi _bizimGemi, Gemi _karsiGemi)
        {
            double r1 = Math.Sqrt(Math.Pow(_bizimGemi.merkez.X-_karsiGemi.merkez.X,2)+ Math.Pow(_bizimGemi.merkez.Y - _karsiGemi.merkez.Y, 2));

            float x = _bizimGemi.merkez.X + (float)(r1 * Math.Cos((_bizimGemi.rota + 90) * Math.PI / 180));
            float y = _bizimGemi.merkez.Y + (float)(r1 * -Math.Sin((_bizimGemi.rota + 90) * Math.PI / 180));

            double r2 = Math.Sqrt(Math.Pow(x - _karsiGemi.merkez.X, 2) + Math.Pow(y- _karsiGemi.merkez.Y, 2));

            double kerteriz = Math.Acos((r2*r2-2*r1* r1)/(-2*r1*r1));
            kerteriz *=  (180.0 / Math.PI);
            //MessageBox.Show(kerteriz+"");
            if (_bizimGemi.merkez.X > _karsiGemi.merkez.X) kerteriz *= -1;
            return -kerteriz;
        }
        public String durumKontrolu(Gemi _bizimGemi, Gemi _karsiGemi)
        {
            String s = "";
            double rota;
            if (-karsiGemi.rota > 180)
                rota = -karsiGemi.rota - 180 + bizimGemi.rota;
            else
                rota = -karsiGemi.rota + 180 + bizimGemi.rota;
            rota %= 360;

            if ((rota >= 355 && rota <= 360) || (rota >= 0 && rota < 5))
            {
                s = "Head-on";
            }
            else if (rota >= 247.5 && rota < 355)
            {
                s = "Crossing Stand-on";
            }
            else if (rota >= 112.5 && rota < 247.5)
            {
                s = "Overtaking";
            }
            else if (rota >= 5 && rota < 112.5)
            {
                s = "Crossing Give-way";
            }
            return s;
        }

        Rota rota = new Rota();
        public void gemiHareketEttir(Gemi gemi)
        {
            
            gemi.merkez.X += float.Parse((gemi.hiz
                   * Math.Cos((gemi.rota + 90) * Math.PI / 180)).ToString());

            gemi.merkez.Y += float.Parse((gemi.hiz
                   * -Math.Sin((gemi.rota + 90) * Math.PI / 180)).ToString());
            // return gemi;
        }

        private bool carpistiMi(Gemi _bizimGemi, Gemi _karsiGemi)
        {
           
            if (Math.Pow((_karsiGemi.merkez.X - _bizimGemi.merkez.X), 2) +
                Math.Pow((_karsiGemi.merkez.Y - _bizimGemi.merkez.Y), 2) <= Math.Pow(_bizimGemi.emniyet_alani, 2))
            {
                MessageBox.Show("Çarpıştı");
                return true;
            }
            return false;
        }

        Point cizimKonumu=new Point();
        private void gemiCiz(Gemi gemi)
        {
            cizimKonumu.X = 0;
            cizimKonumu.Y = 0;
                
            int r = 500;
            float x = gemi.merkez.X + (float)(r * Math.Cos((gemi.rota + 90) * Math.PI / 180));
            float y = gemi.merkez.Y + (float)(r * -Math.Sin((gemi.rota + 90) * Math.PI / 180));
           // Console.WriteLine(Math.Sin(gemi.rota * Math.PI / 180) + "");
            PointF hedef = new PointF(x, y);
           
            Pen cevre;
            Pen yol;
            if (gemiler.IndexOf(gemi) == 0)
            {
                cevre = new Pen(Color.Red);
                yol = new Pen(Color.Blue);
            }
            else
            {
               cevre = new Pen(Color.Thistle);
                yol = new Pen(Color.Black);
            }
            PointF rotaKonum = new PointF();
            rotaKonum.X = gemi.merkez.X + cizimKonumu.X;
            rotaKonum.Y = gemi.merkez.Y + cizimKonumu.Y;

            PointF rotaHedef=new PointF();
            rotaHedef.X = hedef.X + cizimKonumu.X;
            rotaHedef.Y = hedef.Y + cizimKonumu.Y;
            if (gemiler.IndexOf(gemi)==0)
            {
                g.DrawEllipse(cevre, cizimKonumu.X + gemi.merkez.X - gemi.emniyet_alani / 2,
                    cizimKonumu.Y + gemi.merkez.Y - gemi.emniyet_alani / 2, gemi.emniyet_alani, gemi.emniyet_alani);
            }
            g.DrawLine(yol, rotaKonum, rotaHedef);

        }
        public void Yenile()
        {                       
            g.Clear(SystemColors.Control);
            olceklendir();
            for (int i = 0; i < gemiler.Count; i++)
            {
                gemiCiz(gemiler.ElementAt(i));
                gemiler.ElementAt(i).pictureBoxHareketettiir();
            }
            
        }
        public void Yenile1()
        {
            g.Clear(SystemColors.Control);
            for (int i = 0; i < gemiler.Count; i++)
            {
                gemiCiz(gemiler.ElementAt(i));
                gemiler.ElementAt(i).pictureBoxHareketettiir();
            }

        }
        public void gemiHareketHesapla(int _i)
        {
            gemiHareketEttir(gemiler.ElementAt(_i));
            gemiler.ElementAt(_i).pictureBoxHareketettiir();
            gemiCiz(gemiler.ElementAt(_i));
        }

        public void forIleCizim()
        {
            rota.co[2] = Math.Ceiling(rota.co[2]);
            for(int i=0;i<3;i++)
            {
                for(int j=0;j<rota.t[i];j++)
                {
                    gemiHareketHesapla(0);
                }
                gemiler.ElementAt(0).rota -= (int)(rota.co[i]);
            }
            timer1.Enabled = false;
        }

        int index = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if(gemiler.Count>1)
            {               
                 
                g.Clear(this.BackColor);
                if (catismaVar)
                {
                    if (index < 3 && rota.t[index] > 0)
                    {
                        gemiHareketHesapla(0);
                        rota.t[index]--;
                        if(rota.t[index] < 0)
                        {
                            gemiler.ElementAt(0).merkez.X = gemiler.ElementAt(0).merkez.X+float.Parse((rota.t[index]* gemiler.ElementAt(0).hiz
                        * Math.Cos((gemiler.ElementAt(0).rota  + 90) * Math.PI / 180)).ToString());
                            gemiler.ElementAt(0).merkez.Y = gemiler.ElementAt(0).merkez.Y+float.Parse((rota.t[index]* gemiler.ElementAt(0).hiz
                        * -Math.Sin((gemiler.ElementAt(0).rota  + 90) * Math.PI / 180)).ToString());
                            Yenile1();
                        }
                        if (rota.t[index] <= 0)
                        {
                            gemiler.ElementAt(0).rota -= (int)(rota.co[index]);
                            index++;
                        }
                    }

                    if (index > 2)
                    {
                        gemiHareketHesapla(0);
                    }
                    for (int i = 1; i < gemiler.Count; i++)
                    {
                        gemiHareketHesapla(i);
                    }
                    alternatifYollariCiz(); 
                }
                else
                {
                    for (int i = 0; i < gemiler.Count; i++)
                    {
                        gemiHareketHesapla(i);
                    }
                }
                radarGorunumu();

            }            
        }

        void radarGorunumu()
        {
            float x = 0, y = 0;
            x = (float)(gemiler.ElementAt(0).hiz * Math.Cos((gemiler.ElementAt(0).rota + 90) * Math.PI / 180));
            y = (float)(gemiler.ElementAt(0).hiz * -Math.Sin((gemiler.ElementAt(0).rota + 90) * Math.PI / 180));
            for (int i=0;i<gemiler.Count;i++)
            {
                gemiler.ElementAt(i).merkez.X -= x;
                gemiler.ElementAt(i).merkez.Y -= y;
            }
            float x_yedek, y_yedek;
            for (int i=0;i<alternatifYollar.Count;i++)
            {
                for (int j = 0; j < alternatifYollar.ElementAt(0).Count; j++)
                {
                    x_yedek = alternatifYollar.ElementAt(i).ElementAt(j).X - x;
                    y_yedek = alternatifYollar.ElementAt(i).ElementAt(j).Y - y;
                    alternatifYollar.ElementAt(i).RemoveAt(j);
                    alternatifYollar.ElementAt(i).Insert(j,new PointF(x_yedek,y_yedek));

                }
            }
            
        }

        void alternatifYollariBelirle()
        {
            float x, y;
            float x_yedek =0, y_yedek=0;
            double rota;
            double r = 100;
            for(int k=0;k < gosterilecekAlternatifYolSayisi; k++)
            {
                
                int i = rnd.Next(0, GeneticAlgorithm.rotalar.Count);
                if (k == 0) i = 0;
                points = new List<PointF>();
                points.Add(new PointF(gemiler.ElementAt(0).merkez.X ,gemiler.ElementAt(0).merkez.Y));
                alternatifRotalar.Add(GeneticAlgorithm.rotalar.ElementAt(i));
                rota = gemiler.ElementAt(0).rota;
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                    {
                        x = gemiler.ElementAt(0).merkez.X + (float)(GeneticAlgorithm.rotalar.ElementAt(i).t[j] * gemiler.ElementAt(0).hiz
                            * Math.Cos((rota + 90) * Math.PI / 180));
                        y = gemiler.ElementAt(0).merkez.Y + (float)(GeneticAlgorithm.rotalar.ElementAt(i).t[j] * gemiler.ElementAt(0).hiz
                            * -Math.Sin((rota + 90) * Math.PI / 180));
                    }
                    else
                    {
                        x = x_yedek + (float)(GeneticAlgorithm.rotalar.ElementAt(i).t[j] * gemiler.ElementAt(0).hiz
                            * Math.Cos((rota + 90) * Math.PI / 180));
                        y = y_yedek + (float)(GeneticAlgorithm.rotalar.ElementAt(i).t[j] * gemiler.ElementAt(0).hiz
                            * -Math.Sin((rota + 90) * Math.PI / 180));
                    }
                    x_yedek = x;
                    y_yedek = y;
                    rota -= GeneticAlgorithm.rotalar.ElementAt(i).co[j];
                    points.Add(new PointF(x , y ));
                }
                x = x_yedek + (float)(r*Math.Cos((rota + 90) * Math.PI / 180));
                y = y_yedek + (float)(r*-Math.Sin((rota + 90) * Math.PI / 180));
                points.Add(new PointF(x , y));
                alternatifYollar.Add(points);
                //points.Clear();
            }
           
        }

        

        void alternatifYollariOlceklendir()
        {
            float x, y;
            int bulunulan_indis = 0;
            double rota;
            int rota1;
            double r = 100;
            for (int i = 0; i < alternatifYollar.Count; i++)
            {
                rota = gemiler.ElementAt(0).rota;
                rota1 = gemiler.ElementAt(0).rota;
                for (int k = this.rota.t.Length-1; k >= 0; k--)
                    if (this.rota.t[k] < alternatifRotalar.ElementAt(i).t[k])
                    {
                        x = gemiler.ElementAt(0).merkez.X - (float)((alternatifRotalar.ElementAt(i).t[k] - this.rota.t[k]) *
                        gemiler.ElementAt(0).hiz * Math.Cos((rota + 90) * Math.PI / 180));
                        y = gemiler.ElementAt(0).merkez.Y - (float)((alternatifRotalar.ElementAt(i).t[k] - this.rota.t[k]) *
                            gemiler.ElementAt(0).hiz * -Math.Sin((rota + 90) * Math.PI / 180));

                        alternatifYollar.ElementAt(i).RemoveAt(k);
                        alternatifYollar.ElementAt(i).Insert(k, new PointF(x, y));
                        if (k > bulunulan_indis)
                            bulunulan_indis = k;

                        if (k > 0)
                            rota += this.rota.co[k-1];
                    }

                gemiler.ElementAt(0).rota = rota1;
                for (int j = bulunulan_indis+1; j < alternatifYollar.ElementAt(i).Count-1; j++)
                {
                    x = alternatifYollar.ElementAt(i).ElementAt(j-1).X + (float)(alternatifRotalar.ElementAt(i).t[j-1] *
                        gemiler.ElementAt(0).hiz * Math.Cos((rota + 90) * Math.PI / 180));
                    y = alternatifYollar.ElementAt(i).ElementAt(j-1).Y + (float)(alternatifRotalar.ElementAt(i).t[j-1]  *
                        gemiler.ElementAt(0).hiz * -Math.Sin((rota + 90) * Math.PI / 180));

                    rota -= alternatifRotalar.ElementAt(i).co[j-1];
                    alternatifYollar.ElementAt(i).RemoveAt(j);
                    alternatifYollar.ElementAt(i).Insert(j, new PointF(x, y));
                }

                alternatifYollar.ElementAt(i).Remove(alternatifYollar.ElementAt(i).Last());
                x = alternatifYollar.ElementAt(i).Last().X + (float)(r *
                         Math.Cos((rota + 90) * Math.PI / 180));
                y = alternatifYollar.ElementAt(i).Last().Y + (float)(r *
                         -Math.Sin((rota + 90) * Math.PI / 180));
                
                alternatifYollar.ElementAt(i).Add(new PointF(x, y));

                /*
                    x = gemiler.ElementAt(0).merkez.X - (float)((alternatifRotalar.ElementAt(i).t[0] - this.rota.t[0]) *
                    gemiler.ElementAt(0).hiz * Math.Cos((rota + 90) * Math.PI / 180));
                y = gemiler.ElementAt(0).merkez.Y - (float)((alternatifRotalar.ElementAt(i).t[0] - this.rota.t[0]) *
                    gemiler.ElementAt(0).hiz * -Math.Sin((rota + 90) * Math.PI / 180));
                alternatifYollar.ElementAt(i).RemoveAt(0);
                alternatifYollar.ElementAt(i).Insert(0,new PointF(x,y));
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                    {
                        x = alternatifYollar.ElementAt(i).ElementAt(0).X + (float)(alternatifRotalar.ElementAt(i).t[j] * gemiler.ElementAt(0).hiz
                            * Math.Cos((rota + 90) * Math.PI / 180));
                        y = alternatifYollar.ElementAt(i).ElementAt(0).Y + (float)(alternatifRotalar.ElementAt(i).t[j] * gemiler.ElementAt(0).hiz
                            * -Math.Sin((rota + 90) * Math.PI / 180));
                    }
                    else
                    {
                        x = x_yedek + (float)(alternatifRotalar.ElementAt(i).t[j] * gemiler.ElementAt(0).hiz
                            * Math.Cos((rota + 90) * Math.PI / 180));
                        y = y_yedek + (float)(alternatifRotalar.ElementAt(i).t[j] * gemiler.ElementAt(0).hiz
                            * -Math.Sin((rota + 90) * Math.PI / 180));
                    }
                    x_yedek = x;
                    y_yedek = y;
                    rota -= alternatifRotalar.ElementAt(i).co[j];
                    alternatifYollar.ElementAt(i).RemoveAt(j + 1);
                    alternatifYollar.ElementAt(i).Insert(j + 1, new PointF(x, y));
                }
                x = x_yedek + (float)(r * Math.Cos((rota + 90) * Math.PI / 180));
                y = y_yedek + (float)(r * -Math.Sin((rota + 90) * Math.PI / 180));

                alternatifYollar.ElementAt(i).Remove(alternatifYollar.ElementAt(i).Last());
                alternatifYollar.ElementAt(i).Add(new PointF(x, y));
                //points.Clear();*/
            }

        }
        void alternatifYollariCiz()
        {
            Pen pen = new Pen(Color.Green, 1);
            PointF[] points;
            
            for (int i = 1; i < alternatifYollar.Count; i++)
            {
                points = alternatifYollar.ElementAt(i).ToArray();
                g.DrawLines(pen, points);
            }
            Pen opt = new Pen(Color.Purple,2);

            points = alternatifYollar.ElementAt(0).ToArray();
            //if (rota.t[2] > 0)
                g.DrawLines(opt, points);
        }

        void ikiGemiCizgi()
        {
            cizimKonumu.X = this.Width / 2;
            cizimKonumu.Y = this.Height / 2;

            g.DrawLine(new Pen(Color.Purple,2), gemiler.ElementAt(0).merkez.X + cizimKonumu.X, gemiler.ElementAt(0).merkez.Y + cizimKonumu.Y
                , gemiler.ElementAt(1).merkez.X + cizimKonumu.X, gemiler.ElementAt(1).merkez.Y + cizimKonumu.Y);
        }

        public double DcpaHesapla(Gemi gemi1,Gemi gemi2)
        {
            gemi1.merkez.X += float.Parse((gemi1.hiz
                   * Math.Cos((gemi1.rota + 90) * Math.PI / 180)).ToString());
            gemi1.merkez.Y += float.Parse((gemi1.hiz
                   * -Math.Sin((gemi1.rota + 90) * Math.PI / 180)).ToString());

            gemi2.merkez.X += float.Parse((gemi2.hiz
                   * Math.Cos((gemi2.rota + 90) * Math.PI / 180)).ToString());
            gemi2.merkez.Y += float.Parse((gemi2.hiz
                   * -Math.Sin((gemi2.rota + 90) * Math.PI / 180)).ToString());

            return  Math.Sqrt(Math.Pow((gemi1.merkez.X - gemi2.merkez.X), 2) + Math.Pow((gemi1.merkez.Y - gemi2.merkez.Y), 2));
        }

        //Çatışma riski kontrolü
        public Cpa SimuleEt(Gemi gemi1, Gemi gemi2)
        {

            //CPA noktaları
            PointF cpaOld = new PointF();
            PointF cpaNew = new PointF();

            //DCPA
            double dcpaOld = Int16.MaxValue;
            double dcpaNew = Int16.MaxValue-1; //While şartına girmesi için dcpaOld'dan küçük

            PointF gemi1Merkez, gemi2Merkez;

            //Merkezler değişeceği için yedeklendi
            gemi1Merkez = gemi1.merkez;
            gemi2Merkez = gemi2.merkez;

            while (dcpaNew < dcpaOld)
            {
                dcpaOld = dcpaNew;
                cpaOld = cpaNew;

                dcpaNew = DcpaHesapla(gemi1,gemi2);
                cpaNew = gemi1.merkez;
            }
            gemi1.merkez = gemi1Merkez;
            gemi2.merkez = gemi2Merkez;

            Cpa cpa = new Cpa();
            cpa.cpa = cpaOld;
            cpa.dcpa = dcpaOld;

            return cpa;
        }

        private bool catismaRiskiVarMi(Cpa cpa, Gemi gemi)
        {
            bool catismaRiski = false;

            if (cpa.dcpa < gemi.emniyet_alani / 2 )
                catismaRiski = true;

            return catismaRiski;
        }

        private void renkDegisimi(object sender, EventArgs e)
        {

            label1.Text = "%" + progressBar1.Value ;
        }
        private void GenetikHesapla()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            GeneticAlgorithm ga = new GeneticAlgorithm(Convert.ToInt32(textBox1.Text), Convert.ToDouble(textBox2.Text),
                Convert.ToDouble(textBox3.Text), Convert.ToInt32(textBox4.Text));
            ga.SetForm(xx);
            ga.kromozonYarat();
            progressBar1.Maximum = ga.iterasyonSayisi;
            progressBar1.Value = 0;


             List<Task> tasks = new List<Task>();
             for (int i = 0; i < ga.iterasyonSayisi; i++)
                 {
                     var t = Task.Run(() =>
                     {
                         progressBar1.Value++;
                         LoadingForm.progressUpdate(i);
                        // Console.WriteLine("Task thread ID: {0}",
                                     //   Thread.CurrentThread.ManagedThreadId);
                         ga.hesapla();
                     });
                 tasks.Add(t);
                t.Wait();
            }
         /*   Action[] action = new Action[ga.iterasyonSayisi];
            for (int i = 0; i < ga.iterasyonSayisi; i++)
            {
                action[i] = () => {
                    progressBar1.Value++;
                    ga.hesapla();
                };
            }
            

            Parallel.Invoke(
            action);


            */

           //  Task.WaitAll(tasks.ToArray());

            // foreach (Task tt in tasks)
            //Console.WriteLine("Task {0} Status: {1}", tt.Id, tt.Status);
            // t.RunSynchronously();
            // t.Wait();
            rota = ga.SahteGenetik(ga.optimumKromozon);

            if (progressBar1.Value == ga.iterasyonSayisi)
            {
                bilemedim();
                stopWatch.Stop();
                Console.WriteLine(stopWatch.ElapsedMilliseconds);
            } 
        }

        private void bilemedim()
        {
            GeneticAlgorithm.rotalar = GeneticAlgorithm.rotalar.OrderBy(q => q.fitness).ToList();
            rota = GeneticAlgorithm.rotalar.ElementAt(0).Klonla();
            if (catismaVar)
                alternatifYollariBelirle();
            label1.Text = "Hesaplandı!";
        }

        static void BasicAction()
        {

            Console.WriteLine("Method=alpha, Thread={0}", Thread.CurrentThread.ManagedThreadId);
        }
        private int mevcutHizAraligiDurumu()
        {
            for(int i=0;i<gemiler.ElementAt(0).gemiHizAraliklari.GetLength(0);i++)
            {

                if (gemiler.ElementAt(0).gemiHizAraliklari[i, 0] <= gemiler.ElementAt(0).yedek_hiz &&
                    gemiler.ElementAt(0).gemiHizAraliklari[i, 1] >= gemiler.ElementAt(0).yedek_hiz)
                    return i;
            }
            return -1;
        }

        private int ustHizLimitiGetir(int mevcut)
        {
            if (gemiler.ElementAt(0).gemiHizAraliklari.GetLength(0) > mevcut + 1)
                return mevcut + 1;
            return mevcut;
        }
        private int altHizLimitiGetir(int mevcut)
        {
            if (0 <= mevcut - 1)
                return mevcut - 1;
            return mevcut;
        }
        private void hizDegisikligiIleKacinma()
        {
            int mevcutHizDurumu = mevcutHizAraligiDurumu();
            int maxHiz = 0;
            int minHiz = 0;
            for (int i = (int)gemiler.ElementAt(0).yedek_hiz+1; i < gemiler.ElementAt(0).gemiHizAraliklari[altHizLimitiGetir(mevcutHizDurumu),1]; i++)
            {
                
                Gemi gemi1 = gemiler.ElementAt(0).Clone();
                gemi1.yedek_hiz = i;
                gemi1.hiz = gemi1.yedek_hiz * (1.0f * timer1.Interval / hizOran) * trackBar1.Value;
                Cpa cpa = SimuleEt(gemi1, gemiler.ElementAt(1));
                if (!catismaRiskiVarMi(cpa, gemi1))
                {
                    maxHiz = i;
                    break;
                }
            }

            for (int i = (int)gemiler.ElementAt(0).yedek_hiz-1; i > gemiler.ElementAt(0).gemiHizAraliklari[ustHizLimitiGetir(mevcutHizDurumu), 1]; i--)
            {
                 
                    Gemi gemi1 = gemiler.ElementAt(0).Clone();
                    gemi1.yedek_hiz = i;
                    gemi1.hiz = gemi1.yedek_hiz * (1.0f * timer1.Interval / hizOran) * trackBar1.Value;
                    Cpa cpa = SimuleEt(gemi1, gemiler.ElementAt(1));
                    if (!catismaRiskiVarMi(cpa, gemi1))
                    {
                        minHiz = i;
                    break;
                    }

            }

            DialogBox db = new DialogBox(minHiz, (int)gemiler.ElementAt(0).yedek_hiz, maxHiz);
            db.ShowDialog();

        } 
        private void button3_Click(object sender, EventArgs e)
        {
            
            if (veriOnayla)
            {

                Cpa cpa = SimuleEt(gemiler.ElementAt(0), gemiler.ElementAt(1));

                if (catismaRiskiVarMi(cpa, gemiler.ElementAt(0)) && hizForm.isChecked())
                {
                    hizDegisikligiIleKacinma();
                    olceklendir();
                }

                cpa = SimuleEt(gemiler.ElementAt(0), gemiler.ElementAt(1));
                if (catismadanKaciliyor && catismaRiskiVarMi(cpa, gemiler.ElementAt(0)))
                {
                    label1.Visible = true;
                    progressBar1.Visible = true;
                    LoadingForm loadingForm = new LoadingForm(Convert.ToInt32(textBox4.Text));
                    loadingForm.Show();
                    MessageBox.Show("ÇATIŞMA RİSKİ SÖZ KONUSUDUR..!\n"+ durumKontrolu(gemiler.ElementAt(0), 
                        gemiler.ElementAt(1)) + "\n"+cpa.dcpa/trackBar1.Value);                    
                    catismaVar = true;
                    GenetikHesapla();
                    catismadanKaciliyor = false;
                }

                timer1.Interval = 100;
                timer1.Enabled = !timer1.Enabled;

                if (timer1.Enabled == true)
                {
                    button3.Text = "Durdur";
                    button3.BackColor = Color.Red;
                }
                else
                {
                    button3.Text = "Devam";
                    button3.BackColor = Color.Green;
                }
                if (!button3.Text.Equals("Devam"))
                {

                }
            }
            else
            {
                MessageBox.Show("Öncelikle Verileri Onaylayın");
            }
        }
       public  static AnaEkran xx;
     
        private void button6_Click(object sender, EventArgs e)
        {
            if (!button3.Text.Equals("Durdur"))
            {
         
                DialogResult result = MessageBox.Show("Temizlemek İstediğinizden Emin Misiniz ??", "Onaylama", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    temizle();
                   // Yenile();
                }
            }
            else
            {
                MessageBox.Show("Bu İşlem İçin Program Durdurulmalı");
            }

      }
        public void temizle()
        {
            
            for (int i = 0; i < gemiler.Count; i++)
                gemiler.ElementAt(i).pb.Dispose();
            
            
            gemiGirdileriEkraniItemsDuzenle();
          
            gemiler.Clear();
            catismaVar = false;
            veriOnayla = false;
            GeneticAlgorithm.rotalar.Clear();
            button3.Text = "Başlat";
            alternatifYollar.Clear();
            points.Clear();
            index = 0;
            label1.Text = "Hesaplanıyor...";
            label1.Visible = false;
            progressBar1.Value = 0;
            progressBar1.Visible = false;
            catismadanKaciliyor = true;
            trackBar1.Value = 20;
            trackBarEskiDeger = trackBar1.Value;
            trackBar1.Enabled = true;
            label15.Text = "Ölçek : 1 mil = " + trackBar1.Value + " px";
            //Form1.ActiveForm.BackColor = SystemColors.ControlLight;//Sadece Control a boyadıgımız zaman degisik yapmıyordu.Bizde once farklı bir renge boyadık sonrasında default renk olan control rengine boyadık.
            //Form1.ActiveForm.BackColor = SystemColors.Control;
            this.BackColor = SystemColors.ControlDark;
            this.BackColor = SystemColors.Control;

            guncellemeEkrani.temizle();
            /*
            xx.Controls.Clear();
            Form1 ff1 = new Form1();
            for (int i = 0; i < ff1.Controls.Count;)
                xx.Controls.Add(ff1.Controls[i]);*/
            
        }
        private void gemiGirdileriEkraniItemsDuzenle()
        {
            gemiGirdileriEkrani.Controls["button1"].Text = "Kendi Gemimizi Oluştur";
            gemiGirdileriEkrani.hideItems();
            TextBox f1t1 = (TextBox)gemiGirdileriEkrani.Controls["textbox1"];
            f1t1.ReadOnly = false;
        }
        public void SaveFunc()
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
                saveLoad.Save(s, gemiler);
            }
        }

        public void RaporCikart()
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.Filter = "Data Files (*.xls)|*.xls";
            sv.DefaultExt = "xls";
            sv.AddExtension = true;
            sv.ShowDialog();

            String s = sv.FileName;

            if (!s.Equals(""))
            {
                SaveLoad saveLoad = new SaveLoad();
                saveLoad.RaporSave(s,GeneticAlgorithm.rotalar );
            }
        }
        public void LoadFunc()
        {
            
            OpenFileDialog op = new OpenFileDialog();
            op.ShowDialog();
            String secilenDizin = op.FileName.ToString();

            if (!secilenDizin.Equals(""))
            {
                temizle();
                SaveLoad saveLoad = new SaveLoad();
                float[,] veriler = saveLoad.Load(secilenDizin);
                for (int i = 0; i < veriler.GetLength(0); i++)
                {
                    PointF p = new PointF();
                    p.X = veriler[i, veriler.GetLength(1) - 2];
                    p.Y = veriler[i, veriler.GetLength(1) - 1];

                    setVeriler((int) veriler[i, 0], veriler[i, 1], (int) veriler[i, 2], p);
                }
            }
        }
        public void LoadFuncKerteriz()
        {
            
            OpenFileDialog op = new OpenFileDialog();
            op.ShowDialog();
            if (op.FileName.Equals(null))
            {
                AnaEkran.xx.temizle();
            }
            
            String secilenDizin = op.FileName.ToString();

            if (!secilenDizin.Equals(""))
            {
                AnaEkran.xx.temizle();
                SaveLoad saveLoad = new SaveLoad();
                float[,] veriler = saveLoad.Load(secilenDizin);

                PointF p = new PointF();
                /* p.X = veriler[0, veriler.GetLength(1) - 2];
                 p.Y = veriler[0, veriler.GetLength(1) - 1];
                 */

                 p.X = AnaEkran.xx.Width / 2;
                 p.Y = AnaEkran.xx.Height / 2;
                 
                //Form1.setVeriler((int)veriler[0, 0] / Form1.katSayi, (int)veriler[0, 1], (int)-veriler[0, 2], p);
                AnaEkran.setVeriler((int)veriler[0, 0], veriler[0, 1], (int)-veriler[0, 2], p);
                float kerterizAcisi = 1;
                float uzaklik = 1;
                for (int i = 1; i < veriler.GetLength(0); i++)
                {
                    //PointF p = new PointF();
                    /* p.X = veriler[i, veriler.GetLength(1) - 2];
                     p.Y = veriler[i, veriler.GetLength(1) - 1];*/

                    kerterizAcisi = -veriler[i, veriler.GetLength(1) - 2];
                    uzaklik = veriler[i, veriler.GetLength(1) - 1];
                    

                    p.X = AnaEkran.gemiler.ElementAt(0).merkez.X + float.Parse(((uzaklik *
                            trackBar1.Value)* Math.Cos((AnaEkran.gemiler.ElementAt(0).rota + kerterizAcisi + 90) * Math.PI / 180)).ToString());


                    p.Y = AnaEkran.gemiler.ElementAt(0).merkez.Y + float.Parse(((uzaklik *
                            trackBar1.Value)* -Math.Sin((AnaEkran.gemiler.ElementAt(0).rota + kerterizAcisi + 90) * Math.PI / 180)).ToString());

                    /*   p.X = veriler[i, veriler.GetLength(1) - 2];
                       p.Y = veriler[i, veriler.GetLength(1) - 1];*/

                    //Form1.setVeriler((int)veriler[i, 0] / Form1.katSayi, (int)veriler[i, 1], (int)-veriler[i, 2], p);
                    AnaEkran.setVeriler((int)veriler[i, 0], veriler[i, 1], (int)-veriler[i, 2], p);
                    AnaEkran.gemiler.ElementAt(i).bizimGemiyeUzaklik = uzaklik;
                    AnaEkran.gemiler.ElementAt(i).yedek_bizimGemiyeUzaklik = uzaklik;
                    AnaEkran.gemiler.ElementAt(i).kerterizAcisi = kerterizAcisi;
                    //Console.WriteLine("Zero : "+(ikiNoktaArasiUzaklikHesabi(gemiler.ElementAt(0), Form1.gemiler.ElementAt(i)) / trackBar1.Value));
                }
            }
            AnaEkran.xx.Yenile();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (button3.Text == "Durdur")
            {
                MessageBox.Show("Bu İşlem İçin Program Durdurulmalı");
            }
            else
            {
                LoadFuncKerteriz();
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SaveFunc();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            RaporCikart();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
          //  xx = this;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
        
        

        private void trackBar1_ValueChanged_1(object sender, EventArgs e)
        {
            //olceklendir();
            label15.Text = "Ölçek : 1 mil = " + trackBar1.Value + " px";
        }

        private void trackBar1_MouseDown(object sender, MouseEventArgs e)
        {
            trackBarEskiDeger = trackBar1.Value;
            timer1.Enabled = false;
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            olceklendir();
            //Yenile();
            if(button3.Text.Equals("Durdur"))
            timer1.Enabled = true;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            MessageBox.Show("Bu Yazılım Barış Elçiseven ve Mehmet Furkan Atlı Tarafından COLREG Kurallarına Uygun Olarak Hazırlanmıştır.");


           /* PointF[] pf = { new PointF(500, 500),
                new PointF(500, 400),new PointF(600, 300),
                new PointF(500, 250),new PointF(500, 200)
            };
            PointF[] pf2 = { new PointF(500, 500),
                new PointF(515, 400),new PointF(585, 300),
                new PointF(515, 250),new PointF(500, 200)
            };
            g.DrawCurve(new Pen(Color.Red, 1), pf2);
            g.DrawLines(new Pen(Color.Blue, 1), pf);*/
        }
    }
}
