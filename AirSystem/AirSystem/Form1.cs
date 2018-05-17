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
    public partial class LoginAirsystem : Form
    {
        string usuario;
        string senha;
        string tipoUser;

        public LoginAirsystem()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //novoUsuario nu = new novoUsuario();
            //nu.Show();
                usuario = textBox1.Text;
                senha = textBox2.Text;

            if ((!String.IsNullOrEmpty(usuario)) || (!String.IsNullOrEmpty(senha)))
            {


                SqlCommand cmd = new SqlCommand("SELECT usuario,senha FROM Usuarios WHERE usuario='" + usuario + "'and senha='" + senha + "'", conexao.conectar());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    SqlCommand command = new SqlCommand("SELECT * FROM Usuarios WHERE usuario='" + usuario + "';", conexao.conectar());
                    conexao.conectar();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            tipoUser = reader.GetString(9);

                        }
                    }

                    if (tipoUser == "Adm")
                    {
                        this.Hide();
                        principalAdm pAdm = new principalAdm();
                        pAdm.Closed += (s, args) => this.Close();
                        pAdm.Show();
                    }
                    else
                    {
                        this.Hide();
                        principalUsuario pUs = new principalUsuario();
                        pUs.Closed += (s, args) => this.Close();
                        pUs.Show();
                    }
                }
                else {
                    MessageBox.Show("Usuário e/ou senha incorretos!");
                }
            }
            else {
                MessageBox.Show("Preencha os campos corretamente!");
            }       
            }

        private void button3_Click(object sender, EventArgs e)
        {
            novoUsuario novoUser = new novoUsuario();
            novoUser.Show();
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
