using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto_Grafos
{
    public class GrafosClass
    {
        private RutasResultado rutasResultado = new RutasResultado();
        private int[,] matriz;
        private int nodos;
        public Dictionary<string, int> mapaIndices = new Dictionary<string, int> {
            {"Parque Norte", 0}, {"Familia Herrera", 1}, {"CC Norte", 2}, 
            {"Familia Pérez", 3}, {"Parque Central", 4}, {"Hospital", 5},
            {"UCR", 6}, {"Iglesia", 7}, {"CC Sur", 8},
            {"Familia López", 9}, {"Parque Sur", 10}, {"Familia Rodríguez", 11}
        };

        public Dictionary<int, string> mapaLetras = new Dictionary<int, string> {
            {0, "Parque Norte"}, {1, "Familia Herrera"}, {2, "CC Norte"},
            {3 , "Familia Pérez"}, {4 , "Parque Central"}, {5 , "Hospital"},
            {6 , "UCR"}, {7 , "Iglesia"}, {8 , "CC Sur"},
            {9 , "Familia López"}, {10 , "Parque Sur"}, {11 , "Familia Rodríguez"}
        };

        public GrafosClass()
        {
            nodos = 12;
            matriz = new int[12, 12] 
            {
                { 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0 }, // Parque Norte
                { 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0 }, // Familia Herrera
                { 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0 }, // CC Norte
                { 3, 0, 0, 0, 5, 0, 4, 0, 0, 0, 0, 0 }, // Familia Pérez
                { 0, 4, 0, 5, 0, 4, 0, 2, 0, 0, 0, 0 }, // Parque Central
                { 0, 0, 2, 0, 4, 0, 0, 0, 3, 0, 0, 0 }, // Hospital
                { 0, 0, 0, 4, 0, 0, 0, 3, 0, 2, 0, 0 }, // UCR
                { 0, 0, 0, 0, 2, 0, 3, 0, 1, 0, 1, 0 }, // Iglesia
                { 0, 0, 0, 0, 0, 3, 0, 1, 0, 0, 0, 2 }, // CC Sur
                { 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 5, 0 }, // Familia López
                { 0, 0, 0, 0, 0, 0, 0, 1, 0, 5, 0, 3 }, // Parque Sur
                { 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 3, 0 }  // Familia Rodríguez
            };            
        }

        public void Dijkstra(string inicio, string fin)
        {
            string value = string.Empty;
            int origen = mapaIndices[inicio];
            int destino = mapaIndices[fin];

            int[] dist = new int[nodos];
            bool[] visitado = new bool[nodos];
            int[] previo = new int[nodos];

            for (int i = 0; i < nodos; i++)
            {
                dist[i] = int.MaxValue;
                visitado[i] = false;
                previo[i] = -1;
            }

            dist[origen] = 0;

            for (int count = 0; count < nodos - 1; count++)
            {
                int u = MinDistancia(dist, visitado);
                if (u == -1) break;
                visitado[u] = true;

                for (int v = 0; v < nodos; v++)
                {
                    if (!visitado[v] && matriz[u, v] > 0 &&
                        dist[u] != int.MaxValue &&
                        dist[u] + matriz[u, v] < dist[v])
                    {
                        dist[v] = dist[u] + matriz[u, v];
                        previo[v] = u;
                    }
                }
            }

            var response = new StringBuilder();
            response.AppendLine($"Ruta más corta de {inicio} a {fin}: {dist[destino]}");
            response.Append(MostrarRuta(previo, destino, response.ToString()));
            rutasResultado.SetRutaDijkstra(response.ToString());
        }

        private int MinDistancia(int[] dist, bool[] visitado)
        {
            int min = int.MaxValue, min_index = -1;
            for (int v = 0; v < nodos; v++)
            {
                if (!visitado[v] && dist[v] <= min)
                {
                    min = dist[v];
                    min_index = v;
                }
            }
            return min_index;
        }

        public string MostrarRuta(int[] previo, int destino, string message)
        {

            var value = new StringBuilder();
            if (previo[destino] == -1)
            {
                value.AppendLine("No hay ruta.");
                return null;
            }

            Stack<int> ruta = new Stack<int>();
            int actual = destino;
            while (actual != -1)
            {
                ruta.Push(actual);
                actual = previo[actual];
            }

            value.AppendLine("Ruta: ");
            while (ruta.Count > 0)
            {
                value.AppendLine(mapaLetras[ruta.Pop()] + " ");
            }

            return value.ToString();
        }

        public void Floyd()
        {
            var value = new StringBuilder();
            int[,] dist = new int[nodos, nodos];
            int[,] next = new int[nodos, nodos];

            for (int i = 0; i < nodos; i++)
            {
                for (int j = 0; j < nodos; j++)
                {
                    dist[i, j] = matriz[i, j] > 0 ? matriz[i, j] : (i == j ? 0 : int.MaxValue);
                    next[i, j] = matriz[i, j] > 0 ? j : -1;
                }
            }

            for (int k = 0; k < nodos; k++)
            {
                for (int i = 0; i < nodos; i++)
                {
                    for (int j = 0; j < nodos; j++)
                    {
                        if (dist[i, k] != int.MaxValue && dist[k, j] != int.MaxValue &&
                            dist[i, k] + dist[k, j] < dist[i, j])
                        {
                            dist[i, j] = dist[i, k] + dist[k, j];
                            next[i, j] = next[i, k];
                        }
                    }
                }
            }

            value.AppendLine("Todas las rutas más cortas:");
            for (int i = 0; i < nodos; i++)
            {
                for (int j = 0; j < nodos; j++)
                {
                    if (i != j && dist[i, j] != int.MaxValue)
                    {
                        value.AppendLine($"Ruta de {mapaLetras[i]} a {mapaLetras[j]} (dist: {dist[i, j]}): ");
                        value.AppendLine(MostrarRutaFloyd(next, i, j));
                    }
                }
            }

            rutasResultado.SetRutaFloyd(value.ToString());
        }

        private string MostrarRutaFloyd(int[,] next, int u, int v)
        {
            var value = new StringBuilder();
            if (next[u, v] == -1)
            {
                value.AppendLine(" No hay ruta.");
                return value.ToString();
            }

            value.AppendLine($"{mapaLetras[u]}");
            while (u != v)
            {
                u = next[u, v];
                value.AppendLine($" > {mapaLetras[u]}");
            }
           
            return value.ToString();
        }

        public void Warshall()
        {
            bool[,] alcance = new bool[nodos, nodos];
            for (int i = 0; i < nodos; i++)
                for (int j = 0; j < nodos; j++)
                    alcance[i, j] = matriz[i, j] > 0;

            for (int k = 0; k < nodos; k++)
                for (int i = 0; i < nodos; i++)
                    for (int j = 0; j < nodos; j++)
                        alcance[i, j] = alcance[i, j] || (alcance[i, k] && alcance[k, j]);

            var response = new StringBuilder();
            response.AppendLine("Matriz de Alcance (1 = hay camino):");
            for (int i = 0; i < nodos; i++)
            {
                for (int j = 0; j < nodos; j++)
                {
                    response.Append((alcance[i, j] ? "1" : "0") + " ");
                }
                response.AppendLine("");
            }

            rutasResultado.SetRutaWarshall(response.ToString());
        }
    }    
}
