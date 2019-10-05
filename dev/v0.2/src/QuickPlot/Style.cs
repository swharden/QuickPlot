using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot
{
    public class Style
    {
        public SKColor lineColor;
        public float lineWidth;

        public SKColor markerColor;
        public float markerSize;

        public Style()
        {
            SetDefaults();
        }

        public Style(int index)
        {
            SetDefaults();
            lineColor = Tools.IndexedColor10(index);
            markerColor = Tools.IndexedColor10(index);
        }

        private void SetDefaults()
        {
            lineColor = SKColors.Black;
            markerColor = SKColors.Black;
            lineWidth = 1;
            markerSize = 3;
        }
    }
}
