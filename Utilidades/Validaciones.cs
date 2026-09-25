using Spectre.Console;

namespace AlgoritmosOrdenamiento_Ruben_Ibañez.Utilidades
{
    public static class Validaciones
    {
        public static string LeerTexto(string mensaje, bool permitirVacio = false)
        {
            var prompt = new TextPrompt<string>(mensaje)
            {
                AllowEmpty = permitirVacio,
                PromptStyle = Style.Parse("green")
            };

            return AnsiConsole.Prompt(prompt).Trim();
        }

        // Lee un entero dentro del rango
        public static int LeerEntero(string mensaje, int minimo, int maximo, int? valorPorDefecto = null)
        {
            var prompt = new TextPrompt<int>(mensaje)
                .ValidationErrorMessage("[red]Debe ingresar un número entero válido.[/]")
                .Validate(valor => valor >= minimo && valor <= maximo
                    ? ValidationResult.Success()
                    : ValidationResult.Error($"[red]El valor debe estar entre {minimo} y {maximo}.[/]"));

            if (valorPorDefecto.HasValue)
                prompt.DefaultValue(valorPorDefecto.Value);

            return AnsiConsole.Prompt(prompt);
        }

        public static bool Confirmar(string mensaje, bool valorPorDefecto = true)
        {
            return AnsiConsole.Confirm(mensaje, valorPorDefecto);
        }

        public static void Pausar(string mensaje)
        {
            AnsiConsole.MarkupLine($"[grey]{mensaje}[/]");
            Console.ReadKey(true);
        }

        public static void PausarYRegresarAlMenu()
        {
            AnsiConsole.WriteLine();
            Pausar("Presione una tecla para regresar al menú...");
        }
    }
}
