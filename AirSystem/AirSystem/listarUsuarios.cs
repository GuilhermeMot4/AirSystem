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
    public partial class listarUsuarios : Form

    {
        byte[] image_byte = null;
        string senha;
        string csenha;
        string imagem;

        public listarUsuarios()
        {
            InitializeComponent();
        }

        private void listarUsuarios_Load(object sender, EventArgs e)
        {

            SqlCommand cmd = new SqlCommand("SELECT * FROM Usuarios WHERE tipo_usuario = 'User'", conexao.conectar());
            conexao.conectar();

            try
            {
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                dataGridView1.DataSource = bs;
                sda.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void puxarDados(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Usuarios WHERE id_usuario = '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", conexao.conectar());
            conexao.conectar();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    textBox1.Text = reader.GetString(1);
                    textBox2.Text = reader.GetString(2);
                    textBox3.Text = reader.GetString(3);
                    maskedTextBox1.Text = reader.GetString(4);
                    textBox5.Text = reader.GetString(5);
                    textBox6.Text = reader.GetString(6);
                    textBox7.Text = reader.GetString(7);
                    byte[] picData = reader["foto"] as byte[] ?? null;
                    image_byte = picData;
                    if (picData != null)
                    {
                        using (MemoryStream ms = new MemoryStream(picData))
                        {
                            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ms);
                            pictureBox1.Image = bmp;

                        }
                    }

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            novoUsuario nuser = new novoUsuario();
            nuser.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {


            senha = textBox7.Text;
            csenha = textBox8.Text;

            if (senha.Equals(csenha))
            {
                string query = "UPDATE Usuarios SET nome = @nome, sobrenome = @sobrenome,endereco = @end, nascimento = @nasc, numero = @numero, usuario = @usuario, senha= @senha, foto = @foto WHERE id_usuario = '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";

                using (SqlCommand command = new SqlCommand(query, conexao.conectar()))
                {

                    command.Parameters.AddWithValue("@nome", textBox1.Text);
                    command.Parameters.AddWithValue("@sobrenome", textBox2.Text);
                    command.Parameters.AddWithValue("@end", textBox3.Text);
                    command.Parameters.AddWithValue("@nasc", maskedTextBox1.Text);
                    command.Parameters.AddWithValue("@numero", textBox5.Text);
                    command.Parameters.AddWithValue("@usuario", textBox6.Text);
                    command.Parameters.AddWithValue("@senha", textBox7.Text);
                    command.Parameters.AddWithValue("@foto", image_byte);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Editado com sucesso!");
                    this.Close();
                }
            }
            else {
                MessageBox.Show("Senhas não conferem!");
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|AllFiles(*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foto = dialog.FileName.ToString();
                imagem = dialog.FileName.ToString();
                pictureBox1.ImageLocation = foto;

                FileStream fstream = new FileStream(imagem, FileMode.Open, FileAccess.Read);

                BinaryReader br = new BinaryReader(fstream);

                image_byte = br.ReadBytes((int)fstream.Length);

            }
      

        }

        private void button2_Click(object sender, EventArgs e)
        {
            image_byte = null;
            pictureBox1.ImageLocation = null;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM Usuarios WHERE id_usuario = '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";

            using (SqlCommand command = new SqlCommand(query, conexao.conectar())) {
                command.ExecuteNonQuery();
                MessageBox.Show("Excluído com sucesso!");
            }
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            button4.BackColor = Color.Green;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.BackColor = Color.Transparent;
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            button3.BackColor = Color.Green;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.Transparent;
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            button6.BackColor = Color.Red;
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            button6.BackColor = Color.Transparent;
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            button5.BackColor = Color.Green;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.BackColor = Color.Transparent;
        }
    }
}
   
