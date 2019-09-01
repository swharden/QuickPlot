# QuickPlot
**QuickPlot is a plotting library for .NET** written in C# for .NET Core. QuickPlot is very simple, fast, and easy to use. It can be installed with [NuGet](https://www.nuget.org/packages/QuickPlot).

**WARNING:** QuickPlot is early in development and not yet intended to be used. [ScottPlot](https://github.com/swharden/ScottPlot) is a mature library which can serve many plotting needs while QuickPlot is being developed.

## Features

### New Features (new compared to ScottPlot v3)
* multiple figures per image
* improved rendering system
  * plot components are all separate bitmaps - this allows the user to get the bitmap corresponding to a legend, or an axis edge (with ticks and tick labels)
  * bitmap generation is abstracted (allowing switch between different graphics systems, such as GDI and OpenGL)
* intelligent tick labels (with DateTime support)
* tools for plotting logarithmic data
* enhanced mouse interactivity (e.g., "show value on hover")
* tools for managing incoming data streams (e.g., serial plotter)
* multiple Y axes (e.g., a second vertical axis on the right side)

### Standard Features (which ScottPlot v3 has)
* highspeed rendering (tens of millions of points)
* GUI-optional: can create plots from console applications

### Design Goals & Philosophy 
* no dependencies (just .NET core libraries)
* MIT licensing (extremely permissive)

## Tasks
These are listed in order

* create skeleton for the rendering process
* create a single plottable object (scatter plot)
* add mouse interactivity
* create minimal user control (drag and zoom only)
* abstract graphics engine (support GDI and OpenGL)

## Code Concepts
A big change from ScottPlot v3 is that now a _Figure_ may contain several _Plots_. Each _Plot_ may contain several _Plottable_ objects.

These examples plot the following data:

```cs
double[] xs = {1, 2, 3, 4, 5};
double[] ys = {1, 4, 9, 16, 25};
```

Create a Figure with a single Plot:

```cs
var fig = QuickPlot.Figure(width: 600, height: 400);
fig.plot.Scatter(xs, ys);
fig.Save("demo.jpg");
```

Create a Figure with two sub-plots:

```cs
var fig = QuickPlot.Figure(width: 600, height: 400, subPlots: 2);
fig.plots[0].Scatter(xs, ys);
fig.plots[1].Scatter(xs, ys);
fig.Layout(rows: 2, cols: 1);
fig.Save("demo.jpg");
```