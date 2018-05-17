using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirSystem
{
    public partial class novoUsuario : Form
    {
        string foto = "";
        bool verificar = true;
        byte[] image_byte = null;
        public novoUsuario()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(maskedTextBox1.Text) || String.IsNullOrEmpty(textBox3.Text) || String.IsNullOrEmpty(textBox5.Text) || String.IsNullOrEmpty(textBox7.Text) || String.IsNullOrEmpty(textBox9.Text))
            {
                verificar = false;
                MessageBox.Show("Preencha todos os campos!");
            }
            else {
                if (textBox8.Text.Equals(textBox7.Text))
                {
                    verificar = true;
                    Cadastro();                 
                }
                else {
                    verificar = false;
                    MessageBox.Show("Senhas diferentes!");
                }
                verificar = false;
            }
        }
        public void Cadastro() {

            FileStream fstream = new FileStream(this.textBox9.Text, FileMode.Open, FileAccess.Read);

            BinaryReader br = new BinaryReader(fstream);

            image_byte = br.ReadBytes((int)fstream.Length);

            string query = "INSERT INTO Usuarios(nome,sobrenome,endereco,nascimento,numero,usuario,senha,foto,tipo_usuario) VALUES (@nome,@sobrenome,@end,@nasc,@numero,@usuario,@senha,@foto,@tipo)";

            using (SqlCommand command = new SqlCommand(query, conexao.conectar()))
            {
            
                command.Parameters.AddWithValue("@nome", textBox1.Text);
                command.Parameters.AddWithValue("@sobrenome", textBox2.Text);
                command.Parameters.AddWithValue("@end", textBox4.Text);
                command.Parameters.AddWithValue("@nasc", maskedTextBox1.Text);
                command.Parameters.AddWithValue("@numero", textBox3.Text);
                command.Parameters.AddWithValue("@usuario", textBox5.Text);
                command.Parameters.AddWithValue("@senha", textBox7.Text);
                command.Parameters.AddWithValue("@foto", image_byte);
                command.Parameters.AddWithValue("@tipo", "User");

                command.ExecuteNonQuery();            
                MessageBox.Show("Cadastrado com sucesso!");
                this.Close();
            }

        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|AllFiles(*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                foto = dialog.FileName.ToString();
                textBox9.Text = foto;
                pictureBox1.ImageLocation = foto;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            image_byte = null;
            textBox9 = null;
            pictureBox1.ImageLocation = null;   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor = Color.Green;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Transparent;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button2.BackColor = Color.Red;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.Transparent;
        }
    }
               
}

