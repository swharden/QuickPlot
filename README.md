> **WARNING:** QuickPlot is early in development and not yet intended to be used. **[ScottPlot](https://github.com/swharden/ScottPlot)** is a mature and stable .NET library which can serve many plotting needs while QuickPlot is being developed.

---

# QuickPlot
[![](https://img.shields.io/azure-devops/build/swharden/swharden/1?label=Build&logo=azure%20pipelines)](https://dev.azure.com/swharden/swharden/_build/latest?definitionId=1&branchName=master)
[![](https://img.shields.io/azure-devops/tests/swharden/swharden/1?label=Tests&logo=azure%20pipelines)](https://dev.azure.com/swharden/swharden/_build/latest?definitionId=1&branchName=master)

**QuickPlot is a simple plotting library for .NET** that makes it easy to interactively display high density data (tens of millions of points). QuickPlot is written in .NET Standard so it can be used in both .NET Framework and .NET Core projects. An interactive user control is available for Windows Forms and WPF Applications.

![](demos/QuickPlotDemos/screenshot.png)

### Supported Platforms

QuickPlot can be used to save plots as images from Console Applications (including ASP .NET), or interactively display plots using the InteractivePlot user control available for WinForms, WPF, and UWP.

Library | Platform | Function
---|---|---
QuickPlot | .NET Standard 2.0 | Plot data and return (or save) Bitmaps
QuickPlot.WinForms | .NET Framework 4.6.1 | Interactively display plots
QuickPlot.WPF | .NET Core 2.0 | Interactively display plots
QuickPlot.UWP | UWP 10.0.16299 | _not yet written_

## Design Goals

### Value Performance but Prioritize Simplicity
QuickPlot is a library born from the struggle between performance and simplicity. The primary goal of QuickPlot is to provide immediate usability on all .NET platforms without any special configuration. A secondary goal is to be able to render high density data (tens of millions of points) fast enough to allow real-time mouse interaction.

### Rendering Framework
QuickPlot exclusively uses System.Drawing to create figures as bitmaps. This  method is traditionally regarded as inefficient compared to modern hardware-accelerated rendering frameworks. However, more advanced rendering frameworks are often difficult to configure, offer limited cross-platform support, and/or are not officially or regularly maintained. Microsoft's [System.Drawing.Common](https://www.nuget.org/packages/System.Drawing.Common/) is a modern cross-platform implementation of System.Drawing which demonstrates moderate performance but has the great advantage of being simple to use on any .NET platform without requiring any special configuration.

### No Documentation Required
QuickPlot's API strives to be so simple that it does not require documentation. Anyone can learn to use all the primary features of QuickPlot by reviewing the QuickPlot Cookbook.

### Keep Code Light and Flexible
Effort is continuously invested to keep the QuickPlot code base small and easy to review. When complex features are considered for addition, the creation of complex _demos_ is favored over injecting complexity into the core library. 

Similarly, users seeking uncommon or advanced functionality are encouraged to fork and modify the code rather than build a complex inheritance system that requires QuickPlot's internal class structure to be rigidly maintained.

### QuickPlot vs. ScottPlot
QuickPlot was written from scratch in September, 2019 with an API designed to closely mimic [ScottPlot](https://github.com/swharden/ScottPlot). The primary improvements of QuickPlot over ScottPlot are: (1) written in .NET Standard, (2) separation of plotting library from user control libraries, (3) a more performant procedural-style rendering system, and (4) support for subplots.

## About QuickPlot
QuickPlot was created by [Scott Harden](http://www.SWHarden.com/) ([Harden Technologies, LLC](http://tech.swharden.com)) with many contributions from the user community. To inquire about the development special features or customized versions of this software for consumer applications, contact the author at [SWHarden@gmail.com](mailto:swharden@gmail.com).