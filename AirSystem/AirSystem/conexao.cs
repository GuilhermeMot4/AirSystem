using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirSystem
{
    class conexao
    {
        public static SqlConnection conectar()
        {
            string stringConexao = "Data Source=localhost;Initial Catalog=model;Integrated Security=True";
            SqlConnection conn = new SqlConnection(stringConexao);
            conn.Open();
            return conn;
        }
    }
}
