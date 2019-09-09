> **WARNING:** QuickPlot is early in development and not yet intended to be used. **[ScottPlot](https://github.com/swharden/ScottPlot)** is a mature .NET library which can serve many plotting needs while QuickPlot is being developed.

---

# QuickPlot
**QuickPlot is a simple and fast plotting library for .NET applications.** QuickPlot makes it easy to interactively display high density data (tens of millions of points). QuickPlot draws images with Skia and benefits from OpenGL hardware acceleration, so it is extremely fast even at high resolutions. QuickPlot can be used in WinForms, WPF, or Console applications.

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