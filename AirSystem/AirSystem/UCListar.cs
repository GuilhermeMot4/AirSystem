using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace AirSystem
{
    public partial class UCListar : UserControl
    {
        string id;
        public UCListar(int id)
        {
            this.id = id.ToString();
            InitializeComponent();
            GerarCards();
        }

        private void GerarCards()
        {// SELECT a.foto,m.modelo FROM modelosSet m, aeronaveSet a WHERE a.idModelo = m.Id
            flowLayoutPanel1.Controls.Clear();
            SqlCommand command = new SqlCommand("SELECT a.foto,m.modelo,a.som FROM Modelo m, Aeronave a WHERE a.id_modelo = m.id_modelo AND a.id_modelo = '"+id+"'", conexao.conectar());
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
    }
}
