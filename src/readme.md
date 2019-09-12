### 2019-09-11: I started over from scratch again
* QuickPlot 
  * a class library (.NET standard 1.3)
    * can be referenced by .NET Framework  4.6
	* can be referenced by .NET Core 1.0
  * no user controls
  * a console application cookbook functions as a test suite
* QuickPlot.WinForms
  * contains user control for interactive plot
  * user control is smart enough to use OpenGL if it's available
  * the demo application serves as a test suite

TODO: step versions back and see if it still works

Build target: omg what a nightmare!!!!!!! This only works if:
  * QuickPlot targets "x64"
  * QuickPlot.Forms targets "Any CPU"
  * Programs that use the user control target "x64"

### 2019-09-10: I started over from scratch
* Build target notes
  * Only `QuickPlot` has Skia, yet it can be built for `any cpu`
  * The programs which use QuickPlot must build for a single CPU target

### Notes

QuickPlot GDI source code:
* https://github.com/swharden/QuickPlot/tree/master/dev/QuickPlot

Skia example
* https://github.com/swharden/Csharp-Data-Visualization/tree/master/examples/2019-09-08-SkiaSharp-openGL

StendProg's user control for Skia:
https://github.com/swharden/ScottPlot/blob/631d6794973bc635c05ed01ff9431d4ac108eac5/dev/ScottPlotSkia/ScottPlotSkia/FormsPlotSkia.cs

StendProg's Skia backend implimentation:
https://github.com/swharden/ScottPlot/blob/631d6794973bc635c05ed01ff9431d4ac108eac5/dev/ScottPlotSkia/ScottPlotSkia/SkiaBackend.cs
https://github.com/swharden/ScottPlot/blob/631d6794973bc635c05ed01ff9431d4ac108eac5/dev/ScottPlotSkia/ScottPlotSkia/SkaiExtensions.cs
