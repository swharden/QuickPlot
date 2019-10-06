using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace QuickPlot
{
    // this is a DTO which is easier to pass than tons of arguments
    public class Style
    {
        public Color lineColor = Color.Black;
        public float lineWidth = 1;

        public Color markerColor = Color.Black;
        public float markerSize = 3;

        public Style(int index)
        {
            lineColor = Tools.IndexedColor10(index);
            markerColor = Tools.IndexedColor10(index);
        }
    }
}
