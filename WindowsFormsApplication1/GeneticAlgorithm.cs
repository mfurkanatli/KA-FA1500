using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using System.Threading;
namespace WindowsFormsApplication1
{
    public class GeneticAlgorithm
    {
        List<Kromozom> ebeveynler = new List<Kromozom>();
        List<Kromozom> cocuklar = new List<Kromozom>();
        List<Kromozom> eslesmeHavuzu = new List<Kromozom>();
        Rota optimumRota = new Rota();
        public Kromozom optimumKromozon = new Kromozom();
        public static int populasyonSayisi = 20;
        int bitSayisi = 8;
        public double crossOverRate = 0.6;
        public static double mutationRate = 0;
        public int iterasyonSayisi = 100;
        double[] ruletOlasilik;
        int[] iterasyon = new int[100];
        long[] sure = new long[100];
        Random rand = new Random();
        Form1 f1;
        static public List<Rota> rotalar = new List<Rota>();
        
        public GeneticAlgorithm(int pop, double crossR,double mut, int it)
        {
            populasyonSayisi = pop;
            crossOverRate = crossR;
            mutationRate = mut;
            iterasyonSayisi = it;
            ruletOlasilik = new double[populasyonSayisi + 1];
            
        }
   
        public void SetForm(Form1 _f1)
        {
            f1 = _f1;
        }

        bool BireyUygunMu(Kromozom birey)
        {
            Gemi gemi1 = new Gemi(Form1.gemiler.ElementAt(0).emniyet_alani, Form1.gemiler.ElementAt(0).hiz, 
                -Form1.gemiler.ElementAt(0).rota, Form1.gemiler.ElementAt(0).merkez,f1);
            Gemi gemi2 = new Gemi(Form1.gemiler.ElementAt(1).emniyet_alani, Form1.gemiler.ElementAt(1).hiz,
                -Form1.gemiler.ElementAt(1).rota, Form1.gemiler.ElementAt(1).merkez, f1);

            /*gemi1.pb.Visible = false;
            gemi2.pb.Visible = false;*/

            Rota rota = new Rota();

            birey.outputDegerHesapla();

            rota.co[0] = birey.co1_deger;
            rota.co[1] = -birey.co2_deger;
            rota.co[2] = birey.co3;

            rota.t[0] = birey.t1_deger;
            rota.t[1] = birey.t2_deger;
            rota.t[2] = Math.Round(birey.t3);


            for(int index=0;index<3;index++)
            {
                for(int j=0;j<rota.t[index];j++)
                {
                    if (f1.DcpaHesapla(gemi1, gemi2) < gemi1.emniyet_alani / 2)
                        return false;

                }
                gemi1.rota -= (int) (rota.co[index]);
            }

            if (f1.SimuleEt(gemi1, gemi2).dcpa < gemi1.emniyet_alani / 2)
                return false;


            return true;
        }

        public Rota SahteGenetik(Kromozom min)
        {
            Rota rota = new Rota();
            //kromozonYarat();
            //Kromozom kro=minimumBul(ebeveynler);
            rota.co[0] = min.co1_deger;
            rota.co[1] = -min.co2_deger;
            rota.co[2] = min.co3;

            rota.t[0] = min.t1_deger;
            rota.t[1] = min.t2_deger;
            rota.t[2] = min.t3;
            rota.fitnessHesapla();
            /*
            Console.WriteLine("ROTAMIZ");

            for (int index = 0; index < 3; index++)
            {
                Console.Write(rota.t[index] + " : " + rota.co[index] + " / ");
            }
            */
            return rota;
        }

        public bool rotaFarkliMi(Rota gelen)
        {
            for (int i = 0; i < rotalar.Count; i++) 
            {
                if (rotalar.ElementAt(i).fitness == gelen.fitness)
                    return false;
            }
            return true;
        }

