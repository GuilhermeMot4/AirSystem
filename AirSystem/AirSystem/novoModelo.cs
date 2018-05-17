using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirSystem
{
    public partial class novoModelo : Form
    {
        string modelo;
        string qtdAssento;
        string origem;
        string fabricacao;
        string turbinas;
        public novoModelo()
        {
            InitializeComponent();
        }

        private void novoModelo_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add(1);
            comboBox1.Items.Add(2);
            comboBox1.Items.Add(3);
            comboBox1.Items.Add(4);

            comboBox2.Items.Add("Brasil");
            comboBox2.Items.Add("Alemanha");
            comboBox2.Items.Add("Estados Unidos");
            comboBox2.Items.Add("Reino Unido");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            modelo = textBox6.Text;
            qtdAssento = textBox3.Text;
            origem = comboBox2.SelectedItem.ToString();
            fabricacao = maskedTextBox1.Text;
            turbinas = comboBox1.SelectedItem.ToString();

            if (!(String.IsNullOrEmpty(modelo)) || !(String.IsNullOrEmpty(qtdAssento))
                || !(String.IsNullOrEmpty(fabricacao)))
            {

                string query = "INSERT INTO Modelo VALUES(@mod,@qtd,@origem,@fabricacao,@turbinas)";

                SqlCommand cmd = new SqlCommand(query, conexao.conectar());

                cmd.Parameters.AddWithValue("@mod", modelo);
                cmd.Parameters.AddWithValue("@qtd", qtdAssento);
                cmd.Parameters.AddWithValue("@origem", origem);
                cmd.Parameters.AddWithValue("@fabricacao", fabricacao);
                cmd.Parameters.AddWithValue("@turbinas", turbinas);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Cadastrado com Sucesso!");
                this.Hide();
            }
            else {
                MessageBox.Show("Preencha os campos corretamente!");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            gerenciarAviao ga = new gerenciarAviao();
            ga.Show();
        }
    }
}
