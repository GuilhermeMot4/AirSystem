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
    public partial class gerenciarCompanhia : Form
    {
        string idCompanhia;
        string companhia;
        string cnpj;
        string cidade;
        string estado;
        string telefone;
        string email;
        byte[] image_byte = null;
        string imagem;

        public gerenciarCompanhia()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            companhia = textBox1.Text;
            cnpj = textBox3.Text;
            cidade = textBox12.Text;
            estado = textBox4.Text;
            telefone = textBox5.Text;
            email = textBox6.Text;

            FileStream fstream = new FileStream(this.textBox7.Text, FileMode.Open, FileAccess.Read);

            BinaryReader br = new BinaryReader(fstream);

            image_byte = br.ReadBytes((int)fstream.Length);

            string query = "INSERT INTO Companhia VALUES(@comp,@cnpj,@cidade,@estado,@telefone,@email,@imagem)";

            SqlCommand cmd = new SqlCommand(query, conexao.conectar());

            cmd.Parameters.AddWithValue("@comp", companhia);
            cmd.Parameters.AddWithValue("@cnpj", cnpj);
            cmd.Parameters.AddWithValue("@cidade", cidade);
            cmd.Parameters.AddWithValue("@estado", estado);
            cmd.Parameters.AddWithValue("@telefone", telefone);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@imagem", image_byte);

            cmd.ExecuteNonQuery();
 
            MessageBox.Show("Cadastrado com sucesso!");
            this.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|AllFiles(*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imagem = dialog.FileName.ToString();
                textBox7.Text = imagem;
                pictureBox1.ImageLocation = imagem;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            image_byte = null;
            textBox7 = null;
            pictureBox1.ImageLocation = null;
        }

        private void gerenciarCompanhia_Load(object sender, EventArgs e)
        {
            PuxarDadosGrid();
        }

        private void PuxarDadosGrid()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Companhia", conexao.conectar());

            SqlDataAdapter sqlAdpter = new SqlDataAdapter();
            sqlAdpter.SelectCommand = cmd;
            DataTable dt = new DataTable();
            sqlAdpter.Fill(dt);
            BindingSource bs = new BindingSource();
            bs.DataSource = dt;
            dataGridView1.DataSource = bs;
            sqlAdpter.Update(dt);
        }

        private void puxarDados(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT id_companhia,foto FROM Companhia WHERE id_companhia = '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", conexao.conectar());

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    idCompanhia = reader.GetInt32(0).ToString();
                    byte[] picData = reader["foto"] as byte[] ?? null;
                    image_byte = picData;
                    if (picData != null)
                    {
                        using (MemoryStream ms = new MemoryStream(picData))
                        {
                            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ms);
                            pictureBox2.Image = bmp;

                        }
                    }

                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Companhia WHERE id_companhia = '" + idCompanhia + "'", conexao.conectar());
            cmd.ExecuteNonQuery();

            MessageBox.Show("Excluído com sucesso!");
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string busca = textBox2.Text;

            SqlCommand cmd = new SqlCommand("select * from Companhia c where c.companhia = '" + busca + "'", conexao.conectar());

            SqlDataAdapter sqlAdpter = new SqlDataAdapter();
            sqlAdpter.SelectCommand = cmd;
            DataTable dt = new DataTable();
            sqlAdpter.Fill(dt);
            BindingSource bs = new BindingSource();
            bs.DataSource = dt;
            dataGridView1.DataSource = bs;
            sqlAdpter.Update(dt);
        }
    }
}
    
