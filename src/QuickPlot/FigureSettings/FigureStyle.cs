using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot.FigureSettings
{
    public class FigureStyle
    {
        public float edges = 10; // pixels between plots and the edge of the figure
        public float horizontal = 5; // pixels between subplots
        public float vertical = 5; // pixels between subplots
        public SKColor bgColor = SKColor.Parse("#FFFFFF");
    }
}
