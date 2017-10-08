using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class LoadingForm : Form
    {
        static ProgressBar pb;
        static LoadingForm lf;
        static Label lb;
        public LoadingForm(int gelen)
        {
            lf = this;
            lb = label1;
            InitializeComponent();
            
            pb = progressBar1;
            pb.Maximum = gelen;
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {
            lb = label1;
        }

        static public void progressUpdate(int gelen)
        {
            Console.WriteLine(((gelen * 100) / pb.Maximum));
            pb.Value = gelen+1;
            lb.Text = "Hesaplanıyor.. Lütfen Bekleyiniz. % " + (((gelen+1) * 100) / pb.Maximum);
            if (pb.Value ==(pb.Maximum))
            { 
                lf.Dispose();
            }
        }
    }
}
