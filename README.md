> **WARNING:** QuickPlot is early in development and not yet intended to be used. **[ScottPlot](https://github.com/swharden/ScottPlot)** is a mature and stable .NET library which can serve many plotting needs while QuickPlot is being developed.

---

# QuickPlot
[![Build Status](https://dev.azure.com/swharden/swharden/_apis/build/status/swharden.QuickPlot?branchName=master)](https://dev.azure.com/swharden/swharden/_build/latest?definitionId=1&branchName=master)

**QuickPlot is a simple plotting library for .NET** that makes it easy to interactively display high density data (tens of millions of points). QuickPlot is written in .NET Standard so it can be used in both .NET Framework and .NET Core projects.

### Supported Platforms

QuickPlot can be used to save plots as images from Console Applications (including ASP .NET), or interactively display plots using the InteractivePlot user control available for WinForms, WPF, and UWP.

Library | Platform | Function
---|---|---
QuickPlot | .NET Standard 2.0 | Plot data and return (or save) Bitmaps
QuickPlot.WinForms | .NET Framework 4.6.1 | Interactively display plots
QuickPlot.WPF | .NET Core 2.0 | Interactively display plots
QuickPlot.UWP | UWP 10.0.16299 | Interactively display plots

### Dependencies

QuickPlot draws images using [System.Drawing.Common](https://www.nuget.org/packages/System.Drawing.Common/), an officially-supported cross-platform graphics library available on NuGet. While more advanced hardware-accelerated graphics libraries exist, QuickPlot's use of the standard library ensures it will compile effortlessly on any .NET system without requiring any special configuration.