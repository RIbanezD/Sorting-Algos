namespace AlgoritmosOrdenamiento_Ruben_Ibañez.Algoritmos
{
    // Compara pares de elementos adyacentes y los intercambia si están desordenados.
    public class BubbleSort : AlgoritmoOrdenamiento
    {
        public override string Nombre => "Bubble Sort";

        protected override void EjecutarOrdenamiento(int[] datos)
        {
            int n = datos.Length;

            for (int i = 0; i < n - 1; i++)
            {
                bool huboIntercambio = false;

                for (int j = 0; j < n - 1 - i; j++)
                {
                    RegistrarComparacion(datos, j, j + 1);

                    if (datos[j] > datos[j + 1])
                    {
                        Intercambiar(datos, j, j + 1);
                        huboIntercambio = true;
                    }
                }

                // Si en una pasada completa no hubo intercambios, ya está ordenado
                if (!huboIntercambio)
                    break;
            }
        }
    }
}
