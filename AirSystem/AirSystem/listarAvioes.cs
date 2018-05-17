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
    public partial class listarAvioes : Form
    {
        public listarAvioes()
        {
            InitializeComponent();
        }

        private void GerarCards()
        {// SELECT a.foto,m.modelo FROM modelosSet m, aeronaveSet a WHERE a.idModelo = m.Id
            flowLayoutPanel1.Controls.Clear();
            SqlCommand command = new SqlCommand("SELECT a.foto,m.modelo,a.som FROM Modelo m, Aeronave a WHERE a.id_modelo = m.id_modelo", conexao.conectar());
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    byte[] picData = reader["foto"] as byte[] ?? null;
                    byte[] somData = reader["som"] as byte[] ?? null;


                    if (picData != null)
                    {
                        using (MemoryStream ms = new MemoryStream(picData))
                        {
                            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ms);
                            UCAviazinho card = new UCAviazinho();
                            card.Construtor(reader.GetString(1), bmp, somData);
                            flowLayoutPanel1.Controls.Add(card);
                        }
                    }
                }




            }
        }

        private void listarAvioes_Load(object sender, EventArgs e)
        {
            GerarCards();

            SqlCommand command = new SqlCommand("SELECT * FROM Companhia", conexao.conectar());
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string titulo = reader.GetString(1);

                    string title = titulo.ToString();
                    TabPage myTabPage = new TabPage(title);
                    myTabPage.Controls.Add(new UCListar(reader.GetInt32(0)));
                    tabControl1.TabPages.Add(myTabPage);

                }



            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            tabControl1.Alignment = TabAlignment.Bottom;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            tabControl1.Alignment = TabAlignment.Left;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            tabControl1.Alignment = TabAlignment.Right;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            tabControl1.Alignment = TabAlignment.Top;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string busca = textBox1.Text;

            flowLayoutPanel2.Controls.Clear();
            SqlCommand command = new SqlCommand("SELECT a.foto,m.modelo,a.som FROM Modelo m, Aeronave a WHERE a.id_modelo = m.id_modelo AND m.modelo = '" + busca + "'", conexao.conectar());
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    byte[] picData = reader["foto"] as byte[] ?? null;
                    byte[] somData = reader["som"] as byte[] ?? null;


                    if (picData != null)
                    {
                        using (MemoryStream ms = new MemoryStream(picData))
                        {
                            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ms);
                            UCAviazinho card = new UCAviazinho();
                            card.Construtor(reader.GetString(1), bmp, somData);
                            flowLayoutPanel2.Controls.Add(card);
                        }
                    }
                }
            }

        }
    }
}
            
   
    
