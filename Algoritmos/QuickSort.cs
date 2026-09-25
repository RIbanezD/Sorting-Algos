namespace AlgoritmosOrdenamiento_Ruben_Ibañez.Algoritmos
{
    // Elige un pivote, coloca a su izquierda los menores y a su derecha los mayores
    public class QuickSort : AlgoritmoOrdenamiento
    {
        public override string Nombre => "Quick Sort";

        protected override void EjecutarOrdenamiento(int[] datos)
        {
            OrdenarRango(datos, 0, datos.Length - 1);
        }

        private void OrdenarRango(int[] datos, int bajo, int alto)
        {
            while (bajo < alto)
            {
                int posicionPivote = Particionar(datos, bajo, alto);

                // Se procesa recursivamente la parte más pequeña y se itera sobre la más grande.
                if (posicionPivote - bajo < alto - posicionPivote)
                {
                    OrdenarRango(datos, bajo, posicionPivote - 1);
                    bajo = posicionPivote + 1;
                }
                else
                {
                    OrdenarRango(datos, posicionPivote + 1, alto);
                    alto = posicionPivote - 1;
                }
            }
        }

        // Partición de Lomuto - El pivote es el elemento central
        private int Particionar(int[] datos, int bajo, int alto)
        {
            // Mover el pivote al final del rango
            int medio = bajo + (alto - bajo) / 2;
            if (medio != alto)
                Intercambiar(datos, medio, alto);

            int valorPivote = datos[alto];
            int frontera = bajo - 1;                                    // último índice de la zona de elementos <= pivote

            for (int j = bajo; j < alto; j++)
            {
                RegistrarComparacion(datos, j, -1, alto);

                if (datos[j] <= valorPivote)
                {
                    frontera++;
                    if (frontera != j)
                        Intercambiar(datos, frontera, j, alto);
                }
            }

            if (frontera + 1 != alto)
                Intercambiar(datos, frontera + 1, alto);

            return frontera + 1;
        }
    }
}
