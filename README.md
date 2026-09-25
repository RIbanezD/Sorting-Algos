# AlgoritmosOrdenamiento_Ruben_Ibañez

Aplicación de consola en C# que implementa manualmente los algoritmos **Bubble Sort**, **Insertion Sort**, **Merge Sort** y **Quick Sort**, con un menú interactivo, generación de datos de prueba aleatorios y visualización animada de cada algoritmo mediante gráficos de barras verticales.

Laboratorio 5 — Procesos y Algoritmos II — Universidad Regional de Guatemala — Segundo Semestre 2026.

## Contenido

- [Requisitos](#requisitos)
- [Cómo ejecutar](#cómo-ejecutar)
- [Estructura del proyecto](#estructura-del-proyecto)
- [Funcionalidades](#funcionalidades)
- [Algoritmos implementados](#algoritmos-implementados)

## Requisitos

- [.NET SDK 10.0](https://dotnet.microsoft.com/download) o superior.
- Paquete NuGet `Spectre.Console` (se restaura automáticamente con `dotnet restore` / `dotnet run`). 
- Una terminal con soporte UTF-8, idealmente maximizada (Terminal de Windows, PowerShell, o la consola integrada de VS Code).

## Cómo ejecutar

```bash
git clone https://github.com/RIbanezD/Sorting-Algos.git
cd AlgoritmosOrdenamiento_Ruben_Ibañez
dotnet run
```

También puedes abrir el archivo '.csproj' en Visual Studio y presionar `F5`.

## Estructura del proyecto

```
AlgoritmosOrdenamiento_Ruben_Ibañez/
├── Program.cs                          # Punto de entrada: menú principal y flujo del programa
├── Algoritmos/
│   ├── AlgoritmoOrdenamiento.cs        # Clase base abstracta (estadísticas, notificación de pasos)
│   ├── BubbleSort.cs
│   ├── InsertionSort.cs
│   ├── MergeSort.cs
│   └── QuickSort.cs
├── Servicios/
│   ├── ListaNumeros.cs                 # Conserva los números registrados durante la ejecución
│   ├── GeneradorDatosPrueba.cs         # Genera números aleatorios dentro de un rango
│   └── VisualizadorBarras.cs           # Gráfico de barras animado
├── Utilidades/
│   └── Validaciones.cs                 # Lectura y validación de datos del usuario
└── AlgoritmosOrdenamiento_Ruben_Ibañez.csproj
```

## Algoritmos implementados

| Algoritmo | Idea principal |
|---|---|
| Bubble Sort | Intercambia pares adyacentes desordenados | 
| Insertion Sort | Inserta cada elemento en la parte ya ordenada |
| Merge Sort | Divide, ordena y mezcla mitades |
| Quick Sort | Particiona la lista alrededor de un pivote |

## Autor

**Ruben Ibañez**
Carné: 2627280

## Repositorio

Enlace de GitHub: `github.com/RIbanezD/Sorting-Algos`
