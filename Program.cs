using System.Diagnostics;
using System.Globalization;
using AlgoritmosOrdenamiento_Ruben_Ibañez.Algoritmos;
using AlgoritmosOrdenamiento_Ruben_Ibañez.Servicios;
using AlgoritmosOrdenamiento_Ruben_Ibañez.Utilidades;
using Spectre.Console;

namespace AlgoritmosOrdenamiento_Ruben_Ibañez
{
    public class Program
    {
        private static readonly ListaNumeros lista = new ListaNumeros();

        // Retardo de la animación ajustable (ms)
        private static int retardoMs = 60;

        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            MostrarBienvenida();

            bool salir = false;
            while (!salir)
            {
                MostrarEncabezado();
                MostrarMenuPrincipal();

                string opcion = Validaciones.LeerTexto("Seleccione una opción:", permitirVacio: true);

                switch (opcion)
                {
                    case "1": RegistrarNumeros(); break;
                    case "2": MostrarListaActual(); break;
                    case "3": Sorting(new BubbleSort()); break;
                    case "4": Sorting(new InsertionSort()); break;
                    case "5": Sorting(new MergeSort()); break;
                    case "6": Sorting(new QuickSort()); break;
                    case "7": CargarDatosPrueba(); break;
                    case "8": MenuVisualizacion(); break;
                    case "9":
                        salir = true;
                        break;
                    default:
                        AnsiConsole.MarkupLine("[red]Opción inválida. Presione una tecla para continuar...[/]");
                        Console.ReadKey(true);
                        break;
                }
            }

            AnsiConsole.Clear();
            var titulo = new FigletText("Hasta la vista malabarista")
            {
                Color = Color.Aqua,
                Justification = Justify.Center
            };
            
            var minitext = new Text("Ahí nos vidrios", new Style(Color.Grey))
            {
                Justification = Justify.Center
            };
            
            AnsiConsole.Write(titulo);
            AnsiConsole.Write(minitext);
            AnsiConsole.WriteLine();
            Validaciones.PausarYRegresarAlMenu();
            AnsiConsole.Clear();
        }

        // --- Menu ---
        private static void MostrarBienvenida()
        {
            AnsiConsole.Clear();
            var titulo = new FigletText("Sistema de Algoritmos de Ordenamiento")
            {
                Color = Color.Blue,
                Justification = Justify.Center
            };
            
            var minitext = new Text("Version 1.0 - DataSolutions", new Style(Color.Grey))
            {
                Justification = Justify.Center
            };
            
            AnsiConsole.Write(titulo);
            AnsiConsole.Write(minitext);
            AnsiConsole.WriteLine();
            Validaciones.PausarYRegresarAlMenu();
        }

        private static void MostrarEncabezado()
        {
            AnsiConsole.Clear();
            var regla = new Rule("[bold cyan]SISTEMA DE ALGORITMOS DE ORDENAMIENTO[/]")
            {
                Justification = Justify.Center,
                Style = Style.Parse("cyan")
            };
            AnsiConsole.Write(regla);
            AnsiConsole.WriteLine();
        }

        private static void MostrarMenuPrincipal()
        {
            var panel = new Panel(
                "[green][[1]][/] Registrar Números\n" +
                "[green][[2]][/] Mostrar Lista Actual\n" +
                "[green][[3]][/] Bubble Sort\n" +
                "[green][[4]][/] Insertion Sort\n" +
                "[green][[5]][/] Merge Sort\n" +
                "[green][[6]][/] Quick Sort\n" +
                "[green][[7]][/] Cargar Datos de Prueba aleatorios\n" +
                "[green][[8]][/] Visualizar Algoritmos\n" +
                "[green][[9]][/] Salir")
            {
                Header = new PanelHeader(" Menú Principal "),
                Border = BoxBorder.Rounded,
                BorderStyle = Style.Parse("cyan")
            };
            AnsiConsole.Write(panel);
            AnsiConsole.MarkupLine($"[grey]Números en la lista: {lista.Cantidad}[/]");
            AnsiConsole.WriteLine();
        }

        // -- Registrar numeros -- 

