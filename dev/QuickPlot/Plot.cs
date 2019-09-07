using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot
{
    public class Plot
    {
        public Settings.AxisLabels labels;
        public Settings.Axes axes;

        public Plot()
        {
            axes = new Settings.Axes();
            labels = new Settings.AxisLabels();
        }

        public Bitmap Render(Bitmap bmp, Graphics gfx, Rectangle rect)
        {
            Settings.PlotLayout layout = LayOut(bmp, gfx, rect);
            layout.LabelRegions();

            return bmp;
        }

        private Settings.PlotLayout LayOut(Bitmap bmp, Graphics gfx, Rectangle rect)
        {
            // create a layout for this plot
            Settings.PlotLayout layout = new Settings.PlotLayout(bmp, gfx, rect);

            // adjust the layout components based on:
            //  - whether they are null
            //  - how large things are

            if (labels.title.IsValid)
            {
                int titleHeight = 20;
                layout.title.Match(layout.plotArea);
                layout.title.ShrinkTo(top: titleHeight);
            }

            if (labels.x.IsValid)
            {
                int labelBottomHeight = 20;
                layout.labelX.Match(layout.plotArea);
                layout.labelX.ShrinkTo(bottom: labelBottomHeight);
            }

            if (labels.y.IsValid)
            {
                int labelLeftWidth = 20;
                layout.labelY.Match(layout.plotArea);
                layout.labelY.ShrinkTo(left: labelLeftWidth);
            }

            if (labels.y2.IsValid)
            {
                int labelRightWidth = 20;
                layout.labelY2.Match(layout.plotArea);
                layout.labelY2.ShrinkTo(right: labelRightWidth);
            }

            if (axes.enableX)
            {
                int scaleSizeB = 20;
                layout.scaleX.Match(layout.plotArea);
                layout.scaleX.ShrinkTo(bottom: scaleSizeB);
                layout.scaleX.Shift(up: layout.labelX.Height);
            }

            if (axes.enableY)
            {
                int scaleSizeL = 50;
                layout.scaleY.Match(layout.plotArea);
                layout.scaleY.ShrinkTo(left: scaleSizeL);
                layout.scaleY.Shift(right: layout.labelY.Width);
            }

            if (axes.enableY2)
            {
                int scaleSizeR = 50;
                layout.scaleY2.Match(layout.plotArea);
                layout.scaleY2.ShrinkTo(right: scaleSizeR);
                layout.scaleY2.Shift(left: layout.labelY2.Width);
            }

            layout.data.Match(layout.plotArea);
            layout.data.ShrinkBy(
                left: layout.labelY.Width + layout.scaleY.Width,
                right: layout.labelY2.Width + layout.scaleY2.Width,
                bottom: layout.labelX.Height + layout.scaleX.Height,
                top: layout.title.Height);

            layout.ShrinkToRemoveOverlaps();

            return layout;
        }
    }
}
