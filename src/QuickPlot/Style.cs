using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot
{
    public class Style
    {
        readonly SKColor color;
        readonly float lineWidth, markerSize, fontSize;
        readonly string fontName, label;
        public readonly SKPaint paint;


        public Style(int colorIndex = 0, SKColor? color = null, float lineWidth = 1, float markerSize = 3, string fontName = "Segoe UI", float fontSize = 12, string label = null)
        {
            this.color = (color is null) ? IndexedColor(colorIndex) : (SKColor)color;
            this.lineWidth = lineWidth;
            this.markerSize = markerSize;
            this.fontName = fontName;
            this.fontSize = fontSize;
            this.label = label;
            paint = new SKPaint { IsAntialias = true, Color = this.color };
        }

        public static SKColor IndexedColor(int index, bool usePalette20 = false)
        {
            // colors came from https://github.com/vega/vega/wiki/Scales#scale-range-literals

            string[] colors;
            if (usePalette20)
            {
                colors = new string[] {
                    "#1f77b4", "#aec7e8", "#ff7f0e", "#ffbb78", "#2ca02c",
                    "#98df8a", "#d62728", "#ff9896", "#9467bd", "#c5b0d5",
                    "#8c564b", "#c49c94", "#e377c2", "#f7b6d2", "#7f7f7f",
                    "#c7c7c7", "#bcbd22", "#dbdb8d", "#17becf", "#9edae5",
                };
            }
            else
            {
                colors = new string[] {
                    "#1f77b4", "#ff7f0e", "#2ca02c", "#d62728", "#9467bd",
                    "#8c564b", "#e377c2", "#7f7f7f", "#bcbd22", "#17becf"
                };
            }

            return SKColor.Parse(colors[index % colors.Length]);
        }
    }
}
