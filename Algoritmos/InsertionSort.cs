namespace AlgoritmosOrdenamiento_Ruben_Ibañez.Algoritmos
{
    // Toma cada elemento y lo inserta en su lugar dentro de la porción ya ordenada
    public class InsertionSort : AlgoritmoOrdenamiento
    {
        public override string Nombre => "Insertion Sort";

        protected override void EjecutarOrdenamiento(int[] datos)
        {
            for (int i = 1; i < datos.Length; i++)
            {
                int clave = datos[i];   // elemento a insertar
                int j = i - 1;

                // Desplazar a la derecha los elementos mayores
                while (j >= 0)
                {
                    RegistrarComparacion(datos, j, j + 1);

                    if (datos[j] > clave)
                    {
                        datos[j + 1] = datos[j];
                        RegistrarEscritura(datos, j + 1);
                        j--;
                    }
                    else
                    {
                        break;
                    }
                }

                // Colocar la clave en su posición correcta
                if (j + 1 != i)
                {
                    datos[j + 1] = clave;
                    RegistrarEscritura(datos, j + 1);
                }
            }
        }
    }
}
