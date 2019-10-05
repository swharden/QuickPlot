﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace QuickPlot
{
    public class Style
    {
        public Color lineColor;
        public float lineWidth;

        public Color markerColor;
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
            lineColor = Color.Black;
            markerColor = Color.Black;
            lineWidth = 1;
            markerSize = 3;
        }
    }
}
