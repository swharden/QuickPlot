using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace QuickPlot.PlotSettings
{
    public class Label
    {
        //public float minimumWidth = 20;
        //public float minimumHeight = 20;
        public string text = "label";
        public float fontSize = 12;
        public bool bold = false;
        public string fontName = "Segoe UI";
        public Color fontColor = Color.Black;

        public Font font
        {
            get
            {
                FontStyle fs = (bold) ? FontStyle.Bold : FontStyle.Regular;
                Font fnt = new Font(fontName, fontSize, fs, GraphicsUnit.Pixel);
                if (fnt.Name == fontName)
                    return fnt;
                else
                    return new Font(FontFamily.GenericSerif, fontSize, fs, GraphicsUnit.Pixel);
            }
        }

        public Brush brush
        {
            get
            {
                return new SolidBrush(fontColor);
            }
        }
    }
}
