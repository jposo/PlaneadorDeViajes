using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProyectoFinal
{
    public partial class CiudadForm : Form
    {
        public CiudadForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            var coordenadas = e.Location;
            coordx.Value = coordenadas.X;
            coordy.Value = coordenadas.Y;
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            try
            {
                var nom = nombre.Text;
                var cx = coordx.Value;
                var cy = coordy.Value;

                using (StreamWriter sw = File.AppendText("ciudades.txt"))
                {
                    sw.WriteLine("{0}\t{1}\t{2}\n", nom, cx, cy);
                }

                MessageBox.Show("Efectivamente dada de alta la ciudad");
                pictureBox1.Invalidate();
            } catch
            {
                MessageBox.Show("Hubo un error");
            }
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
            foreach(var datos in rutas)
            {
                (int cx, int cy) coorinicio;
                ciudades.TryGetValue(datos.Value.inicio, out coorinicio);
                (int cx, int cy) coordestino;
                ciudades.TryGetValue(datos.Value.destino, out coordestino);
                //Console.WriteLine("{0}\t{1}, {2}\t{3}, {4}", datos.Key, coorinicio.cx, coorinicio.cy, coordestino.cx, coordestino.cy);

                e.Graphics.DrawLine(Pens.Green, new Point(coorinicio.cx, coorinicio.cy), new Point(coordestino.cx, coordestino.cy));
            }
        }
    }
}