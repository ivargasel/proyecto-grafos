namespace Proyecto_Grafos
{
    public class RutasResultado
    {
        private static string RutaDijkstra { get; set; }
        private static string RutaFloyd { get; set; }
        private static string RutaWarshall { get; set; }

        public string SetRutaDijkstra(string _ruta)
        {
            RutaDijkstra = _ruta;
            return RutaDijkstra;
        }

        public string SetRutaFloyd(string _ruta)
        {
            RutaFloyd = _ruta;
            return RutaFloyd;
        }

        public string SetRutaWarshall(string _ruta)
        {
            RutaWarshall = _ruta;
            return RutaWarshall;
        }

        public string GetRutaDijkstra()
        {
            return RutaDijkstra;
        }

        public string GetRutaFloyd()
        {
            return RutaFloyd;
        }

        public string GetRutaWarshall()
        {
            return RutaWarshall;
        }
    }
}