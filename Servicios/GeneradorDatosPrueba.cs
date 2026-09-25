namespace AlgoritmosOrdenamiento_Ruben_Ibañez.Servicios
{
    // Genera números enteros aleatorios
    public static class GeneradorDatosPrueba
    {
        // Genera x números aleatorios en el rango
        public static int[] Generar(int cantidad, int minimo, int maximo)
        {
            var aleatorio = new Random();
            int[] datos = new int[cantidad];

            for (int i = 0; i < cantidad; i++)
                datos[i] = aleatorio.Next(minimo, maximo + 1);

            return datos;
        }
    }
}
