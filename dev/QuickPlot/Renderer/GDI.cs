using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Renderer
{
    public static class GDI
    {
        public static Bitmap Render(Bitmap bmp, Graphics gfx, Rectangle rect, Plot plt)
        {
            Settings.PlotLayout layout = LayOut(gfx, rect, plt);
            if (plt.advancedSettings.showLayout)
                OutlineLayoutRegions(gfx, layout);
            RenderLabels(layout, gfx, plt);
            RenderScales(layout, gfx, plt);
            return bmp;
        }

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

        public static SizeF MeasureString(Graphics gfx, Settings.AxisLabel label)
        {
            return gfx.MeasureString(label.text, label.fs.Font);
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

            layout.ShrinkToRemoveOverlaps();

            return layout;
        }

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

        public enum AlignHoriz { left, center, right };
        public enum AlignVert { top, center, bottom };

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

        private static void RenderScales(Settings.PlotLayout layout, Graphics gfx, Plot plt)
        {
            if (layout.scaleX.IsValid)
            {
                gfx.DrawLine(plt.axes.axisX.fs.Pen, layout.scaleX.TopLeft, layout.scaleX.TopRight);
            }

            if (layout.scaleY.IsValid)
            {
                gfx.DrawLine(plt.axes.axisY.fs.Pen, layout.scaleY.TopRight, layout.scaleY.BottomRight);
            }

            if (layout.scaleY2.IsValid)
            {
                gfx.DrawLine(plt.axes.axisY2.fs.Pen, layout.scaleY2.TopLeft, layout.scaleY2.BottomLeft);
            }
        }
    }

}
