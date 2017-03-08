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
        static int populasyonSayisi = 100;
        int bitSayisi = 8;
        int optimumSonuc = -11;
        double crossOverRate = 0.6;
        double mutationRate = 0.3;
        double iterasyonSayisi = 100;
        double[] ruletOlasilik = new double[populasyonSayisi+1];
        int calismaSuresi = 0;
        int[] iterasyon = new int[100];
        long[] sure = new long[100];
        Random rand = new Random();
        Form1 f1;
        public void start()
        {            
            /*
                Stopwatch stopWatch = new Stopwatch();
                int k = 0;
                int it;
                for (int i = 0; i < iterasyonSayisi; i++)
                {
                    sifirla();
                    kromozonYarat();
                    stopWatch.Start();
                    it = 0;
                    do
                    {
                        hesapla();
                        it++;
                        //Console.WriteLine(minimumBul(ebeveynler).ToString());
                    } while (minimumBul(ebeveynler) != optimumSonuc && it < 10);
                    stopWatch.Stop();
                    sure[k] = stopWatch.ElapsedMilliseconds;
                    stopWatch.Reset();
                    iterasyon[k] = it;
                    k++;
                }
                ortalamaHesapla();
            */
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

            gemi1.pb.Visible = false;
            gemi2.pb.Visible = false;

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

        public Rota SahteGenetik()
        {
            Rota rota = new Rota();
            kromozonYarat();
            Kromozom kro=minimumBul(ebeveynler);
            rota.co[0] = kro.co1_deger;
            rota.co[1] = -kro.co2_deger;
            rota.co[2] = kro.co3;

            rota.t[0] = kro.t1_deger;
            rota.t[1] = kro.t2_deger;
            rota.t[2] = kro.t3;

            Console.WriteLine("ROTAMIZ");

            for (int index = 0; index < 3; index++)
            {
                Console.Write(rota.t[index] + " : " + rota.co[index] + " / ");
            }

            return rota;
        }

        void kromozonYarat()
        {
            //for (int i = 0; i < populasyonSayisi; i++)
            int i=0;
            while(ebeveynler.Count < populasyonSayisi)
            {
                ebeveynler.Add(new Kromozom());
                i = ebeveynler.Count-1;
                for (int j = 0; j < ebeveynler.ElementAt(i).binaryCode.Length; j++)
                {
                    if (j > 0)
                    {
                        ebeveynler.ElementAt(i).binaryCode[j] = rand.Next(0, 2).ToString();
                        ebeveynler.ElementAt(i).t1[j] = rand.Next(0, 2).ToString();
                        ebeveynler.ElementAt(i).t2[j] = rand.Next(0, 2).ToString();
                        ebeveynler.ElementAt(i).co1[j] = rand.Next(0, 2).ToString();
                        ebeveynler.ElementAt(i).co2[j] = rand.Next(0, 2).ToString();
                    }
                    else
                    {
                        ebeveynler.ElementAt(i).binaryCode[j] = rand.Next(0, 1).ToString();
                        ebeveynler.ElementAt(i).t1[j] = rand.Next(0, 1).ToString();
                        ebeveynler.ElementAt(i).t2[j] = rand.Next(0, 1).ToString();
                        ebeveynler.ElementAt(i).co1[j] = rand.Next(0, 1).ToString();
                        ebeveynler.ElementAt(i).co2[j] = rand.Next(0, 1).ToString();
                    }
                }
                if(!BireyUygunMu(ebeveynler.ElementAt(i)))
                {
                    ebeveynler.RemoveAt(i);
                }
            }
        }

        void yazdir()
        {/*
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
        {/*
            for (int j = 0; j < gelen.Count(); j++)
            {
                Console.Write(gelen[j] + " ");
            }
            Console.Write(" ; " + ebeveynler.ElementAt(i).degerHesapla2(gelen) + " ; " + ebeveynler.ElementAt(i).fitness + "\n");*/
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
            for (int i = 0; i < populasyonSayisi; i++)
            {
                if (gelen.ElementAt(i).fitness <= min.fitness)
                {
                    min = gelen.ElementAt(i);
                }
            }
            return min;
        }

        void generationReplacement()
        {
            ebeveynler.Clear();
            for (int i = 0; i < populasyonSayisi; i++)
            {
                ebeveynler.Add(cocuklar.ElementAt(i));
            }
            cocuklar.Clear();
        }

        void hesapla()
        {
            //calismaSuresi++;
            degerleriHesapla(ebeveynler);
            fitnessHesapla(ebeveynler);
            ruletCarki();
            eslesmeHavuzuBelirle();
            crossOver();
            mutasyonIslemi();
            yazdir();
            generationReplacement();
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
