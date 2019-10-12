# Troubleshooting SkiaSharp and OpenGL

I have experienced great difficulty developing user controls which reliably use Skia and OpenGL to reliably. A frequency problem is Visual Studio Designer crashes. These notes (and example code) demonstrate how to make everything work together.

### Never create a user control targeting x64
Visual Studio is only 32-bit. If you create a user control project, it must target `x86` or `any CPU`. If you target `x64` you will get an error when you try to add the user control: _Failed to load toolbox item. It will be removed from the toolbox._ Knowing this, the following chart lists the ideal target for a multi-package system:

project | platform | build target
---|---|---
QuickPlot | .NET Standard | `Any CPU`
QuickPlot.WinForms | .NET Framework | `Any CPU`
QuickPlotDemo | .NET Framework | `x86` or `x64` (not `Any CPU`)

### Upgrade (or downgrade) Visual Studio

I found that the Skia and OpenGL user controls crash specific versions of Visual Studio. If you get unexpected Visual Studio crashes using the latest version of Visual Studio, consider _downgrading_ your Visual Studio installation.