        void alternatifRotalariTut(List<Kromozom> gelen)
        {
            List<Rota> yedekRota = new List<Rota>();
            for (int i = 0; i < gelen.Count; i++)
                if (rotaFarkliMi(SahteGenetik(gelen.ElementAt(i))))
                    rotalar.Add(SahteGenetik(gelen.ElementAt(i)));

        }
        public void kromozonYarat()
        {
            //for (int i = 0; i < populasyonSayisi; i++)
            int i=0;
            while(ebeveynler.Count < populasyonSayisi)
            {
                ebeveynler.Add(new Kromozom());
                i = ebeveynler.Count-1;
                
                        ebeveynler.ElementAt(i).t1=bireyOlustur(ebeveynler.ElementAt(i).t1);
                        ebeveynler.ElementAt(i).t2 = bireyOlustur(ebeveynler.ElementAt(i).t2);
                        ebeveynler.ElementAt(i).co1 = bireyOlustur(ebeveynler.ElementAt(i).co1);
                        ebeveynler.ElementAt(i).co2 = bireyOlustur(ebeveynler.ElementAt(i).co2);

                if (!BireyUygunMu(ebeveynler.ElementAt(i)))
                {
                    ebeveynler.RemoveAt(i);
                }
            }

            optimumKromozon.fitness = 3000;
        }
        public string[] bireyOlustur(string[] s)
        {
            string[] s1 = new string[s.Length];
            for (int j = 0; j < s.Length; j++)
            {
                s1[j] = rand.Next(0, 2).ToString();
            }
            return s1;
        }
        void yazdir()
        {
            /*
            for (int i = 0; i < populasyonSayisi; i++)
            {
                //richTextBox1.Text += "////";
                Console.Write("///");
                inputYazdir(i, ebeveynler.ElementAt(i).co1);
                // richTextBox1.Text += "////";
                Console.Write("///");
                inputYazdir(i, ebeveynler.ElementAt(i).t1);
                //richTextBox1.Text += "////";
                Console.Write("///");
                inputYazdir(i, ebeveynler.ElementAt(i).co2);
                //richTextBox1.Text += "////";
                Console.Write("///");
                inputYazdir(i, ebeveynler.ElementAt(i).t2);
                //richTextBox1.Text += "\n";
                Console.Write("\n");
            }*/
        }

        void inputYazdir(int i, string[] gelen)
        {
            for (int j = 0; j < gelen.Count(); j++)
            {
                Console.Write(gelen[j] + " ");
            }
            Console.Write(" ; " + ebeveynler.ElementAt(i).degerHesapla2(gelen) + " ; " + ebeveynler.ElementAt(i).fitness + "\n");
        }
        void degerleriHesapla(List<Kromozom> gelen)
        {
            for (int i = 0; i < populasyonSayisi; i++)
            {
             //   gelen.ElementAt(i).degerHesapla();
                gelen.ElementAt(i).outputDegerHesapla();//sonradan eklendi
            }
        }
        int fitnessMinBul(List<Kromozom> gelen)
        {
            Kromozom min = new Kromozom();
            min = ebeveynler.ElementAt(0);
            for (int i = 0; i < populasyonSayisi; i++)
            {
                if (ebeveynler.ElementAt(i).fitness < min.fitness)
                    min = ebeveynler.ElementAt(i);
            }
            return min.fitness;

        }
        void fitnessHesapla(List<Kromozom> gelen)
        {
            for (int i = 0; i < populasyonSayisi; i++)
            {
                gelen.ElementAt(i).fitnessHesapla();
            }

            int min = fitnessMinBul(gelen);
            if (min < 0)
            {
                for (int i = 0; i < populasyonSayisi; i++)
                {
                    gelen.ElementAt(i).fitness += Math.Abs(min) + 1;
                }
            }
        }
        void ruletCarki()
        {
            double terslerinToplami = 0; // mininmum için 1/f(x) toplamı
            ruletOlasilik[0] = 0;
            for (int i = 0; i < populasyonSayisi; i++)
            {
                terslerinToplami += (1.0 / ebeveynler.ElementAt(i).fitness);
                ebeveynler.ElementAt(i).olasilik = 1.0 / ebeveynler.ElementAt(i).fitness;
            }
            for (int i = 0; i < populasyonSayisi; i++)
            {
                ebeveynler.ElementAt(i).olasilik /= terslerinToplami;
                ruletOlasilik[i + 1] = ruletOlasilik[i] + ebeveynler.ElementAt(i).olasilik;
            }
        }
        int indisAl(double gelen)
        {
            for (int i = 0; i < populasyonSayisi; i++)
            {
                if (ruletOlasilik[i] <= gelen && gelen < ruletOlasilik[i + 1])
                    return i;
            }
            return 3;
        }
        void eslesmeHavuzuBelirle()
        {
            double random;
            //  Console.WriteLine(calismaSuresi + "\n");
            for (int i = 0; i < populasyonSayisi; i++)
            {
                random = rand.NextDouble();
                eslesmeHavuzu.Add(ebeveynler.ElementAt(indisAl(random))); //random üretilen sayı hangi indis aralığındaysa onu eşleşme havuzuna ekle
                //eslesmeHavuzu.Add(ebeveynler.ElementAt(i));
            }

        }

