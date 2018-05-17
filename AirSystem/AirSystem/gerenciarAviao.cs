using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirSystem
{
    public partial class gerenciarAviao : Form
    {
        public gerenciarAviao()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            novoFabricante nf = new novoFabricante();
            nf.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            novoModelo nm = new novoModelo();
            nm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            gerenciarAeronave gerenciarAero = new gerenciarAeronave(); 
            gerenciarAero.Show(); 
        }
    }
}
