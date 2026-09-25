# AlgoritmosOrdenamiento_Ruben_Ibañez

Aplicación de consola en C# que implementa manualmente los algoritmos **Bubble Sort**, **Insertion Sort**, **Merge Sort** y **Quick Sort**, con un menú interactivo, generación de datos de prueba aleatorios y visualización animada de cada algoritmo mediante gráficos de barras verticales.

Laboratorio 5 — Procesos y Algoritmos II — Universidad Regional de Guatemala — Segundo Semestre 2026.

## Contenido

- [Cómo ejecutar](#cómo-ejecutar)
- [Estructura del proyecto](#estructura-del-proyecto)
- [Algoritmos implementados](#algoritmos-implementados)

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
