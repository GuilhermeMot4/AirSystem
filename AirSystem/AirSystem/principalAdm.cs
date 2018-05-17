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
    public partial class principalAdm : Form
    {
        public principalAdm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listarUsuarios listarUsers = new listarUsuarios();
            listarUsers.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            gerenciarAviao ga = new gerenciarAviao();
            ga.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            gerenciarCompanhia gc = new gerenciarCompanhia();
            gc.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listarAvioes la = new listarAvioes();
            la.Show();
        }
    }
}
