using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ProyectoFinal
{
    public partial class BusqForm : Form
    {
        public BusqForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush Brush = new SolidBrush(Color.Red);

            // Crea la posición y tamaño del círculo
            int diam = 10;

            var ciudades = Datos.Ciudades();
            // pintar puntos de ciudades
            foreach (var datos in ciudades)
            {
                int xp = datos.Value.cx - (diam / 2);
                int yp = datos.Value.cy - (diam / 2);

                // Dibuja un círculo
                e.Graphics.FillEllipse(Brush, xp, yp, diam, diam);
                // Coloca el nombre de la ciudad
                e.Graphics.DrawString(datos.Key,
                                   new Font("Arial", 10),
                                   Brushes.Black,
                                   new Point(datos.Value.cx + diam, datos.Value.cy));
            }
            var rutas = Datos.Rutas();
            // pintar lineas de rutas
            foreach (var datos in rutas)
            {
                (int cx, int cy) coorinicio;
                ciudades.TryGetValue(datos.Value.inicio, out coorinicio);
                (int cx, int cy) coordestino;
                ciudades.TryGetValue(datos.Value.destino, out coordestino);
                // Console.WriteLine("{0}\t{1}, {2}\t{3}, {4}", datos.Key, coorinicio.cx, coorinicio.cy, coordestino.cx, coordestino.cy);

                e.Graphics.DrawLine(Pens.Green, new Point(coorinicio.cx, coorinicio.cy), new Point(coordestino.cx, coordestino.cy));
            }
        }

        private void BusqForm_Load(object sender, EventArgs e)
        {
            var ciudades = Datos.Ciudades();

            foreach (var datos in ciudades)
            {
                comboBox1.Items.Add(datos.Key);
                comboBox2.Items.Add(datos.Key);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string transporte = radioButton1.Checked ? "ar" : "tp";
            string criterio = "dist";
            if (!radioButton3.Checked)
                if (!radioButton4.Checked)
                    criterio = "costo";
                else
                    criterio = "tiempo";
            int[,] matriz = Datos.GenerarMatriz(transporte, criterio);
            for (int i = 0; i < matriz.Length; i++)
            {
                for (int j = 0; j < matriz.Length; j++)
                {
                    Console.Write("{0} ", matriz[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
