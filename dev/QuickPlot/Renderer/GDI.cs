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
            OutlineLayoutRegions(gfx, layout);
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

            OutlineAndLabel(gfx, layout.title, "title", Color.Gray, transparency);
            OutlineAndLabel(gfx, layout.labelX, "labelX", Color.Gray, transparency);
            OutlineAndLabel(gfx, layout.labelY, "labelY", Color.Gray, transparency);
            OutlineAndLabel(gfx, layout.labelY2, "labelY2", Color.Gray, transparency);

            OutlineAndLabel(gfx, layout.scaleX, "scaleX", Color.Green, transparency);
            OutlineAndLabel(gfx, layout.scaleY, "scaleY", Color.Green, transparency);
            OutlineAndLabel(gfx, layout.scaleY2, "scaleY2", Color.Green, transparency);

            OutlineAndLabel(gfx, layout.data, "data", Color.Magenta, transparency);
        }

        public static SizeF MeasureString(Graphics gfx, Settings.AxisLabel label)
        {
            return MeasureString(gfx, label.text, label.fontSize, label.fontFamily, label.bold);
        }

        public static SizeF MeasureString(Graphics gfx, string s, float fontSize, string fontFamily, bool bold)
        {
            FontStyle style = (bold) ? FontStyle.Bold : FontStyle.Regular;
            Font fnt = new Font(fontFamily, fontSize, style);
            SizeF stringSize = gfx.MeasureString(s, fnt);
            return stringSize;
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
                Size scaleSize = new Size(200, 20);
                layout.scaleX.Match(layout.plotArea);
                layout.scaleX.ShrinkTo(bottom: scaleSize.Height);
                layout.scaleX.Shift(up: layout.labelX.Height);
            }

            if (plt.axes.enableY)
            {
                Size scaleSize = new Size(200, 20);
                layout.scaleY.Match(layout.plotArea);
                layout.scaleY.ShrinkTo(left: scaleSize.Height);
                layout.scaleY.Shift(right: layout.labelY.Width);
            }

            if (plt.axes.enableY2)
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
    }
}
