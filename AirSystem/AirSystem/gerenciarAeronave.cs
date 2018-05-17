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
    public partial class gerenciarAeronave : Form
    {
        byte[] image_byte = null;
        byte[] som_byte = null;
        string idAeronave;
        string imagem;
        string busca;
        public gerenciarAeronave()
        {
            InitializeComponent();
        }

        public class modelos
        {
            public string Nome { get; set; }
            public int Id { get; set; }
        }

        public class companinha
        {
            public string Nome { get; set; }
            public int Id { get; set; }
        }

        public class fabricante
        {
            public string Nome { get; set; }
            public int Id { get; set; }
        }


        private List<modelos> RetornaListaModelos()
        {
            List<modelos> lista = new List<modelos>();
            string query = "SELECT modelo,id_modelo FROM Modelo";
            SqlCommand cmd = new SqlCommand(query, conexao.conectar());
            conexao.conectar();
            SqlDataReader leitor = cmd.ExecuteReader();
            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    modelos c = new modelos();
                    c.Nome = leitor["modelo"].ToString();
                    c.Id = int.Parse(leitor["id_modelo"].ToString());
                    lista.Add(c);
                }
            }
            return lista;
        }

        private List<companinha> RetornaListaCompaninha()
        {
            List<companinha> lista = new List<companinha>();
            string query = "SELECT companhia,id_companhia FROM Companhia";
            SqlCommand cmd = new SqlCommand(query, conexao.conectar());
            conexao.conectar();
            SqlDataReader leitor = cmd.ExecuteReader();
            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    companinha c = new companinha();
                    c.Nome = leitor["companhia"].ToString();
                    c.Id = int.Parse(leitor["id_companhia"].ToString());
                    lista.Add(c);
                }
            }
            return lista;
        }
        private List<fabricante> RetornaListaFabricante()
        {
            List<fabricante> lista = new List<fabricante>();
            string query = "SELECT nome,id_fabricante FROM Fabricante";
            SqlCommand cmd = new SqlCommand(query, conexao.conectar());
            conexao.conectar();
            SqlDataReader leitor = cmd.ExecuteReader();
            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    fabricante c = new fabricante();
                    c.Nome = leitor["nome"].ToString();
                    c.Id = int.Parse(leitor["id_fabricante"].ToString());
                    lista.Add(c);
                }
            }
            return lista;
        }

        private void CarregaComboBox()
        {
            comboBox1.DisplayMember = "Nome";
            comboBox1.ValueMember = "Id";
            comboBox1.DataSource = RetornaListaModelos();

            comboBox2.DisplayMember = "Nome";
            comboBox2.ValueMember = "Id";
            comboBox2.DataSource = RetornaListaFabricante();

            comboBox3.DisplayMember = "Nome";
            comboBox3.ValueMember = "Id";
            comboBox3.DataSource = RetornaListaCompaninha();
        }

        private bool ValidarCampos()
        {
            if (String.IsNullOrEmpty(textBox4.Text) || String.IsNullOrEmpty(textBox5.Text) || som_byte == null || image_byte == null)
            {
                MessageBox.Show("Preencha todos os campos");
                return false; 
            }
            else
            {
                return true;
            }
        }
   
        private void PuxarDadosGrid()
        {
            SqlCommand cmd = new SqlCommand("select a.id_aeronave,m.modelo,c.companhia,f.nome,m.qtd_assento,m.fabricacao,a.cor,a.descricao from Aeronave a,Modelo m,Fabricante f,Companhia c where a.id_modelo = m.id_modelo AND a.id_fabricante = f.id_fabricante AND a.id_companhia = c.id_companhia", conexao.conectar());

            SqlDataAdapter sqlAdpter = new SqlDataAdapter();
            sqlAdpter.SelectCommand = cmd;
            DataTable dt = new DataTable();
            sqlAdpter.Fill(dt);
            BindingSource bs = new BindingSource();
            bs.DataSource = dt;
            dataGridView1.DataSource = bs;
            sqlAdpter.Update(dt);
        }
      
        private void button10_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FileStream fstream = new FileStream(dialog.FileName.ToString(), FileMode.Open, FileAccess.Read);
                string som = dialog.FileName.ToString();
                BinaryReader br = new BinaryReader(fstream);
                som_byte = br.ReadBytes((int)fstream.Length);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(dialog.FileName);
                string imagem = dialog.FileName.ToString();
                FileStream fstream = new FileStream(dialog.FileName.ToString(), FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fstream);
                image_byte = br.ReadBytes((int)fstream.Length);
            }
        }

        private void gerenciarAeronave_Load_1(object sender, EventArgs e)
        {
            CarregaComboBox();
            PuxarDadosGrid();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            if (ValidarCampos())
            {

                SqlCommand command = new SqlCommand("INSERT INTO Aeronave VALUES(@modelo,@fabric,@comp,@cor,@desc,@som,@img)", conexao.conectar());
                command.Parameters.AddWithValue("@modelo", comboBox1.SelectedValue.ToString());
                command.Parameters.AddWithValue("@fabric", comboBox2.SelectedValue.ToString());
                command.Parameters.AddWithValue("@comp", comboBox3.SelectedValue.ToString());
                command.Parameters.AddWithValue("@cor", textBox4.Text.ToString());
                command.Parameters.AddWithValue("@desc", textBox5.Text.ToString());
                command.Parameters.AddWithValue("@som", som_byte);
                command.Parameters.AddWithValue("@img", image_byte);
                command.ExecuteNonQuery();

                MessageBox.Show("Cadastrado com sucesso!");
                this.Close();
            }
        }

        private void puxarDados(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT id_aeronave,foto FROM Aeronave WHERE id_aeronave = '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'", conexao.conectar());

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    idAeronave = reader.GetInt32(0).ToString();
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
            SqlCommand cmd = new SqlCommand("DELETE FROM Aeronave WHERE id_aeronave = '" + idAeronave + "'", conexao.conectar());
            cmd.ExecuteNonQuery();

            MessageBox.Show("Excluído com sucesso!");
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string busca = textBox2.Text;

            SqlCommand cmd = new SqlCommand("select a.id_aeronave,m.modelo,c.companhia,f.nome,m.qtd_assento,m.fabricacao,a.cor,a.descricao from Aeronave a,Modelo m,Fabricante f,Companhia c where a.id_modelo = m.id_modelo AND a.id_fabricante = f.id_fabricante AND a.id_companhia = c.id_companhia AND m.modelo = '" + busca+"'", conexao.conectar());

            SqlDataAdapter sqlAdpter = new SqlDataAdapter();
            sqlAdpter.SelectCommand = cmd;
            DataTable dt = new DataTable();
            sqlAdpter.Fill(dt);
            BindingSource bs = new BindingSource();
            bs.DataSource = dt;
            dataGridView1.DataSource = bs;
            sqlAdpter.Update(dt);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            gerenciarAviao gaviao = new gerenciarAviao();
            gaviao.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

    }
}
