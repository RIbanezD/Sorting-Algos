namespace AlgoritmosOrdenamiento_Ruben_Ibañez.Servicios
{
    public class ListaNumeros
    {
        private readonly List<int> _numeros = new List<int>();

        public int Cantidad => _numeros.Count;

        public bool EstaVacia() => _numeros.Count == 0;

        public void Agregar(int numero)
        {
            _numeros.Add(numero);
        }

        public void AgregarRango(IEnumerable<int> numeros)
        {
            _numeros.AddRange(numeros);
        }

        public void Reemplazar(IEnumerable<int> numeros)
        {
            _numeros.Clear();
            _numeros.AddRange(numeros);
        }

        // Devuelve una copia en forma de arreglo
        public int[] ObtenerCopia()
        {
            int[] copia = new int[_numeros.Count];
            for (int i = 0; i < _numeros.Count; i++)
                copia[i] = _numeros[i];
            return copia;
        }
    }
}