        private static void RegistrarNumeros()
        {
            MostrarEncabezado();
            AnsiConsole.Write(new Rule("[yellow]Registro de Números[/]").LeftJustified());
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[grey]Ingrese números enteros. Puede escribir varios separados por espacio. Deje vacío y presione Enter para terminar.[/]");
            AnsiConsole.WriteLine();

            int registrados = 0;
            while (true)
            {
                string entrada = Validaciones.LeerTexto("Número(s):", permitirVacio: true);
                if (string.IsNullOrWhiteSpace(entrada))
                    break;

                string[] partes = entrada.Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string parte in partes)
                {
                    if (int.TryParse(parte, NumberStyles.Integer, CultureInfo.InvariantCulture, out int numero))
                    {
                        lista.Agregar(numero);
                        registrados++;
                        AnsiConsole.MarkupLine($"[green]  + {numero} agregado.[/]");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine($"[red]  '{Markup.Escape(parte)}' no es un entero válido y fue ignorado.[/]");
                    }
                }
            }

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine($"[bold green]Se registraron {registrados} números.[/] [grey]Total en la lista: {lista.Cantidad}[/]");
            Validaciones.PausarYRegresarAlMenu();
        }

        // -- Mostrar lista --
        private static void MostrarListaActual()
        {
            MostrarEncabezado();
            AnsiConsole.Write(new Rule("[cyan]Lista Actual[/]").LeftJustified());
            AnsiConsole.WriteLine();

            if (lista.EstaVacia())
            {
                AnsiConsole.MarkupLine("[red]La lista está vacía. Registre números o cargue datos de prueba.[/]");
            }
            else
            {
                MostrarLista($"Lista Actual ({lista.Cantidad} elementos)", lista.ObtenerCopia(), "cyan");
            }

            Validaciones.PausarYRegresarAlMenu();
        }

        // -- Sorting -- 
        private static void Sorting(AlgoritmoOrdenamiento algoritmo)
        {
            MostrarEncabezado();
            AnsiConsole.Write(new Rule($"[yellow]{algoritmo.Nombre}[/]").LeftJustified());
            AnsiConsole.WriteLine();

            if (lista.EstaVacia())
            {
                AnsiConsole.MarkupLine("[red]La lista está vacía. Registre números o cargue datos de prueba primero.[/]");
                Validaciones.PausarYRegresarAlMenu();
                return;
            }

            int[] original = lista.ObtenerCopia();

            var cronometro = Stopwatch.StartNew();
            int[] ordenada = algoritmo.Ordenar(original);
            cronometro.Stop();

            MostrarResultado(algoritmo, original, ordenada, cronometro.Elapsed.TotalMilliseconds);
        }

        // Muestra la lista original, la lista ordenada y las estadísticas
        private static void MostrarResultado(AlgoritmoOrdenamiento algoritmo, int[] original, int[] ordenada, double? tiempoMs)
        {
            MostrarEncabezado();
            AnsiConsole.Write(new Rule($"[yellow]{algoritmo.Nombre}[/]").LeftJustified());
            AnsiConsole.WriteLine();

            MostrarLista("Lista Original", original, "yellow");
            AnsiConsole.WriteLine();
            MostrarLista("Lista Ordenada", ordenada, "green");
            AnsiConsole.WriteLine();

            var tabla = new Table().Border(TableBorder.Rounded).BorderColor(Color.Cyan1);
            tabla.AddColumn("Métrica");
            tabla.AddColumn("Valor");
            tabla.AddRow("Algoritmo", algoritmo.Nombre);
            tabla.AddRow("Elementos", original.Length.ToString());
            tabla.AddRow("Comparaciones", algoritmo.Estadisticas.Comparaciones.ToString());
            tabla.AddRow("Movimientos", algoritmo.Estadisticas.Movimientos.ToString());
            if (tiempoMs.HasValue)
                tabla.AddRow("Tiempo de ejecución", $"{tiempoMs.Value:F4} ms");
            AnsiConsole.Write(tabla);

            Validaciones.PausarYRegresarAlMenu();
        }

        // Muestra una lista de números
        private static void MostrarLista(string titulo, int[] numeros, string colorBorde)
        {
            var panel = new Panel(new Text(string.Join(", ", numeros)))
            {
                Header = new PanelHeader($" {titulo} "),
                Border = BoxBorder.Rounded,
                BorderStyle = Style.Parse(colorBorde),
                Expand = true
            };
            AnsiConsole.Write(panel);
        }

        private static void CargarDatosPrueba()
        {
            bool volver2 = false;
            while (!volver2)
            {
                MostrarEncabezado();
                var panel = new Panel(
                    "[green][[1]][/] Cargar Datos  \n" +
                    "[green][[2]][/] Regresar")
                {
                    Header = new PanelHeader(" Datos de Prueba "),
                    Border = BoxBorder.Rounded,
                    BorderStyle = Style.Parse("cyan")
                };
                AnsiConsole.Write(panel);
                AnsiConsole.WriteLine();

                string opcion = Validaciones.LeerTexto("Seleccione una opción:", permitirVacio: true);

                if (opcion == "1")
                {
                    AnsiConsole.Clear();
                    AnsiConsole.MarkupLine("[grey]Se generarán números enteros aleatorios dentro del rango que indique.[/]");
                    AnsiConsole.MarkupLine($"[grey]Para la visualización se recomienda no exceder de {VisualizadorBarras.CapacidadMaxima()} números.[/]");
                    AnsiConsole.WriteLine();
                    int cantidad = Validaciones.LeerEntero("Cantidad de números a generar:", 1, 1000, 20);
                    int minimo = Validaciones.LeerEntero("Valor mínimo del rango:", -1000000, 1000000, -100);
                    int maximo = Validaciones.LeerEntero("Valor máximo del rango:", minimo, 1000000, 100);

                    int[] datos = GeneradorDatosPrueba.Generar(cantidad, minimo, maximo);

                    bool reemplazar = true;
                    if (!lista.EstaVacia())
                    {
                        reemplazar = Validaciones.Confirmar(
                            $"La lista ya tiene {lista.Cantidad} números. ¿Desea reemplazarla? (No = agregar al final)");
                    }

                    if (reemplazar)
                        lista.Reemplazar(datos);
                    else
                        lista.AgregarRango(datos);

                    AnsiConsole.WriteLine();
                    AnsiConsole.MarkupLine($"[bold green]Se generaron {cantidad} números entre {minimo} y {maximo}.[/] [grey]Total en la lista: {lista.Cantidad}[/]");
                    MostrarLista("Datos generados", datos, "green");
                    volver2 = true;
                    Validaciones.PausarYRegresarAlMenu();
                }
                else if(opcion == "2")
                {
                    AnsiConsole.MarkupLine("[yellow]Regresando al menú principal...[/]");
                    Console.ReadKey(true);
                    volver2 = true;
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Opción inválida.[/]");
                    Console.ReadKey(true);
                }
            }
        }

        // visualizacion de algoritmos
        private static void MenuVisualizacion()
        {
            // Si no hay datos, genera de prueba para animar
            if (lista.EstaVacia())
            {
                MostrarEncabezado();
                AnsiConsole.MarkupLine("[red]La lista está vacía.[/]");
                if (Validaciones.Confirmar("¿Desea cargar datos de prueba ahora?"))
                    CargarDatosPrueba();

                if (lista.EstaVacia())
                    return;
            }

            bool volver = false;
            while (!volver)
            {
                MostrarEncabezado();
                var panel = new Panel(
                    "[green][[1]][/] Visualizar Bubble Sort\n" +
                    "[green][[2]][/] Visualizar Insertion Sort\n" +
                    "[green][[3]][/] Visualizar Merge Sort\n" +
                    "[green][[4]][/] Visualizar Quick Sort\n" +
                    "[green][[5]][/] Cambiar velocidad de la animación\n" +
                    "[green][[6]][/] Volver al Menú Principal")
                {
                    Header = new PanelHeader(" Visualización de Algoritmos "),
                    Border = BoxBorder.Rounded,
                    BorderStyle = Style.Parse("cyan")
                };
                AnsiConsole.Write(panel);
                AnsiConsole.MarkupLine($"[grey]Retardo actual: {retardoMs} ms por paso | Números en la lista: {lista.Cantidad}[/]");
                AnsiConsole.WriteLine();

                string opcion = Validaciones.LeerTexto("Seleccione una opción:", permitirVacio: true);

                switch (opcion)
                {
                    case "1": EjecutarVisualizacion(new BubbleSort()); break;
                    case "2": EjecutarVisualizacion(new InsertionSort()); break;
                    case "3": EjecutarVisualizacion(new MergeSort()); break;
                    case "4": EjecutarVisualizacion(new QuickSort()); break;
                    case "5": CambiarVelocidad(); break;
                    case "6": volver = true; break;
                    default:
                        AnsiConsole.MarkupLine("[red]Opción inválida.[/]");
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        private static void CambiarVelocidad()
        {
            MostrarEncabezado();
            AnsiConsole.Write(new Rule("[yellow]Velocidad de la Animación[/]").LeftJustified());
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[green][[1]][/] Lenta   (150 ms)");
            AnsiConsole.MarkupLine("[green][[2]][/] Normal  (60 ms)");
            AnsiConsole.MarkupLine("[green][[3]][/] Rápida  (15 ms)");
            AnsiConsole.WriteLine();

            int opcion = Validaciones.LeerEntero("Seleccione la velocidad:", 1, 3);
            retardoMs = opcion == 1 ? 150 : opcion == 2 ? 60 : 15;

            AnsiConsole.MarkupLine($"[green]Retardo establecido en {retardoMs} ms.[/]");
            Validaciones.PausarYRegresarAlMenu();
        }

        private static void EjecutarVisualizacion(AlgoritmoOrdenamiento algoritmo)
        {
            int[] original = lista.ObtenerCopia();

            var visualizador = new VisualizadorBarras { RetardoMs = retardoMs };
            int[]? ordenada = visualizador.Visualizar(algoritmo, original);

            if (ordenada == null)
            {
                AnsiConsole.MarkupLine("[yellow]Visualización cancelada.[/]");
                Validaciones.PausarYRegresarAlMenu();
                return;
            }

            Validaciones.Pausar("Ordenamiento finalizado. Presione una tecla para ver el resumen...");
            MostrarResultado(algoritmo, original, ordenada, null);
        }
    }
}
