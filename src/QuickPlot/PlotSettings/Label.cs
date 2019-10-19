using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot.PlotSettings
{
    public class Label
    {
        public string text = "label";
        public float fontSize = 12;
        public SKFontStyleWeight weight = SKFontStyleWeight.Normal;
        public string fontName = "Segoe UI";
        public SKColor fontColor = SKColors.Black;
        public SKTextAlign align = SKTextAlign.Center;

        public SKPaint MakePaint()
        {
            return new SKPaint()
            {
                IsAntialias = true,
                Color = fontColor,
                TextSize = fontSize,
                TextAlign = align,
                Typeface = SKTypeface.FromFamilyName(fontName, weight, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright)
            };
        }
    }
}
