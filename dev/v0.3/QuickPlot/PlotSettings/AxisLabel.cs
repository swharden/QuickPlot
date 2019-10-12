using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot.PlotSettings
{
    class AxisLabel
    {
        public bool visible = true;
        public string text = "label";
        public float fontSize = 14;
        public string fontFamily = "Arial";
        public SKColor fontColor = SKColors.Black;
        public bool bold = false;

        public SKPaint paint
        {
            get
            {
                SKPaint paint = new SKPaint
                {
                    IsAntialias = true,
                    TextSize = fontSize,
                    Color = fontColor,
                    TextAlign = SKTextAlign.Center,
                    FakeBoldText = bold
                };
                return paint;
            }
        }

        public float width { get { return paint.MeasureText(text); } }
    }
}
