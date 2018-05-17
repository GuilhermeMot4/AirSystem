using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace AirSystem
{
    public partial class UCAviazinho : UserControl
    {

        String nome;
        Bitmap foto;
        Color cor;
        Byte[] som_array;


        public UCAviazinho()
        {
            InitializeComponent();
        }
        public void Construtor(String nome, Bitmap foto, Byte[] som) {
            this.nome = nome;
            label1.Text = nome;
            this.foto = foto;       
            cor = Label.DefaultForeColor;
            som_array = som;
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Image = foto;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (MemoryStream ms = new MemoryStream(som_array)) {
                SoundPlayer sp = new SoundPlayer(ms);
                sp.Play();
            }
        }
    }
}
