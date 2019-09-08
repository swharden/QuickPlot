# QuickPlot
**QuickPlot is a plotting library for .NET** written in C# for .NET Core. QuickPlot is very simple, fast, and easy to use. It can be installed with [NuGet](https://www.nuget.org/packages/QuickPlot).

**WARNING:** QuickPlot is early in development and not yet intended to be used. [ScottPlot](https://github.com/swharden/ScottPlot) is a mature library which can serve many plotting needs while QuickPlot is being developed.

![](dev/quickplot-screenshot.png)

## Features

### New Features (new compared to ScottPlot v3)
* multiple figures can contain several subplots
* intelligent layout engine
* intelligent tick labels (with DateTime support)
* enhanced mouse support makes it easier to "show value on hover"
* multiple Y axes are supported
* highspeed rendering (tens of millions of points)
* GUI is optional (plots can be created from console applications)
* no dependencies (just .NET core libraries)
* MIT licensing (extremely permissive)

## Example Code

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
fig.Subplot(2, 1, 1); // Layout with 2 rows and 1 column, activate plot 1
fig.plot.Scatter(xs, ys);
fig.Subplot(2, 1, 2); // Layout with 2 rows and 1 column, activate plot 2
fig.plot.Scatter(xs, ys);
fig.Save("demo.jpg");
```