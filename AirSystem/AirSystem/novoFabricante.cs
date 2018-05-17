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
    public partial class novoFabricante : Form
    {
        string fabricante;
        string sede;
        string pais;
        string telefone;
        string email;

        public novoFabricante()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fabricante = textBox1.Text;
            sede = textBox2.Text;
            pais = textBox3.Text;
            telefone = maskedTextBox1.Text;
            email = textBox5.Text;

            if (!(String.IsNullOrEmpty(fabricante)) || !(String.IsNullOrEmpty(sede)) || !(String.IsNullOrEmpty(pais))
                || !(String.IsNullOrEmpty(telefone)) || !(String.IsNullOrEmpty(email)))
            {

                string stringConexao = "INSERT INTO Fabricante VALUES(@fab,@sede,@pais,@telefone,@email)";

                SqlCommand comando = new SqlCommand(stringConexao, conexao.conectar());

                comando.Parameters.AddWithValue("@fab", fabricante);
                comando.Parameters.AddWithValue("@sede", sede);
                comando.Parameters.AddWithValue("@pais", pais);
                comando.Parameters.AddWithValue("@telefone", telefone);
                comando.Parameters.AddWithValue("@email", email);

                comando.ExecuteNonQuery();

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
            this.Hide();
            ga.Show();
        }
    }
}
