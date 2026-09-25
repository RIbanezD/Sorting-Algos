namespace AlgoritmosOrdenamiento_Ruben_Ibañez.Algoritmos
{
    // Tipo de acción que el algoritmo está realizando en un paso
    public enum TipoPaso
    {
        Comparacion,
        Intercambio,
        Escritura
    }

    // paso para dibujar el algoritmo
    public delegate void PasoOrdenamiento(int[] datos, int indiceA, int indiceB, int pivote, TipoPaso tipo);

    // Contadores
    public class EstadisticasOrdenamiento
    {
        public long Comparaciones { get; set; }
        public long Movimientos { get; set; }
    }

    public abstract class AlgoritmoOrdenamiento
    {
        public abstract string Nombre { get; }

        public PasoOrdenamiento? AlPaso { get; set; }

        public EstadisticasOrdenamiento Estadisticas { get; private set; } = new EstadisticasOrdenamiento();

        // Usa una copia para guardar la lista ordenada
        public int[] Ordenar(int[] original)
        {
            int[] copia = new int[original.Length];
            for (int i = 0; i < original.Length; i++)
                copia[i] = original[i];

            Estadisticas = new EstadisticasOrdenamiento();
            EjecutarOrdenamiento(copia);
            return copia;
        }

        protected abstract void EjecutarOrdenamiento(int[] datos);

        // Auxiliares
        protected void NotificarPaso(int[] datos, int indiceA, int indiceB, int pivote, TipoPaso tipo)
        {
            AlPaso?.Invoke(datos, indiceA, indiceB, pivote, tipo);
        }

        protected void RegistrarComparacion(int[] datos, int indiceA, int indiceB, int pivote = -1)
        {
            Estadisticas.Comparaciones++;
            NotificarPaso(datos, indiceA, indiceB, pivote, TipoPaso.Comparacion);
        }

        protected void Intercambiar(int[] datos, int indiceA, int indiceB, int pivote = -1)
        {
            (datos[indiceA], datos[indiceB]) = (datos[indiceB], datos[indiceA]);
            Estadisticas.Movimientos++;
            NotificarPaso(datos, indiceA, indiceB, pivote, TipoPaso.Intercambio);
        }

        protected void RegistrarEscritura(int[] datos, int indice)
        {
            Estadisticas.Movimientos++;
            NotificarPaso(datos, indice, -1, -1, TipoPaso.Escritura);
        }
    }
}
