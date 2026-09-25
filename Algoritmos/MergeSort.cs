namespace AlgoritmosOrdenamiento_Ruben_Ibañez.Algoritmos
{
    // Divide el arreglo a la mitad, ordena cada mitad y luego mezcla ambas mitades ya ordenadas
    public class MergeSort : AlgoritmoOrdenamiento
    {
        public override string Nombre => "Merge Sort";

        protected override void EjecutarOrdenamiento(int[] datos)
        {
            if (datos.Length < 2) return;

            int[] auxiliar = new int[datos.Length];
            Dividir(datos, auxiliar, 0, datos.Length - 1);
        }

        private void Dividir(int[] datos, int[] auxiliar, int izquierda, int derecha)
        {
            if (izquierda >= derecha) return;  

            int medio = izquierda + (derecha - izquierda) / 2;

            Dividir(datos, auxiliar, izquierda, medio);
            Dividir(datos, auxiliar, medio + 1, derecha);
            Mezclar(datos, auxiliar, izquierda, medio, derecha);
        }

        // Mezcla las mitades ordenadas
        private void Mezclar(int[] datos, int[] auxiliar, int izquierda, int medio, int derecha)
        {
            // Lista auxiliar
            for (int k = izquierda; k <= derecha; k++)
                auxiliar[k] = datos[k];

            int i = izquierda;      // recorre la mitad izquierda
            int j = medio + 1;      // recorre la mitad derecha
            int destino = izquierda;

            while (i <= medio && j <= derecha)
            {
                Estadisticas.Comparaciones++;

                if (auxiliar[i] <= auxiliar[j])
                {
                    datos[destino] = auxiliar[i];
                    i++;
                }
                else
                {
                    datos[destino] = auxiliar[j];
                    j++;
                }

                RegistrarEscritura(datos, destino);
                destino++;
            }

            // Sobrantes de la mitad izquierda
            while (i <= medio)
            {
                datos[destino] = auxiliar[i];
                RegistrarEscritura(datos, destino);
                i++;
                destino++;
            }
        }
    }
}
