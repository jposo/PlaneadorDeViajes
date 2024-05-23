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

namespace ProyectoFinal
{
    public partial class RutaForm : Form
    {
        public RutaForm()
        {
            InitializeComponent();
        }

        private void RutaForm_Load(object sender, EventArgs e)
        {
            ComboBox cb1 = comboBox1;
            string con = Properties.Settings.Default.MapaConnectionString;
            SqlConnection cnn = new SqlConnection(con);
            cnn.Open();
            string query = "SELECT Nombre FROM Ciudad";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                cb1.Items.Add(rdr[0].ToString());
            }
            rdr.Close();
        }
    }
}
