> **WARNING:** QuickPlot is early in development and not yet intended to be used. **[ScottPlot](https://github.com/swharden/ScottPlot)** is a mature .NET library which can serve many plotting needs while QuickPlot is being developed.

> **UPDATE:** QuickPlot uses the SkiaSharp library which is not trivially to get up and running ([issue #1](https://github.com/swharden/QuickPlot/issues/1)). This is especially true when using OpenGL. A core design goal of QuickPlot is minimal complexity, and the dependencies it requires make this not possible. If SkiaSharp (and it's OpenGL control) matures into something easily usable across multiple platforms, this project may be revisited. Until then, I'm stopping work on it. --Scott, October 2019

---

# QuickPlot
[![Build Status](https://dev.azure.com/swharden/swharden/_apis/build/status/swharden.QuickPlot?branchName=master)](https://dev.azure.com/swharden/swharden/_build/latest?definitionId=1&branchName=master)

**QuickPlot is a simple and fast plotting library for .NET applications.** QuickPlot makes it easy to interactively display high density data (tens of millions of points). QuickPlot is written in .NET Standard so it can be used in both .NET Framework and .NET Core projects.

QuickPlot draws images with [Skia](https://skia.org/) (not GDI+) and it can use hardware acceleration (OpenGL) on systems that support it. QuickPlot can be used in Console Applications (saving plotted data as image files) or interactively in WinForms or WPF Applications using the InteractivePlot user control.

![](dev/quickplot-screenshot.png)

### Quickstart

Let's practice plotting the following data:

```cs
double[] xs = {1, 2, 3, 4, 5};
double[] ys = {1, 4, 9, 16, 25};
```

Create a Figure with a single Plot:

```cs
var fig = new QuickPlot.Figure();
fig.plot.Scatter(xs, ys);
fig.Save("demo.jpg");
```

Create a Figure with two sub-plots:

```cs
var fig = new QuickPlot.Figure();
fig.Subplot(2, 1, 1); // Layout with 2 rows and 1 column, activate plot 1
fig.plot.Scatter(xs, ys);
fig.Subplot(2, 1, 2); // Layout with 2 rows and 1 column, activate plot 2
fig.plot.Scatter(xs, ys);
fig.Save("demo.jpg");
```
