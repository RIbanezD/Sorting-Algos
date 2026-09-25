using System.Text;
using AlgoritmosOrdenamiento_Ruben_Ibañez.Algoritmos;

namespace AlgoritmosOrdenamiento_Ruben_Ibañez.Servicios
{
    // Grafico de barras verticales para animar
    public class VisualizadorBarras
    {
        private const char CaracterBarra = '█';
        private const char CaracterEje = '─';
        private const int FilaGrafico = 5;      // fila de la consola donde empieza el gráfico

        private int _anchoBarra;
        private int _separacion;
        private int _filasPositivas;
        private int _filasNegativas;
        private double _maximoPositivo;
        private double _maximoNegativo;
        private int _minimo;
        private int _maximo;

        // Pausa (en milisegundos) entre cada paso de la animación.
        public int RetardoMs { get; set; } = 60;

        // Cantidad máxima de barras que caben en el ancho actual de la consola.
        public static int CapacidadMaxima()
        {
            return Math.Max(10, (ObtenerAnchoConsola() - 1) / 2);
        }

        // Anima el algoritmo sobre los datos. Devuelve la lista ordenada
        public int[]? Visualizar(AlgoritmoOrdenamiento algoritmo, int[] original)
        {
            ConfigurarEscala(original);

            Console.Clear();
            OcultarCursor();
            DibujarMarco(algoritmo.Nombre, original.Length);

            int[]? resultado = null;
            try
            {
                // Estado inicial
                Dibujar(original, -1, -1, -1, TipoPaso.Comparacion, -1, new EstadisticasOrdenamiento());
                Thread.Sleep(700);

                // En cada paso del algoritmo se redibuja el gráfico
                algoritmo.AlPaso = (datos, indiceA, indiceB, pivote, tipo) =>
                {
                    VerificarCancelacion();
                    Dibujar(datos, indiceA, indiceB, pivote, tipo, -1, algoritmo.Estadisticas);
                    Thread.Sleep(RetardoMs);
                };

                resultado = algoritmo.Ordenar(original);

                // Barras en verde para indicar que la lista quedó ordenada
                for (int k = 0; k < resultado.Length; k++)
                {
                    Dibujar(resultado, -1, -1, -1, TipoPaso.Comparacion, k, algoritmo.Estadisticas);
                    Thread.Sleep(Math.Max(10, RetardoMs / 3));
                }
            }
            catch (OperationCanceledException)
            {
                resultado = null;
            }
            finally
            {
                algoritmo.AlPaso = null;
                Console.ResetColor();
                MostrarCursor();
            }

            // Dejar el cursor debajo del gráfico
            IrAFila(FilaGrafico + _filasPositivas + _filasNegativas + 5);
            return resultado;
        }

        // Configs
        // Calcula escala segun rango
        private void ConfigurarEscala(int[] datos)
        {
            _minimo = datos[0];
            _maximo = datos[0];
            for (int i = 1; i < datos.Length; i++)
            {
                if (datos[i] < _minimo) _minimo = datos[i];
                if (datos[i] > _maximo) _maximo = datos[i];
            }

            _maximoPositivo = Math.Max(0L, (long)_maximo);
            _maximoNegativo = Math.Max(0L, -(long)_minimo);

            // Ancho de cada barra: 2 columnas + 1 de separación si caben
            int ancho = ObtenerAnchoConsola();
            if (datos.Length * 3 <= ancho - 1)
            {
                _anchoBarra = 2;
                _separacion = 1;
            }
            else
            {
                _anchoBarra = 1;
                _separacion = 1;
            }

            // Alto total del gráfico según el alto de la consola
            int alturaTotal = Math.Clamp(ObtenerAltoConsola() - 11, 8, 20);

            // Repartir las filas entre la parte positiva y la negativa de forma proporcional
            if (_maximoNegativo == 0)
            {
                _filasPositivas = alturaTotal;
                _filasNegativas = 0;
            }
            else if (_maximoPositivo == 0)
            {
                _filasPositivas = 0;
                _filasNegativas = alturaTotal;
            }
            else
            {
                int positivas = (int)Math.Round(alturaTotal * _maximoPositivo / (_maximoPositivo + _maximoNegativo));
                _filasPositivas = Math.Clamp(positivas, 1, alturaTotal - 1);
                _filasNegativas = alturaTotal - _filasPositivas;
            }
        }

        // Dibujo
        // título, leyenda
        private void DibujarMarco(string nombre, int cantidad)
        {
            int ancho = ObtenerAnchoConsola();

            EscribirLinea(0, $" VISUALIZACIÓN: {nombre.ToUpper()}  ({cantidad} elementos)", ConsoleColor.Cyan);
            EscribirLinea(1, new string('═', Math.Max(10, ancho - 1)), ConsoleColor.DarkGray);
            EscribirLinea(3, $" Rango de valores: {_minimo} a {_maximo}", ConsoleColor.Gray);

            int filaLeyenda = FilaGrafico + _filasPositivas + _filasNegativas + 2;
            IrAFila(filaLeyenda);
            Console.Write(" ");
            EscribirSegmento(CaracterBarra.ToString(), ConsoleColor.Cyan);
            Console.Write(" Sin procesar   ");
            EscribirSegmento(CaracterBarra.ToString(), ConsoleColor.Yellow);
            Console.Write(" Comparando   ");
            EscribirSegmento(CaracterBarra.ToString(), ConsoleColor.Red);
            Console.Write(" Intercambio/Escritura   ");
            EscribirSegmento(CaracterBarra.ToString(), ConsoleColor.Magenta);
            Console.Write(" Pivote   ");
            EscribirSegmento(CaracterBarra.ToString(), ConsoleColor.Green);
            Console.Write(" Ordenado");

            EscribirLinea(filaLeyenda + 1, " Presione ESC para cancelar la animación", ConsoleColor.DarkGray);
        }

