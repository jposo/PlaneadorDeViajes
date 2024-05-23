using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace ProyectoFinal
{
    public partial class RutaForm : Form
    {
        public RutaForm()
        {
            InitializeComponent();
        }
        // agregar los ciudades a todos los combobox
        private void RutaForm_Load(object sender, EventArgs e)
        {
            var ciudades = Datos.Ciudades();

            foreach (var datos in ciudades)
            {
                comboBox1.Items.Add(datos.Key);
                comboBox2.Items.Add(datos.Key);
            }
        }
        
        private void altaRuta_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text;
            string inicio = comboBox1.SelectedItem.ToString();
            string destino = comboBox2.SelectedItem.ToString();

            if (inicio == destino)
            {
                MessageBox.Show("Destino y ruta deben de ser diferentes");
                return;
            }

            decimal distancia = numericUpDown1.Value;
            decimal tAR = numericUpDown2.Value;
            decimal cAR = numericUpDown3.Value;
            decimal tTP = numericUpDown4.Value;
            decimal cTP = numericUpDown5.Value;

            using (StreamWriter sw = File.AppendText("rutas.txt"))
            {
                sw.WriteLine("\n{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}", 
                    nombre, inicio, destino, distancia, tAR, cAR, tTP, cTP);
            }
            MessageBox.Show("Efectivamente dada de alta la ruta");
        }
    }
}
