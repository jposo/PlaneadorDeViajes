using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProyectoFinal
{
    public static class Datos
    {
        public static Dictionary<string, (int cx, int cy)> Ciudades()
        {
            var ciudades = new Dictionary<string, (int cx, int cy)>();
            string[] lineas = File.ReadAllLines("ciudades.txt");

            foreach (string linea in lineas)
            {
                if (string.IsNullOrWhiteSpace(linea))
                    continue;
                string[] split = linea.Split('\t');
                string nombre = split[0];
                int cx = Convert.ToInt32(split[1]);
                int cy = Convert.ToInt32(split[2]);
                ciudades.Add(nombre, (cx, cy));
            }
            return ciudades;
        }
        public static Dictionary<string, 
            (string inicio, string destino, int distancia, int tiempoAR, int costoAR, int tiempoTP, int costoTP)> 
            Rutas()
        {
            var rutas = new Dictionary<string, (string inicio, string destino, int distancia, int tiempoAR, int costoAR, int tiempoTP, int costoTP)>();
            string[] lineas = File.ReadAllLines("rutas.txt");

            foreach (string linea in lineas)
            {
                if (string.IsNullOrWhiteSpace(linea))
                    continue;
                string[] split = linea.Split('\t');
                string nombre = split[0];
                string inicio = split[1];
                string destino = split[2];
                int distancia = Convert.ToInt32(split[3]);
                int tAR = Convert.ToInt32(split[4]);
                int cAR = Convert.ToInt32(split[5]);
                int tTP = Convert.ToInt32(split[6]);
                int cTP = Convert.ToInt32(split[7]);

                rutas.Add(nombre, (inicio, destino, distancia, tAR, cAR, tTP, cTP));
            }
            return rutas;
        }
        private static int EncontrarIndice(string[] arr, string valor)
        {
            for (int i = 0; i < arr.Length; i++)
                if (arr[i].Equals(valor))
                    return i;
           
            return -1;
        }
        private static (string inicio, string destino, int distancia, int tiempoAR, int costoAR, int tiempoTP, int costoTP) EncontrarArista(
            Dictionary<string, (string inicio, string destino, int distancia, int tiempoAR, int costoAR, int tiempoTP, int costoTP)> arista, 
            string valor)
        {
            foreach (var item in arista.Values.ToArray())
            {
                if (item.inicio.Equals(valor) || item.destino.Equals(valor))
                    return item;
            }
            return ("", "", 0, 0, 0, 0, 0);
        }
        /// <summary>
        /// posibles transportes: ar | tp ,,, 
        /// posibles criterios: dist | tiempo | costo
        /// </summary>
        public static int[,] GenerarMatriz(string transporte, string criterio)
        {
            string[] nodos = Ciudades().Keys.ToArray();
            var aristas = Rutas();
            int n = nodos.Length;
            int[,] matriz = new int[n,n];
            // llenar a partir de los criterios
            foreach (string nodo in nodos)
            {
                // obtener datos de la arista de la ciudad
                var arista = EncontrarArista(aristas, nodo);
                int idxInicio = EncontrarIndice(nodos, arista.inicio);
                int idxDestino = EncontrarIndice(nodos, arista.destino);
                int peso = 0;
                if (criterio == "dist") // no se ocupa saber el transporte
                    peso = arista.distancia;
                else if (criterio == "tiempo")
                {
                    if (transporte == "ar")
                        peso = arista.tiempoAR;
                    else if (transporte == "tp")
                        peso = arista.tiempoTP;
                }
                else if (criterio == "costo")
                {
                    if (transporte == "ar")
                        peso = arista.costoAR;
                    else if (transporte == "tp")
                        peso = arista.costoTP;
                }
                matriz[idxInicio, idxDestino] = peso;
                matriz[idxDestino, idxInicio] = peso;
            }
            return matriz;

            /*for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write("{0} ", matriz[i,j]);
                }
                Console.WriteLine();
            }*/
        }
    }
}
