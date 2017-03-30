using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace WindowsFormsApplication1
{
    class SaveLoad
    {
        public int num_rows, num_cols;
        public String[,] dataset;
        public void Save(string filename, List<Gemi> gemiler)
        {
            string path = @"" + filename;
            string veriler = "";
            StreamWriter sW = new StreamWriter(path);
            for (int i = 0; i < gemiler.Count; i++)
            {
                veriler += gemiler.ElementAt(i).yedek_emniyet_alani + ";" + gemiler.ElementAt(i).yedek_hiz + ";" + gemiler.ElementAt(i).rota + ";" + gemiler.ElementAt(i).merkez.X + ";" + gemiler.ElementAt(i).merkez.Y;
                sW.WriteLine(veriler);
                veriler = "";
            }

            sW.Close();
            string fullPath = Path.GetFullPath(path);
            MessageBox.Show("Çıktı text dosyası " + fullPath + " konumuna kaydedilmiştir.");
        }
        public double mileCevir(double deger)
        {
            double mil;

            mil = deger * Form1.gemiler.ElementAt(0).hiz / Form1.xx.trackBar1.Value;


            return mil;
        }

        public double saateCevir(double deger)
        {
            double saat;
            saat = deger / Form1.gemiler.ElementAt(0).yedek_hiz*60;
            return saat;
        }
        public void RaporSave(string filename, List<Rota> rotalar)
        {
            string path = @"" + filename;
            string veriler = "";
            StreamWriter sW = new StreamWriter(path);
            veriler = "Kaçışa Kadar Süre\tKaçış Açısı\tKaçış Süresi\tRotaya Dönüş Açısı\tRotaya Dönüş Sür.\tOrj Rotaya Dönüş Açısı\t"+
                "Kaçış mesafesi (mil)";
            sW.WriteLine(veriler);
            veriler = "";
            for (int i = 0; i < rotalar.Count; i++)
            {
                //veriler += gemiler.ElementAt(i).emniyet_alani + ";" + gemiler.ElementAt(i).hiz + ";" + gemiler.ElementAt(i).rota + ";" + gemiler.ElementAt(i).merkez.X + ";" + gemiler.ElementAt(i).merkez.Y;
                //sW.WriteLine(veriler);
                for(int j = 0; j < 3; j++)
                {
                    
                    veriler += saateCevir(mileCevir(rotalar.ElementAt(i).t[j])).ToString("0.##") + "\t" 
                        + rotalar.ElementAt(i).co[j].ToString("0.##") + "\t";
                }
                veriler += mileCevir(rotalar.ElementAt(i).fitness).ToString("0.##");
                sW.WriteLine(veriler);
                veriler = "";
            }

            sW.Close();
            string fullPath = Path.GetFullPath(path);
            MessageBox.Show("Çıktı text dosyası " + fullPath + " konumuna kaydedilmiştir.");
        }
        public float[,] Load(string filename)
        {
            string whole_file = System.IO.File.ReadAllText(filename);
            whole_file = whole_file.Replace('\n', '\r');
            string[] lines = whole_file.Split(new char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);

            num_rows = lines.Length;
            num_cols = lines[0].Split(';').Length;

            string[,] values = new string[num_rows, num_cols];
            for (int r = 0; r < num_rows; r++)
            {
                string[] line_r = lines[r].Split(';');
                for (int c = 0; c < num_cols; c++)
                {
                    values[r, c] = line_r[c];
                }
            }
            datasetGoster(values);
            return toFloat(values, num_rows, num_cols);
        }

        public void datasetGoster(string[,] _values)
        {
            for (int i = 0; i < num_rows; i++)
            {
                for (int j = 0; j < num_cols; j++)
                {
                    Console.Write(_values[i, j] + ";");
                }
                Console.WriteLine("");
            }
        }
        private float[,] toFloat(string[,] _dataset, int _num_rows, int _num_cols)
        {
            float[,] floatSet = new float[_num_rows, _num_cols];
            for (int i = 0; i < num_rows; i++)
            {
                for (int j = 0; j < num_cols; j++)
                {
                    floatSet[i, j] = float.Parse(_dataset[i, j]);
                }
            }
            return floatSet;
        }
    }
}
