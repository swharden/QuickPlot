using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Plottables
{
    public class LineStyle
    {
        public float width;
        public Color color;
        public bool antiAlias;
        public Pen pen { get { return new Pen(color); } }

        public LineStyle(float width = 1, Color? color = null, bool antiAlias = true)
        {
            this.width = width;
            this.color = color ?? Color.Magenta;
            this.antiAlias = antiAlias;
        }
    }

    public enum MarkerShape { circle, square };

    public class MarkerStyle
    {
        public float size;
        public Color color;
        public bool antiAlias;
        public MarkerShape markerShape;
        public Pen pen { get { return new Pen(color); } }
        public Brush brush { get { return new SolidBrush(color); } }

        public MarkerStyle(float size = 5, Color? color = null, bool antiAlias = true, MarkerShape markerShape = MarkerShape.circle)
        {
            this.size = size;
            this.color = color ?? Color.Magenta;
            this.antiAlias = antiAlias;
            this.markerShape = markerShape;
        }
    }

    public abstract class Plottable
    {
        public LineStyle ls;
        public MarkerStyle ms;
        public string label;
    }
}
