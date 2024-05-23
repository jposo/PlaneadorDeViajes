using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class Inicio : Form
    {
        // diccionario con k:ciudad, v:tupla de datos
        Dictionary<string, (uint X, uint Y)> ciudades = new Dictionary<string, (uint X, uint Y)>();
        // diccionario con k:tupla ciudad inicio y fin, v: tupla datos
        public Inicio()
        {
            InitializeComponent();
        }

        private void ciudadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CiudadForm cf = new CiudadForm();
            cf.ShowDialog();
        }

        private void rutaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RutaForm rf = new RutaForm();
            rf.ShowDialog();
        }

        private void busquedaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BusqForm bq = new BusqForm();
            bq.ShowDialog();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void informacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InfoForm ff = new InfoForm();
            ff.ShowDialog();
        }
    }
}
