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
        double span { get { return high - low; } }
        int pixelSpan;

        public Ticks(double low, double high, int pixelSpan, double pixelsPerTick = 50)
        {
            this.low = low;
            this.high = high;
            this.pixelSpan = pixelSpan;

            tickList = new List<Tick>();

            double idealStep = DetermineGoodStep(pixelsPerTick);
            Generate(idealStep);
        }

        public double DetermineGoodStep(double pixlesPerTick)
        {
            double idealTickCount = (double)pixelSpan / pixlesPerTick;
            double step = Math.Pow(10, (int)Math.Log(span));
            double[] divisions = { 2, 2, 2.5};
            double idealStep = span / idealTickCount;
            for (int i = 0; i < 100; i++)
            {
                if (step > idealStep)
                    step /= divisions[i % divisions.Length];
                else
                    break;
            }
            return step;
        }

        public void Generate(double step)
        {
            tickList.Clear();
            double firstTick = low + Math.Abs(low % step);

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
                if (tickList.Count > 100)
                    break;
            }
        }
    }
}
