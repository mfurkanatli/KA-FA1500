using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class Rota
    {
        public double[] t = new double[3];
        public double[] co = new double[3];
        public double fitness;

        public Rota()
        {

        }

        public Rota(double[] _t, double[] _co,double _f)
        {

            for (int i = 0; i < _t.Length; i++)
                t[i] = _t[i];
            for (int i = 0; i < _co.Length; i++)
                co[i] = _co[i];
            fitness = _f;
            
        }
        public void fitnessHesapla()
        {
            fitness = t[1] + t[2];
        }

        public Rota Klonla()
        {
            return (new Rota(t,co,fitness));
        }
    }
}
