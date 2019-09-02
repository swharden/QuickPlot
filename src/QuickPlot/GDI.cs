using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot
{
    public static class GDI
    {
        public static Bitmap TextImage(string text, float fontSize = 14, bool bold = true)
        {
            FontStyle style = (bold) ? FontStyle.Bold : FontStyle.Regular;

            FontFamily fmly = new FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif);
            Font fnt = new Font(fmly, fontSize, style);
            Graphics gfx = Graphics.FromHwnd(IntPtr.Zero);
            SizeF textSize = gfx.MeasureString(text, fnt);
            if (bold)
                textSize.Width += fontSize / 4;

            Brush brsh = new SolidBrush(Color.Black);
            Bitmap bmp = new Bitmap((int)textSize.Width, (int)textSize.Height);
            gfx = Graphics.FromImage(bmp);
            gfx.Clear(Color.White);
            gfx.DrawString(text, fnt, brsh, 0, 0);
            return bmp;
        }

        public static Bitmap Message(int width, int height, string message)
        {
            Bitmap bmp = new Bitmap(width, height);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.Clear(Color.White);
            gfx.DrawString(message, new Font(FontFamily.GenericMonospace, 10), Brushes.Black, 0, 0);
            return bmp;
        }

        public static Bitmap Scalebar(int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.Clear(Color.LightBlue);
            return bmp;
        }

        public static Bitmap RotateRight(Bitmap bmp)
        {
            float angle = 90;
            Bitmap bmpRot = new Bitmap(bmp.Height, bmp.Width);
            Graphics gfxRot = Graphics.FromImage(bmpRot);
            gfxRot.RotateTransform(angle);
            gfxRot.DrawImage(bmp, new Point(0, -bmp.Height));
            return bmpRot;
        }

        public static Bitmap RotateLeft(Bitmap bmp)
        {
            float angle = -90;
            Bitmap bmpRot = new Bitmap(bmp.Height, bmp.Width);
            Graphics gfxRot = Graphics.FromImage(bmpRot);
            gfxRot.RotateTransform(angle);
            gfxRot.DrawImage(bmp, new Point(-bmp.Width, -0));
            return bmpRot;
        }

        public static Bitmap Outline(Bitmap bmp)
        {
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.DrawRectangle(Pens.Black, 0, 0, bmp.Width - 1, bmp.Height - 1);
            return bmp;
        }
    }
}
