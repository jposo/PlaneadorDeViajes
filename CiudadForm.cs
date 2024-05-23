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
            var nom = nombre.Text;
            var cx = coordx.Value;
            var cy = coordy.Value;

            XDocument ciudades = XDocument.Load("ciudades.xml");
            XElement ciudad = new XElement("ciudad");
            ciudad.Add(new XElement("nombre", nom));
            ciudad.Add(new XElement("coordX", cx));
            ciudad.Add(new XElement("coordY", cy));
            ciudades.Element("root").Add(ciudad);
            ciudades.Save("ciudades.xml");


            string con = Properties.Settings.Default.MapaConnectionString;
            SqlConnection cnn = new SqlConnection(con);

            cnn.Open();
            SqlTransaction sqlTransaction = cnn.BeginTransaction();

            string query = "INSERT INTO Ciudad (Nombre, CoordX, CoordY) VALUES (@Nombre, @CoordX, @CoordY)";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.Transaction = sqlTransaction;
            cmd.Parameters.AddWithValue("@Nombre", nom);
            cmd.Parameters.AddWithValue("@CoordX", cx);
            cmd.Parameters.AddWithValue("@CoordY", cy);

            cmd.ExecuteNonQuery();
            sqlTransaction.Commit();
            MessageBox.Show("Efectivamente dada de alta la ciudad");
        }

        public void PintarCiudades()
        {
            Graphics grafica = this.pictureBox1.CreateGraphics();
            SolidBrush Brush = new SolidBrush(Color.Black);
            
            // Crea la posición y tamaño del círculo
            int ancho = 10;
            int alto = 10;

            string con = Properties.Settings.Default.MapaConnectionString;
            SqlConnection cnn = new SqlConnection(con);

            cnn.Open();
            string query = "SELECT Nombre, CoordX, CoordY FROM Ciudad";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataReader rdr = cmd.ExecuteReader();
            // pintar puntos
            while (rdr.Read())
            {
                string nom = rdr[0].ToString();
                int cx = (int)rdr[1];
                int cy = (int)rdr[2];
                // Dibuja un círculo
                grafica.FillEllipse(Brush, cx, cy, ancho, alto);
                // Coloca el nombre de la ciudad
                grafica.DrawString(nom,
                                   new Font("Arial", 10),
                                   Brushes.Gray,
                                   new Point(cx + 10, cy + 10));
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush Brush = new SolidBrush(Color.Black);

            // Crea la posición y tamaño del círculo
            int diam = 10;

            string con = Properties.Settings.Default.MapaConnectionString;
            SqlConnection cnn = new SqlConnection(con);

            cnn.Open();
            string query = "SELECT Nombre, CoordX, CoordY FROM Ciudad";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataReader rdr = cmd.ExecuteReader();
            // pintar puntos
            while (rdr.Read())
            {
                string nom = rdr[0].ToString();
                int cx = (int)rdr[1];
                int cy = (int)rdr[2];

                int xp = cx - (diam / 2);
                int yp = cy - (diam / 2);

                Console.WriteLine("{0}, {1}", xp, yp);

                // Dibuja un círculo
                e.Graphics.FillEllipse(Brush, xp, yp, diam, diam);
                // Coloca el nombre de la ciudad
                e.Graphics.DrawString(nom,
                                   new Font("Arial", 10),
                                   Brushes.Gray,
                                   new Point(cx + diam, cy));
            }
        }

        private void CiudadForm_Load(object sender, EventArgs e)
        {
            //pictureBox1.Invalidate();
            /*
            *//*Image img = Imagenes.mapamexico;
            Image img_red = ScaleImage(img, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = img_red;*//*

            Image img = pictureBox1.Image;
            Graphics grafica = pictureBox1.CreateGraphics();
            //Graphics grafica = Graphics.FromImage(bmp);
            SolidBrush Brush = new SolidBrush(Color.Red);
            // tamaño del círculo
            int ancho = 10;
            int alto = 10;

            string con = Properties.Settings.Default.MapaConnectionString;
            SqlConnection cnn = new SqlConnection(con);

            cnn.Open();
            string query = "SELECT Nombre, CoordX, CoordY FROM Ciudad";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                string nom = rdr[0].ToString();
                int cx = (int)rdr[1];
                int cy = (int)rdr[2];
                *//*int pbX = (int)(cx * ((double)img.Width / pictureBox1.Width));
                int pbY = (int)(cy * ((double)img.Height / pictureBox1.Height));
                Console.WriteLine("{0} * ({1}/{2}) = {3}", cy, img.Height, pictureBox1.Height, pbY);*/
                /*Console.WriteLine("x:{0}, y: {1}", pbX, pbY);*//*
                // Dibuja un círculo
                grafica.FillEllipse(Brush, cx, cy, ancho, alto);
                // Coloca el nombre de la ciudad
                grafica.DrawString(nom,
                                   new Font("Arial", 10),
                                   Brushes.Gray,
                                   new Point(cx + 10, cy + 10));
            }
            pictureBox1.Refresh();
            rdr.Close();*/
        }
        private Bitmap ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            double ratioX = (double)maxWidth / image.Width;
            double ratioY = (double)maxHeight / image.Height;
            double ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(image.Width * ratio);
            int newHeight = (int)(image.Height * ratio);

            Bitmap newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }

    }
}