using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace QuickPlot
{
    public static class Tools
    {
        public static Color IndexedColor10(int index)
        {
            string[] plottableColors10 = new string[] {
                "#1f77b4", "#ff7f0e", "#2ca02c", "#d62728", "#9467bd",
                "#8c564b", "#e377c2", "#7f7f7f", "#bcbd22", "#17becf"
            };
            return ColorTranslator.FromHtml(plottableColors10[index % plottableColors10.Length]);
        }

        public static void TestImageLines(Bitmap bmp, int count = 1_000, bool antiAlias = true, int seed = 0)
        {
            Random rand = new Random(seed);

            Graphics gfx = Graphics.FromImage(bmp);
            gfx.SmoothingMode = (antiAlias) ? System.Drawing.Drawing2D.SmoothingMode.AntiAlias : System.Drawing.Drawing2D.SmoothingMode.None;
            gfx.Clear(Color.DarkBlue);

            for (int i = 0; i < count; i++)
            {
                Point pt1 = new Point(rand.Next(bmp.Width), rand.Next(bmp.Height));
                Point pt2 = new Point(rand.Next(bmp.Width), rand.Next(bmp.Height));
                gfx.DrawLine(Pens.LightSteelBlue, pt1, pt2);
            }
        }

        public static RectangleF RectangleShrinkBy(RectangleF rect, float left = 0, float right = 0, float bottom = 0, float top = 0)
        {
            rect.X += left;
            rect.Width -= left;

            rect.Width -= right;

            rect.Height -= bottom;

            rect.Y += top;
            rect.Height -= top;

            return rect;
        }

        public static RectangleF RectangleShrinkBy(RectangleF rect, float allSides = 0)
        {
            return RectangleShrinkBy(rect, allSides, allSides, allSides, allSides);
        }

        public static string GetVersionString()
        {
            Version ver = typeof(QuickPlot.Figure).Assembly.GetName().Version;
            return ver.ToString();
        }

        public static Bitmap DesignerModeBitmap(Size size)
        {
            Bitmap bmp = new Bitmap(size.Width, size.Height);

            {
                Graphics gfx = Graphics.FromImage(bmp);
                gfx.Clear(ColorTranslator.FromHtml("#003366"));
                Brush brushLogo = new SolidBrush(ColorTranslator.FromHtml("#FFFFFF"));
                Brush brushMeasurements = new SolidBrush(ColorTranslator.FromHtml("#006699"));
                Pen pen = new Pen(ColorTranslator.FromHtml("#006699"), 3);
                pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                float arrowSize = 7;
                float padding = 3;

                // logo
                FontFamily ff = new FontFamily("Segoe UI");
                gfx.DrawString("QuickPlot", new Font(ff, 24, FontStyle.Bold), brushLogo, 10, 10);
                var titleSize = gfx.MeasureString("QuickPlot", new Font(ff, 24, FontStyle.Bold));
                gfx.DrawString($"version {GetVersionString()}", new Font(ff, 12, FontStyle.Italic), brushLogo, 12, (int)(10 + titleSize.Height * .7));

                // horizontal line
                PointF left = new PointF(padding, size.Height / 2);
                PointF leftA = new PointF(left.X + arrowSize, left.Y + arrowSize);
                PointF leftB = new PointF(left.X + arrowSize, left.Y - arrowSize);
                PointF right = new PointF(size.Width - padding, size.Height / 2);
                PointF rightA = new PointF(right.X - arrowSize, right.Y + arrowSize);
                PointF rightB = new PointF(right.X - arrowSize, right.Y - arrowSize);
                gfx.DrawLine(pen, left, right);
                gfx.DrawLine(pen, left, leftA);
                gfx.DrawLine(pen, left, leftB);
                gfx.DrawLine(pen, right, rightA);
                gfx.DrawLine(pen, right, rightB);

                // vertical line
                PointF top = new PointF(size.Width / 2, padding);
                PointF topA = new PointF(top.X - arrowSize, top.Y + arrowSize);
                PointF topB = new PointF(top.X + arrowSize, top.Y + arrowSize);
                PointF bot = new PointF(size.Width / 2, size.Height - padding);
                PointF botA = new PointF(bot.X - arrowSize, bot.Y - arrowSize);
                PointF botB = new PointF(bot.X + arrowSize, bot.Y - arrowSize);
                gfx.DrawLine(pen, top, bot);
                gfx.DrawLine(pen, bot, botA);
                gfx.DrawLine(pen, bot, botB);
                gfx.DrawLine(pen, top, topA);
                gfx.DrawLine(pen, top, topB);

                // size text
                gfx.DrawString($"{size.Width}px",
                    new Font(ff, 12, FontStyle.Bold), brushMeasurements,
                    (float)(size.Width * .2), (float)(size.Height * .5));

                gfx.RotateTransform(-90);
                gfx.DrawString($"{size.Height}px",
                    new Font(ff, 12, FontStyle.Bold), brushMeasurements,
                    (float)(-size.Height * .4), (float)(size.Width * .5));
            }

            return bmp;
        }
    }
}