        // fps
        private void Dibujar(int[] datos, int indiceA, int indiceB, int pivote, TipoPaso tipo,
                             int verdeHasta, EstadisticasOrdenamiento estadisticas)
        {
            int n = datos.Length;

            EscribirLinea(2,
                $" Comparaciones: {estadisticas.Comparaciones,-8} Movimientos: {estadisticas.Movimientos,-8} Retardo: {RetardoMs} ms",
                ConsoleColor.White);

            // Color de cada barra según lo que el algoritmo esté haciendo
            var colores = new ConsoleColor[n];
            for (int k = 0; k < n; k++)
            {
                if (k <= verdeHasta) colores[k] = ConsoleColor.Green;
                else if (k == pivote) colores[k] = ConsoleColor.Magenta;
                else if (k == indiceA || k == indiceB)
                    colores[k] = tipo == TipoPaso.Comparacion ? ConsoleColor.Yellow : ConsoleColor.Red;
                else colores[k] = ConsoleColor.Cyan;
            }

            // Parte positiva (de arriba hacia el eje)
            for (int nivel = _filasPositivas; nivel >= 1; nivel--)
            {
                IrAFila(FilaGrafico + (_filasPositivas - nivel));
                DibujarFila(datos, colores, nivel, true);
            }

            // Eje
            IrAFila(FilaGrafico + _filasPositivas);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(new string(CaracterEje, n * (_anchoBarra + _separacion)));

            // Parte negativa (del eje hacia abajo)
            for (int nivel = 1; nivel <= _filasNegativas; nivel++)
            {
                IrAFila(FilaGrafico + _filasPositivas + nivel);
                DibujarFila(datos, colores, nivel, false);
            }

            Console.ResetColor();
        }

        // Dibuja una fila horizontal del gráfico.
        private void DibujarFila(int[] datos, ConsoleColor[] colores, int nivel, bool positiva)
        {
            var texto = new StringBuilder();
            ConsoleColor? colorActual = null;

            for (int k = 0; k < datos.Length; k++)
            {
                bool lleno;
                if (positiva)
                    lleno = datos[k] > 0 && AlturaPositiva(datos[k]) >= nivel;
                else
                    lleno = datos[k] < 0 && AlturaNegativa(datos[k]) >= nivel;

                ConsoleColor? colorCelda = lleno ? colores[k] : null;

                if (colorCelda != colorActual)
                {
                    VaciarTexto(texto, colorActual);
                    colorActual = colorCelda;
                }

                if (lleno)
                    texto.Append(new string(CaracterBarra, _anchoBarra)).Append(' ', _separacion);
                else
                    texto.Append(' ', _anchoBarra + _separacion);
            }

            VaciarTexto(texto, colorActual);
        }

        private void VaciarTexto(StringBuilder texto, ConsoleColor? color)
        {
            if (texto.Length == 0) return;

            if (color.HasValue)
                Console.ForegroundColor = color.Value;

            Console.Write(texto.ToString());
            texto.Clear();
        }

        // Altura de una barra positiva.
        private int AlturaPositiva(int valor)
        {
            return Math.Max(1, (int)Math.Round(valor * _filasPositivas / _maximoPositivo));
        }

        // Altura de una barra negativa.
        private int AlturaNegativa(int valor)
        {
            return Math.Max(1, (int)Math.Round(-(double)valor * _filasNegativas / _maximoNegativo));
        }

        // Utils
        private static void EscribirLinea(int fila, string texto, ConsoleColor color)
        {
            int ancho = ObtenerAnchoConsola() - 1;
            if (texto.Length > ancho) texto = texto.Substring(0, ancho);

            IrAFila(fila);
            Console.ForegroundColor = color;
            Console.Write(texto.PadRight(ancho));   // el relleno borra restos del frame anterior
            Console.ResetColor();
        }

        private static void EscribirSegmento(string texto, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(texto);
            Console.ResetColor();
        }

        private static void IrAFila(int fila)
        {
            try { Console.SetCursorPosition(0, fila); }
            catch (ArgumentOutOfRangeException) {}
        }

        // Permite cancelar la animación con Esc.
        private static void VerificarCancelacion()
        {
            try
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                    throw new OperationCanceledException();
            }
            catch (InvalidOperationException) {}
        }

        private static int ObtenerAnchoConsola()
        {
            try { return Math.Max(40, Console.WindowWidth); }
            catch (IOException) { return 80; }
        }

        private static int ObtenerAltoConsola()
        {
            try { return Math.Max(10, Console.WindowHeight); }
            catch (IOException) { return 30; }
        }

        private static void OcultarCursor()
        {
            try { Console.CursorVisible = false; } catch (IOException) { }
        }

        private static void MostrarCursor()
        {
            try { Console.CursorVisible = true; } catch (IOException) { }
        }
    }
}
