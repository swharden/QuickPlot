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
        public Settings.Padding padding;
        public Settings.Labels labels;
        public Settings.Axes axes;

        public Plot()
        {
            axes = new Settings.Axes();
        }

        public Bitmap Render(Bitmap bmp, Graphics gfx, Rectangle rect)
        {
            Layout layout = new Layout(bmp, gfx, rect);

            Region plotRegion = new Region(bmp, gfx, rect);
            plotRegion.Label("plotArea", Color.Gray, 100);
            plotRegion.ShrinkBy(20); // add a little padding to be pretty

            // determine size of tick labels
            int scaleSizeL = 50;
            int scaleSizeR = 50;
            int scaleSizeB = 20;
            int labelBottomHeight = 20;
            int labelLeftWidth = 20;
            int labelRightWidth = 20;
            int titleHeight = 20;

            // lay-out all the components and dont worry about overlaps

            if (labels.top != null)
            {
                layout.title.Match(plotRegion);
                layout.title.ShrinkTo(top: titleHeight);
                plotRegion.ShrinkBy(top: titleHeight);
            }

            if (labels.bottom != null)
            {
                layout.labelX.Match(plotRegion);
                layout.labelX.ShrinkTo(bottom: labelBottomHeight);
                plotRegion.ShrinkBy(bottom: labelBottomHeight);
            }

            if (labels.left != null)
            {
                layout.labelY.Match(plotRegion);
                layout.labelY.ShrinkTo(left: labelLeftWidth);
                plotRegion.ShrinkBy(left: labelLeftWidth);
            }

            if (labels.right != null)
            {
                layout.labelY2.Match(plotRegion);
                layout.labelY2.ShrinkTo(right: labelRightWidth);
                plotRegion.ShrinkBy(right: labelRightWidth);
            }

            if (axes.enableX)
            {
                layout.scaleX.Match(plotRegion);
                layout.scaleX.ShrinkTo(bottom: scaleSizeB);
                plotRegion.ShrinkBy(bottom: scaleSizeB);
            }

            if (axes.enableY)
            {
                layout.scaleY.Match(plotRegion);
                layout.scaleY.ShrinkTo(left: scaleSizeL);
                plotRegion.ShrinkBy(left: scaleSizeL);
            }

            if (axes.enableY2)
            {
                layout.scaleY2.Match(plotRegion);
                layout.scaleY2.ShrinkTo(right: scaleSizeR);
                plotRegion.ShrinkBy(right: scaleSizeR);
            }

            layout.data.Match(plotRegion);

            // fine-tune regions after the layout is established
            layout.scaleX.MatchX(layout.data);
            layout.labelY.MatchY(layout.data);
            layout.labelY2.MatchY(layout.data);
            layout.labelX.MatchX(layout.data);

            // draw all the regions
            layout.LabelRegions();


            return bmp;
        }

        private class Layout
        {
            public readonly Region plotArea;
            public readonly Region title, labelX, labelY, labelY2;
            public readonly Region scaleX, scaleY, scaleY2;
            public readonly Region data;

            public Layout(Bitmap bmp, Graphics gfx, Rectangle plotAreaRect)
            {
                plotArea = new Region(bmp, gfx, plotAreaRect);

                Rectangle zero = new Rectangle(0, 0, 0, 0);

                title = new Region(bmp, gfx, zero);
                labelX = new Region(bmp, gfx, zero);
                labelY = new Region(bmp, gfx, zero);
                labelY2 = new Region(bmp, gfx, zero);

                scaleX = new Region(bmp, gfx, zero);
                scaleY = new Region(bmp, gfx, zero);
                scaleY2 = new Region(bmp, gfx, zero);

                data = new Region(bmp, gfx, zero);
            }

            public void LabelRegions()
            {
                int transparency = 200;

                plotArea.Label("plotArea", Color.Gray, transparency);

                title.Label("title", Color.Gray, transparency);
                labelX.Label("labelX", Color.Gray, transparency);
                labelY.Label("labelY", Color.Gray, transparency);
                labelY2.Label("labelY2", Color.Gray, transparency);

                scaleX.Label("scaleX", Color.Green, transparency);
                scaleY.Label("scaleY", Color.Green, transparency);
                scaleY2.Label("scaleY2", Color.Green, transparency);

                data.Label("data", Color.Magenta, transparency);
            }
        }
    }
}
