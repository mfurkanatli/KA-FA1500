using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Kromozom
    {
        public int deger;
        public int fitness;
        public double olasilik = 0;
        public string[] binaryCode = new string[6];
        public string[] t1 = new string[6];
        public string[] co1 = new string[6];
        public string[] t2 = new string[6];
        public string[] co2 = new string[6];
        double t1_deger, t2_deger, co1_deger, co2_deger;


        static Random rnd = new Random();

        public void fitnessHesapla()
        {
            fitness = deger * deger + 22 * deger;
        }
        public void degerHesapla()
        {
            deger = 0;
            deger -= Convert.ToInt32(binaryCode[0]) * Convert.ToInt32(Math.Pow(2, 7));
            for (int i = 1; i < binaryCode.Length; i++)
            {
                deger += Convert.ToInt32(binaryCode[i]) * Convert.ToInt32(Math.Pow(2, 7 - i));
            }
        }
        public int degerHesapla2(string[] ss)
        {
            deger = 0;
            for (int i = 1; i < ss.Length; i++)
            {
                deger += Convert.ToInt32(ss[i]) * Convert.ToInt32(Math.Pow(2, ss.Length - i));
            }
            return deger;
        }
        static public Kromozom onePointCrossover(Kromozom partner1, Kromozom partner2, int breakPoint)
        {
            Kromozom cocuk = new Kromozom();

            cocuk.co1 = inputAtamaYap(partner1.co1, partner2.co1, breakPoint);
            cocuk.co2 = inputAtamaYap(partner1.co2, partner2.co2, breakPoint);
            cocuk.t1 = inputAtamaYap(partner1.t1, partner2.t1, breakPoint);
            cocuk.t2 = inputAtamaYap(partner1.t2, partner2.t2, breakPoint);
            return cocuk;
        }

        static public Kromozom mutasyonIslemi(Kromozom partner)
        {
            partner.co1 = mut(partner.co1, 0.082);
            partner.co2 = mut(partner.co2, 0.082);
            partner.t1 = mut(partner.t1, 0.082);
            partner.t2 = mut(partner.t2, 0.082);

            return partner;
        }

        private static string[] mut(string[] s, double mutationRate)
        {
            double rastgele;

            for (int j = 1; j < s.Count(); j++)
            {
                rastgele = rnd.NextDouble();
                if (rastgele <= mutationRate)
                {
                    if (s[j].Equals("1"))
                        s[j] = "0";
                    else
                        s[j] = "1";
                }
            }

            return s;
        }
        static public string[] inputAtamaYap(string[] bin, string[] bin2, int breakPoint)
        {
            breakPoint = rastgeleIndisSec(0, bin.Count(), 0.3);
            string[] binary = new string[bin.Count()];
            binary[0] = "0";
            for (int i = 1; i <= breakPoint; i++)
            {
                binary[i] = bin[i];
            }
            for (int i = breakPoint + 1; i < bin.Count(); i++)
            {
                binary[i] = bin2[i];
            }
            return binary;
        }

        static int rastgeleIndisSec(int baslangic, int bitSayisi, double crossOverRate)
        {
            double rastgele;
            for (int i = baslangic; i < bitSayisi; i++)
            {
                rastgele = Kromozom.rnd.NextDouble();
                if (crossOverRate >= rastgele)
                    return i;
            }
            return baslangic;
        }
    }
}
