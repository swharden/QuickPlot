using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Settings
{
    public struct Tick
    {
        public double position;
        public string label;
        public int pixelsFromEdge;
    }

    public class Ticks
    {
        public List<Tick> tickList;
        public int Count { get { return tickList.Count; } }

        double low, high;
        int pixelSpan;

        public Ticks(double low, double high, int pixelSpan)
        {
            this.low = low;
            this.high = high;
            this.pixelSpan = pixelSpan;

            tickList = new List<Tick>();
            Generate();
        }

        public void Generate()
        {
            tickList.Clear();
            double step = 2;
            double firstTick = low + Math.Abs(low % step);
            double stepRatio = step / (high - low);

            double pixelsPerUnit = pixelSpan / (high - low);

            for (double i = firstTick; i < high; i += step)
            {
                double unitsFromEdge = i - low;

                Tick tk = new Tick
                {
                    position = i,
                    label = i.ToString(),
                    pixelsFromEdge = (int)(pixelsPerUnit * unitsFromEdge)
                };
                tickList.Add(tk);
            }
        }
    }
}
