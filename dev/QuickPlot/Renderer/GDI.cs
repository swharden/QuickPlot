using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Renderer
{
    /* This class handles the entire Render() drawing.
     * If you want to make a non-GDI version, replace this class with a similar one and call it to render.
     * Don't just copy these functions. Make your own Render() sequence ideally suited for your rendering enegine.
     */

    public static class GDI
    {
        public static Bitmap Render(Bitmap bmp, Graphics gfx, Rectangle rect, Plot plt)
        {
            // determine the layout (position of title, scales, ticks, etc)
            Settings.PlotLayout layout = LayOut(gfx, rect, plt);

            // generate the ticks now that the layout is known
            plt.axes.axisX.GenerateTicks(layout.data.rect.Width);
            plt.axes.axisY.GenerateTicks(layout.data.rect.Height);
            plt.axes.axisY2.GenerateTicks(layout.data.rect.Height);

            // render everything in one pass
            if (plt.advancedSettings.showLayout)
                OutlineLayoutRegions(gfx, layout);
            RenderFrame(layout, gfx, plt);
            RenderLabels(layout, gfx, plt);
            RenderScales(layout, gfx, plt);
            RenderPlottables(layout, gfx, plt);
            return bmp;
        }


        #region basic drawing tasks

        public static void Fill(Graphics gfx, Region region, Color color, int alpha = 255)
        {
            color = Color.FromArgb(alpha, color);
            Brush brush = new SolidBrush(color);
            gfx.FillRectangle(brush, region.rect);
        }

        public static void Outline(Graphics gfx, Region region, Color color, int alpha = 255)
        {
            color = Color.FromArgb(alpha, color);
            Pen pen = new Pen(color);
            gfx.DrawRectangle(pen, region.rect.X, region.rect.Y, region.rect.Width - 1, region.rect.Height - 1);
        }

        public static SizeF MeasureString(Graphics gfx, Settings.AxisLabel label)
        {
            return gfx.MeasureString(label.text, label.fs.Font);
        }

        public enum AlignHoriz { left, center, right };
        public enum AlignVert { top, center, bottom };

        public static StringFormat StringFormat(AlignHoriz h = AlignHoriz.center, AlignVert v = AlignVert.center)
        {
            StringFormat sf = new StringFormat();

            switch (h)
            {
                case AlignHoriz.left:
                    sf.Alignment = StringAlignment.Near;
                    break;
                case AlignHoriz.right:
                    sf.Alignment = StringAlignment.Far;
                    break;
                case AlignHoriz.center:
                    sf.Alignment = StringAlignment.Center;
                    break;
            }

            switch (v)
            {
                case AlignVert.top:
                    sf.LineAlignment = StringAlignment.Near;
                    break;
                case AlignVert.bottom:
                    sf.LineAlignment = StringAlignment.Far;
                    break;
                case AlignVert.center:
                    sf.LineAlignment = StringAlignment.Center;
                    break;
            }

            return sf;
        }

        public static Color RandomColor
        {
            get
            {
                Random rand = new Random();
                Color color = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                return color;
            }
        }

        private static void OutlineAndLabel(Graphics gfx, Region region, string label, Color color, int alpha = 255, bool outlineToo = true, bool fillToo = true)
        {
            if (!region.IsValid)
                return;

            color = Color.FromArgb(alpha, color);
            Brush brush = new SolidBrush(color);
            Font fnt = new Font(FontFamily.GenericMonospace, 8);

            if (region.rect.Width >= region.rect.Height)
            {
                gfx.DrawString(label, fnt, brush, region.rect.X, region.rect.Y);
            }
            else
            {
                var sf = StringFormat(h: AlignHoriz.left, v: AlignVert.bottom);
                gfx.RotateTransform(90);
                gfx.DrawString(label, fnt, brush, region.rect.Y, -region.rect.X, sf);
                gfx.ResetTransform();
            }

            if (fillToo)
                Fill(gfx, region, color, alpha / 10);
            if (outlineToo)
                Outline(gfx, region, color, alpha);
        }

        #endregion

        public static void OutlineLayoutRegions(Graphics gfx, Settings.PlotLayout layout)
        {
            int transparency = 200;

            OutlineAndLabel(gfx, layout.plotArea, "", Color.LightGray, transparency);

            OutlineAndLabel(gfx, layout.title, "", Color.Gray, transparency);
            OutlineAndLabel(gfx, layout.labelX, "", Color.Gray, transparency);
            OutlineAndLabel(gfx, layout.labelY, "", Color.Gray, transparency);
            OutlineAndLabel(gfx, layout.labelY2, "", Color.Gray, transparency);

            OutlineAndLabel(gfx, layout.scaleX, "", Color.Green, transparency);
            OutlineAndLabel(gfx, layout.scaleY, "", Color.Green, transparency);
            OutlineAndLabel(gfx, layout.scaleY2, "", Color.Green, transparency);

            OutlineAndLabel(gfx, layout.data, "", Color.Magenta, transparency);
        }

        private static Settings.PlotLayout LayOut(Graphics gfx, Rectangle rect, Plot plt)
        {
            // create a layout for this plot
            Settings.PlotLayout layout = new Settings.PlotLayout(rect);

            // adjust the layout components based on:
            //  - whether they are null
            //  - how large things are

            if (plt.axes.labelTitle.IsValid)
            {
                SizeF labelSize = MeasureString(gfx, plt.axes.labelTitle);
                layout.title.Match(layout.plotArea);
                layout.title.ShrinkTo(top: (int)labelSize.Height);
            }

            if (plt.axes.labelX.IsValid)
            {
                SizeF labelSize = MeasureString(gfx, plt.axes.labelX);
                layout.labelX.Match(layout.plotArea);
                layout.labelX.ShrinkTo(bottom: (int)labelSize.Height);
            }

            if (plt.axes.labelY.IsValid)
            {
                SizeF labelSize = MeasureString(gfx, plt.axes.labelY);
                layout.labelY.Match(layout.plotArea);
                layout.labelY.ShrinkTo(left: (int)labelSize.Height);
            }

            if (plt.axes.labelY2.IsValid)
            {
                SizeF labelSize = MeasureString(gfx, plt.axes.labelY2);
                layout.labelY2.Match(layout.plotArea);
                layout.labelY2.ShrinkTo(right: (int)labelSize.Height);
            }

            if (plt.axes.enableX)
            {
                int scaleFontHeight = 20;
                layout.scaleX.Match(layout.plotArea);
                layout.scaleX.ShrinkTo(bottom: scaleFontHeight);
                layout.scaleX.Shift(up: layout.labelX.Height);
            }

            if (plt.axes.enableY)
            {
                int widestScaleTick = 35;
                layout.scaleY.Match(layout.plotArea);
                layout.scaleY.ShrinkTo(left: widestScaleTick);
                layout.scaleY.Shift(right: layout.labelY.Width);
            }

            if (plt.axes.enableY2)
            {
                int widestScaleTick = 35;
                layout.scaleY2.Match(layout.plotArea);
                layout.scaleY2.ShrinkTo(right: widestScaleTick);
                layout.scaleY2.Shift(left: layout.labelY2.Width);
            }

            layout.data.Match(layout.plotArea);
            layout.data.ShrinkBy(
                left: layout.labelY.Width + layout.scaleY.Width,
                right: layout.labelY2.Width + layout.scaleY2.Width,
                bottom: layout.labelX.Height + layout.scaleX.Height,
                top: layout.title.Height);
            layout.scaleY.Shift(left: 1);

            layout.ShrinkToRemoveOverlaps();

            return layout;
        }

        private static void RenderLabels(Settings.PlotLayout layout, Graphics gfx, Plot plt)
        {
            StringFormat sfCentCent = StringFormat(AlignHoriz.center, AlignVert.center);
            gfx.DrawString(plt.axes.labelTitle.text, plt.axes.labelTitle.fs.Font, plt.axes.labelTitle.fs.Brush, layout.title.Center, sfCentCent);
            gfx.DrawString(plt.axes.labelX.text, plt.axes.labelX.fs.Font, plt.axes.labelX.fs.Brush, layout.labelX.Center, sfCentCent);

            gfx.RotateTransform(-90);
            gfx.DrawString(plt.axes.labelY.text, plt.axes.labelY.fs.Font, plt.axes.labelY.fs.Brush, layout.labelY.CenterRotNeg90, sfCentCent);
            gfx.ResetTransform();

            gfx.RotateTransform(90);
            gfx.DrawString(plt.axes.labelY2.text, plt.axes.labelY2.fs.Font, plt.axes.labelY2.fs.Brush, layout.labelY2.CenterRotPos90, sfCentCent);
            gfx.ResetTransform();
        }

        private static void RenderFrame(Settings.PlotLayout layout, Graphics gfx, Plot plt)
        {
            gfx.DrawRectangle(Pens.LightGray, layout.data.rect.Left, layout.data.rect.Top - 1, layout.data.rect.Width, layout.data.rect.Height + 1);
        }

        private static void RenderScales(Settings.PlotLayout layout, Graphics gfx, Plot plt)
        {
            int majorTickLength = 2;

            if (layout.scaleX.IsValid)
            {
                Font fnt = plt.axes.axisX.fs.Font;
                Brush brsh = plt.axes.axisX.fs.Brush;
                Pen pen = plt.axes.axisX.fs.Pen;
                StringFormat sf = StringFormat(AlignHoriz.center, AlignVert.top);

                gfx.DrawLine(plt.axes.axisX.fs.Pen, layout.scaleX.TopLeft, layout.scaleX.TopRight);
                foreach (Settings.Tick tick in plt.axes.axisX.ticks.tickList)
                {
                    int x = layout.scaleX.rect.Left + tick.pixelsFromEdge;
                    Point pt1 = new Point(x, layout.scaleX.rect.Top);
                    Point pt2 = new Point(x, layout.scaleX.rect.Top + majorTickLength);
                    gfx.DrawLine(pen, pt1, pt2);
                    gfx.DrawString(tick.label, fnt, brsh, pt1, sf);
                }
            }

            if (layout.scaleY.IsValid)
            {
                Font fnt = plt.axes.axisY.fs.Font;
                Brush brsh = plt.axes.axisY.fs.Brush;
                Pen pen = plt.axes.axisY.fs.Pen;
                StringFormat sf = StringFormat(AlignHoriz.right, AlignVert.center);

                gfx.DrawLine(plt.axes.axisY.fs.Pen, layout.scaleY.TopRight, layout.scaleY.BottomRight);

                foreach (Settings.Tick tick in plt.axes.axisY.ticks.tickList)
                {
                    int y = layout.scaleY.rect.Bottom - tick.pixelsFromEdge;
                    Point pt1 = new Point(layout.scaleY.rect.Right, y);
                    Point pt2 = new Point(layout.scaleY.rect.Right - majorTickLength, y);
                    gfx.DrawLine(pen, pt1, pt2);
                    gfx.DrawString(tick.label, fnt, brsh, pt2, sf);
                }
            }

            if (layout.scaleY2.IsValid)
            {
                Font fnt = plt.axes.axisY2.fs.Font;
                Brush brsh = plt.axes.axisY2.fs.Brush;
                Pen pen = plt.axes.axisY2.fs.Pen;
                StringFormat sf = StringFormat(AlignHoriz.left, AlignVert.center);

                gfx.DrawLine(plt.axes.axisY2.fs.Pen, layout.scaleY2.TopLeft, layout.scaleY2.BottomLeft);

                foreach (Settings.Tick tick in plt.axes.axisY.ticks.tickList)
                {
                    int y = layout.scaleY2.rect.Bottom - tick.pixelsFromEdge;
                    Point pt1 = new Point(layout.scaleY2.rect.Left, y);
                    Point pt2 = new Point(layout.scaleY2.rect.Left + majorTickLength, y);
                    gfx.DrawLine(pen, pt1, pt2);
                    gfx.DrawString(tick.label, fnt, brsh, pt2, sf);
                }
            }
        }

        private static void RenderPlottables(Settings.PlotLayout layout, Graphics gfx, Plot plt)
        {
            if (layout.data.Width < 1 || layout.data.Height < 1)
                return;

            // Create a new bitmap just for the data area. 
            // If you render outside bitmap area, that's okay.
            // At the end this bitmap is copied onto the figure bitmap.
            // This method is a little slower, but it's much simpler code.

            Bitmap bmpData = new Bitmap(layout.data.Width, layout.data.Height);
            Graphics gfxData = Graphics.FromImage(bmpData);

            foreach (Plottables.Plottable plottable in plt.plottables)
            {
                if (plottable is Plottables.Scatter)
                {
                    Plottables.Scatter scatter = (Plottables.Scatter)plottable;
                    gfxData.SmoothingMode = scatter.ls.antiAlias ? System.Drawing.Drawing2D.SmoothingMode.AntiAlias : System.Drawing.Drawing2D.SmoothingMode.None;
                    PointF[] points = scatter.GetPoints(plt.axes, bmpData.Size);
                    gfxData.DrawLines(scatter.ls.pen, points);
                    foreach (PointF point in points)
                        gfxData.FillEllipse(
                            scatter.ms.brush,
                            point.X - scatter.ms.size / 2,
                            point.Y - scatter.ms.size / 2, 
                            scatter.ms.size,
                            scatter.ms.size
                        );
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            gfxData.Dispose();
            gfx.DrawImage(bmpData, layout.data.Point);
            return;
        }
    }
}