        void ElitistReplacement()
        {
            List<Kromozom> li = new List<Kromozom>();
            for(int i=0;i<populasyonSayisi;i++)
            {
                if(!li.Contains(cocuklar.ElementAt(i))  && BireyUygunMu(cocuklar.ElementAt(i)))
                {
                    li.Add(cocuklar.ElementAt(i));
                }
            }
            int hedef = populasyonSayisi - li.Count;
            for (int i = 0; i <hedef; i++)
            {
                
                li.Add(minimumBul(ebeveynler));
                ebeveynler.Remove(li.Last());                
            }
            
            generationReplacement(li);

        }

        int rastgeleIndisSec(int baslangic)
        {
            double rastgele;
            for (int i = baslangic; i < bitSayisi; i++)
            {
                rastgele = rand.NextDouble();
                if (crossOverRate >= rastgele)
                    return i;
            }
            return baslangic;
        }
        void crossOver()
        {
            int r1, r2, breakPoint;
            while (eslesmeHavuzu.Count != 0)
            {
                do
                {
                    r1 = rand.Next(0, eslesmeHavuzu.Count); //rastgele ebeyn 1
                    r2 = rand.Next(0, eslesmeHavuzu.Count); //rastgele ebeyn 2
                } while (r1 == r2 && eslesmeHavuzu.ElementAt(r1).Equals(eslesmeHavuzu.ElementAt(r2)));
                breakPoint = rastgeleIndisSec(0);

                cocuklar.Add(Kromozom.onePointCrossover(eslesmeHavuzu.ElementAt(r1), eslesmeHavuzu.ElementAt(r2), breakPoint));
                cocuklar.Add(Kromozom.onePointCrossover(eslesmeHavuzu.ElementAt(r2), eslesmeHavuzu.ElementAt(r1), breakPoint));
                if (r1 > r2)
                {
                    eslesmeHavuzu.RemoveAt(r1);
                    eslesmeHavuzu.RemoveAt(r2);
                }
                else
                {
                    eslesmeHavuzu.RemoveAt(r2);
                    eslesmeHavuzu.RemoveAt(r1);
                }
            }
        }
        //swap mutation
        void mutasyonIslemi()
        {

            for (int i = 0; i < populasyonSayisi; i++)
            {
                // cocuklar.ElementAt(i)=Kromozom.mutasyonIslemi(cocuklar.ElementAt(i));
                Kromozom kromozom = cocuklar.ElementAt(i);
                kromozom = Kromozom.mutasyonIslemi(kromozom);
                cocuklar.RemoveAt(i);
                cocuklar.Insert(i, kromozom);
            }
        }
        Kromozom minimumBul(List<Kromozom> gelen)
        {
            Kromozom min = new Kromozom();
            min.fitness = int.MaxValue;
            for (int i = 0; i < gelen.Count; i++)
            {
                if (gelen.ElementAt(i).fitness <= min.fitness)
                {
                    min = gelen.ElementAt(i);
                }
            }
            return min;
        }

        void generationReplacement(List<Kromozom> yeniNesil)
        {
            ebeveynler.Clear();
            for (int i = 0; i < populasyonSayisi; i++)
            {
                ebeveynler.Add(yeniNesil.ElementAt(i));
            }
            yeniNesil.Clear();
            cocuklar.Clear();
        }
        Kromozom GlobalOptimumFitness(Kromozom min,List<Kromozom> list)
        {
            Kromozom k = minimumBul(list);
            if (min.fitness > k.fitness)
                return k;
            return min;

        }
        public void hesapla()
        {
            //calismaSuresi++;
            degerleriHesapla(ebeveynler);
            fitnessHesapla(ebeveynler);
            alternatifRotalariTut(ebeveynler);
            optimumKromozon = GlobalOptimumFitness(optimumKromozon, ebeveynler);
            ruletCarki();
            eslesmeHavuzuBelirle();
            crossOver();
            mutasyonIslemi();
            ElitistReplacement();
            yazdir();
        }

        void sifirla()
        {
            ebeveynler.Clear();
            cocuklar.Clear();
            //richTextBox1.Clear();
            eslesmeHavuzu.Clear();
            for (int i = 0; i < ruletOlasilik.Length; i++) ruletOlasilik[i] = 0;

        }
        void ortalamaHesapla()
        {
            long toplamSure = 0;
            int toplamIterasyon = 0;
            for (int i = 0; i < iterasyonSayisi; i++)
            {
                toplamSure += sure[i];
                toplamIterasyon += iterasyon[i];
            }
            toplamSure /= sure.Length;
            toplamIterasyon /= iterasyon.Length;
            /*richTextBox1.Text += toplamSure + "\n";
            richTextBox1.Text += toplamIterasyon + "\n";*/

        }

    }
}
