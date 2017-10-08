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
    public partial class VeriOnaylamaDialogBox : Form
    {
        public VeriOnaylamaDialogBox()
        {
         
            InitializeComponent();
            pictureBox1.Show();
        }

        private void VeriOnaylamaDialogBox_Load(object sender, EventArgs e)
        {
            this.Show();
            pictureBox1.Refresh();
            System.Threading.Thread.Sleep(1000);
            this.Dispose();
        }
    }
}
