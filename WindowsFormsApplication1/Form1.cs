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
        Graphics cizim;
        Gemi karsiGemi;
        Gemi bizimGemi;
        Graphics g;
        public static int katSayi = 10;
        public static List<Gemi> gemiler = new List<Gemi>();
        bool veriOnayla = false;
        int jenarasyon = 50;
        bool catismaVar = false;


        List<Point> points = new List<Point>(); //altarnatif yol noktaları
        List<List<Point>> alternatifYollar = new List<List<Point>>();
        public Form1()
        {
            InitializeComponent();
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
        
        public static void setVeriler(int emniyet_alani, int hiz, int rota, Point merkez)
        {
            gemiler.Add(new Gemi(emniyet_alani * katSayi, hiz, rota, merkez,xx));
            if (gemiler.Count>1)
            {
                gemiler.ElementAt(gemiler.Count - 1).pb.ImageLocation ="gemi3.png";
            }
        }
      

        private void Form1_Load(object sender, EventArgs e)
        {

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

                veriOnayla = true;

                bizimGemi = gemiler.ElementAt(0);
                karsiGemi = gemiler.ElementAt(1);

                for (int i = 0; i < gemiler.Count; i++)
                {
                    gemiCiz(gemiler.ElementAt(i));
                    if (i > 0)//tcp,dcp Hesaplancak
                    {
                        gemiler.ElementAt(i).tcpa = Gemi.tcpaHesapla(gemiler.ElementAt(0), gemiler.ElementAt(i));
                       // MessageBox.Show(gemiler.ElementAt(i).tcpa +"");
                        gemiler.ElementAt(i).dcpa = Gemi.dcpaHesapla(gemiler.ElementAt(0), gemiler.ElementAt(i));
                    }
                }                
            }
            else
            {
                MessageBox.Show("Yeterli Sayida Gemi Girmediniz");
            }
            
        }

    
        public void gemiKonumlandir(Gemi _gemi)
        {

            int w = this.Width;
            int h = this.Height;
            Graphics gg = this.CreateGraphics();
            Pen pen = new Pen(Color.Blue, 2);

            _gemi.merkez.X = w / 2;
            _gemi.merkez.Y = h / 2;
            gg.DrawArc(pen, _gemi.merkez.X, _gemi.merkez.Y, 10, 10, 0, 360);
        }

        public String durumKontrolu(Gemi _bizimGemi, Gemi _karsiGemi)
        {
            String s = "";
            double rota;
            if (-karsiGemi.rota > 180)
                rota = -karsiGemi.rota - 180 + bizimGemi.rota;
            else
                rota = -karsiGemi.rota + 180 + bizimGemi.rota;

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
            
            gemi.merkez.X += Convert.ToInt32(gemi.hiz
                   * Math.Cos((gemi.rota + 90) * Math.PI / 180));
            gemi.merkez.Y += Convert.ToInt32(gemi.hiz
                * -Math.Sin((gemi.rota + 90) * Math.PI / 180));
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
            cizimKonumu.X = this.Width / 2;
            cizimKonumu.Y = this.Height / 2;
                
            int r = 500;
            int x = gemi.merkez.X + Convert.ToInt32(r * Math.Cos((gemi.rota + 90) * Math.PI / 180));
            int y = gemi.merkez.Y + Convert.ToInt32(r * -Math.Sin((gemi.rota + 90) * Math.PI / 180));
           // Console.WriteLine(Math.Sin(gemi.rota * Math.PI / 180) + "");
            Point hedef = new Point(x, y);
           
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
            Point rotaKonum = new Point();
            rotaKonum.X = gemi.merkez.X + cizimKonumu.X;
            rotaKonum.Y = gemi.merkez.Y + cizimKonumu.Y;

            Point rotaHedef=new Point();
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
            for (int i = 0; i < gemiler.Count; i++)
            {
                gemiCiz(gemiler.ElementAt(i));
            }
        }

        public void gemiHareketHesapla(int _i)
        {
            gemiHareketEttir(gemiler.ElementAt(_i));
            gemiler.ElementAt(_i).pictureBoxHareketettiir();
            gemiCiz(gemiler.ElementAt(_i));
        }

        int index = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(gemiler.Count>1)
            {                
                g.Clear(this.BackColor);
                
                if(index < 3 && rota.t[index]>0)
                {
                    gemiHareketHesapla(0);
                    rota.t[index]--;
                    if (rota.t[index] <= 0)
                    {
                        gemiler.ElementAt(0).rota -= (int)(rota.co[index]);
                        index++;
                    }
                }
                else
                {
                  //  index++;
                }
                if(index > 2)
                {
                    gemiHareketHesapla(0);
                }
                for (int i = 1; i < gemiler.Count; i++)
                {
                    gemiHareketHesapla(i);
                }

                alternatifYollariCiz();            
            }            
        }
        void alternatifYollariBelirle()
        {
            int x, y;
            int x_yedek=0, y_yedek=0;
            double rota;
            for(int i=0;i<GeneticAlgorithm.rotalar.Count;i++)
            {
                points = new List<Point>();
                points.Add(new Point(gemiler.ElementAt(0).merkez.X + this.Width/2, gemiler.ElementAt(0).merkez.Y + this.Height/2));
                rota = gemiler.ElementAt(0).rota;
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                    {
                        x = gemiler.ElementAt(0).merkez.X + Convert.ToInt32(GeneticAlgorithm.rotalar.ElementAt(i).t[j] * gemiler.ElementAt(0).hiz
                            * Math.Cos((rota + 90) * Math.PI / 180));
                        y = gemiler.ElementAt(0).merkez.Y + Convert.ToInt32(GeneticAlgorithm.rotalar.ElementAt(i).t[j] * gemiler.ElementAt(0).hiz
                            * -Math.Sin((rota + 90) * Math.PI / 180));
                    }
                    else
                    {
                        x = x_yedek + Convert.ToInt32(GeneticAlgorithm.rotalar.ElementAt(i).t[j] * gemiler.ElementAt(0).hiz
                            * Math.Cos((rota + 90) * Math.PI / 180));
                        y = y_yedek + Convert.ToInt32(GeneticAlgorithm.rotalar.ElementAt(i).t[j] * gemiler.ElementAt(0).hiz
                            * -Math.Sin((rota + 90) * Math.PI / 180));
                    }
                    x_yedek = x;
                    y_yedek = y;
                    rota -= GeneticAlgorithm.rotalar.ElementAt(i).co[j];
                    points.Add(new Point(x + this.Width / 2, y + this.Height / 2));
                }

                alternatifYollar.Add(points);
                //points.Clear();
            }
        }

        void alternatifYollariCiz()
        {
            Pen pen = new Pen(Color.Green, 1);
            Point[] points;
            for (int i = 0; i < 10; i++)
            {
                points = alternatifYollar.ElementAt(i).ToArray();
                g.DrawLines(pen, points);
            }
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
            gemi1.merkez.X += Convert.ToInt32(gemi1.hiz
                   * Math.Cos((gemi1.rota + 90) * Math.PI / 180));
            gemi1.merkez.Y += Convert.ToInt32(gemi1.hiz
                * -Math.Sin((gemi1.rota + 90) * Math.PI / 180));

            gemi2.merkez.X += Convert.ToInt32(gemi2.hiz
               * Math.Cos((gemi2.rota + 90) * Math.PI / 180));
            gemi2.merkez.Y += Convert.ToInt32(gemi2.hiz
                * -Math.Sin((gemi2.rota + 90) * Math.PI / 180));

            return  Math.Sqrt(Math.Pow((gemi1.merkez.X - gemi2.merkez.X), 2) + Math.Pow((gemi1.merkez.Y - gemi2.merkez.Y), 2));
        }

        //Çatışma riski kontrolü
        public Cpa SimuleEt(Gemi gemi1, Gemi gemi2)
        {

            //CPA noktaları
            Point cpaOld = new Point();
            Point cpaNew = new Point();

            //DCPA
            double dcpaOld = Int16.MaxValue;
            double dcpaNew = Int16.MaxValue-1; //While şartına girmesi için dcpaOld'dan küçük

            Point gemi1Merkez, gemi2Merkez;

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

        private void GenetikHesapla()
        {
            GeneticAlgorithm ga = new GeneticAlgorithm();
            ga.SetForm(xx);
            ga.kromozonYarat();
            for(int i=0;i < jenarasyon; i++)
            {
                ga.hesapla();
                rota = ga.SahteGenetik(ga.optimumKromozon);
            }
            if(catismaVar)
                alternatifYollariBelirle();
        }
        private void button3_Click(object sender, EventArgs e)
        {               

            if (veriOnayla)
            {

                Cpa cpa = SimuleEt(gemiler.ElementAt(0), gemiler.ElementAt(1));
                if (catismaRiskiVarMi(cpa, gemiler.ElementAt(0)))
                {
                    MessageBox.Show("ÇATIŞMA RİSKİ SÖZ KONUSUDUR..!");
                    MessageBox.Show(durumKontrolu(gemiler.ElementAt(0), gemiler.ElementAt(1)) + "");
                    catismaVar = true;
                    GenetikHesapla();
                    /*
                    timer1 = new Timer();
                    timer1.Tick += new EventHandler(timer1_Tick);*/
                }

                timer1.Interval = 100;
                timer1.Enabled = !timer1.Enabled;

                if (timer1.Enabled == true)
                {
                    button3.Text = "Durdur";
                }
                else
                {
                    button3.Text = "Devam";
                }
                if (!button3.Text.Equals("Devam"))
                {

                }
                for (int i = 0; i < gemiler.Count; i++)
                {
                    gemiCiz(gemiler.ElementAt(i));
                }
            }
            else
            {
                MessageBox.Show("Oncelikle Verileri Onaylayin");
            }
        }
        static Form1 xx;
        private void button2_Click(object sender, EventArgs e)
        {
            xx = this;
            Form2 form2 = new Form2();
            form2.Text = "Gemi "+gemiler.Count();
            form2.Show();
        }
        
        
        private void button5_Click(object sender, EventArgs e)
        {
            if (gemiler.Count > 0)
            {                
                Form3 form3 = new Form3(this);
                form3.Show();
            }
            else
            {
                MessageBox.Show("Yeterli Sayida Gemi Yok.\nGemi Giriniz.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!button3.Text.Equals("Durdur"))
            {
         
                DialogResult result = MessageBox.Show("Temizlemek Istediginizden Emin Misiniz ??", "Onaylama", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {

                    for (int i = 0; i < gemiler.Count; i++)
                        gemiler.ElementAt(i).pb.Dispose();

                    gemiler.Clear();
                    veriOnayla = false;
                    index = 0;
                    //Form1.ActiveForm.BackColor = SystemColors.ControlLight;//Sadece Control a boyadıgımız zaman degisik yapmıyordu.Bizde once farklı bir renge boyadık sonrasında default renk olan control rengine boyadık.
                    //Form1.ActiveForm.BackColor = SystemColors.Control;
                    this.BackColor = SystemColors.ControlDark;
                    this.BackColor = SystemColors.Control;
                   // Yenile();
                }
            }
            else
            {
                MessageBox.Show("Bu Islem Icin Program Durdurulmali");
            }

      }
        public void SaveFunc()
        {
            SaveFileDialog sv = new SaveFileDialog();
            sv.ShowDialog();

            String s = sv.FileName;

            if (!s.Equals(""))
            {
                SaveLoad saveLoad = new SaveLoad();
                saveLoad.Save(s, gemiler);
            }
        }


        public void LoadFunc()
        {
            OpenFileDialog op = new OpenFileDialog();
            op.ShowDialog();
            String secilenDizin = op.FileName.ToString();

            if (!secilenDizin.Equals(""))
            {
              //  temizle();
                SaveLoad saveLoad = new SaveLoad();
                int[,] veriler = saveLoad.Load(secilenDizin);
                for (int i = 0; i < veriler.GetLength(0); i++)
                {
                    Point p = new Point();
                    p.X = veriler[i, veriler.GetLength(1) - 2];
                    p.Y = veriler[i, veriler.GetLength(1) - 1];

                    setVeriler(veriler[i, 0] / 10, veriler[i, 1], -veriler[i, 2], p);
                }
            }
        }
    }
}
