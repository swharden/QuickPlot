using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot
{
    /// <summary>
    /// The region of an image we are encouraged to work in
    /// </summary>
    public class Region
    {
        public readonly Bitmap bmp;
        public readonly Graphics gfx;
        public Rectangle rect;
        public bool enabled = true;

        public Point Point { get { return new Point(rect.X, rect.Y); } }
        public Size Size { get { return new Size(rect.Width, rect.Height); } set { rect.Width = value.Width; rect.Height = value.Height; } }
        public int X { get { return rect.X; } set { rect.X = value; } }
        public int X2 { get { return rect.X + rect.Width; } }
        public int Y { get { return rect.Y; } set { rect.Y = value; } }
        public int Y2 { get { return rect.Y + rect.Height; } }
        public int Width { get { return rect.Width; } set { { rect.Width = value; } } }
        public int Height { get { return rect.Height; } set { { rect.Height = value; } } }
        public bool IsValid { get { return ((Width > 0) && (Height > 0) && (enabled)); } }

        public override string ToString()
        {
            //return $"Region of bmp [{bmp.Width}, {bmp.Height}] at ({X}, {Y}) of size [{Width}, {Height}]";
            return $"Region of bmp [{bmp.Width}, {bmp.Height}]: x1={X}, x2={X2}, y1={Y}, y2={Y2}";
        }

        /*
        public Region()
        {
            this.rect = new Rectangle(0, 0, 0, 0);
            this.bmp = null;
            this.gfx = null;
        }
        */

        public Region(Region reg)
        {
            this.rect = reg.rect;
            this.bmp = reg.bmp;
            this.gfx = reg.gfx;
        }

        public Region(Bitmap bmp, Graphics gfx)
        {
            rect = new Rectangle(new Point(0, 0), bmp.Size);
            this.bmp = bmp;
            this.gfx = gfx;
        }

        public Region(Bitmap bmp, Graphics gfx, Rectangle initialRegion)
        {
            this.rect = initialRegion;
            this.bmp = bmp;
            this.gfx = gfx;
        }

        public void ShrinkBy(int px)
        {
            ShrinkBy(px, px, px, px);
        }

        public void ShrinkBy(int left = 0, int right = 0, int bottom = 0, int top = 0)
        {
            rect.X += left;
            rect.Width -= (left + right);
            rect.Y += top;
            rect.Height -= (top + bottom);
        }

        public void ShrinkTo(int? left = null, int? right = null, int? bottom = null, int? top = null)
        {
            if (left != null)
            {
                Width = (int)left;
            }

            if (right != null)
            {
                int shrinkBy = Width - (int)right;
                X += shrinkBy;
                Width = (int)right;
            }

            if (bottom != null)
            {
                int shrinkBy = Height - (int)bottom;
                Y += shrinkBy;
                Height = (int)bottom;
            }

            if (top != null)
            {
                Height = (int)top;
            }
        }

        public void Shift(int right = 0, int left = 0, int down = 0, int up = 0)
        {
            X += right;
            X -= left;
            Y -= up;
            Y += down;
        }

        public void MatchX(Region regToMatch)
        {
            X = regToMatch.X;
            Width = regToMatch.Width;
        }

        public void MatchY(Region regToMatch)
        {
            Y = regToMatch.Y;
            Height = regToMatch.Height;
        }

        public void Match(Region regToMatch)
        {
            MatchX(regToMatch);
            MatchY(regToMatch);
        }

        public void SizeZero()
        {
            Width = 0;
            Height = 0;
        }

        public void Fill(Color color, int alpha = 255)
        {
            color = Color.FromArgb(alpha, color);
            Brush brush = new SolidBrush(color);
            gfx.FillRectangle(brush, rect);
        }

        public void Outline(Color color, int alpha = 255)
        {
            color = Color.FromArgb(alpha, color);
            Pen pen = new Pen(color);
            gfx.DrawRectangle(pen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
        }

        public void Label(string label, Color color, int alpha = 255, bool outlineToo = true, bool fillToo = true)
        {
            if (!IsValid)
                return;

            color = Color.FromArgb(alpha, color);
            Brush brush = new SolidBrush(color);
            Font fnt = new Font(FontFamily.GenericMonospace, 8);

            if (Width >= Height)
            {
                gfx.DrawString(label, fnt, brush, rect.X, rect.Y);
            }
            else
            {
                var sf = Tools.Misc.StringFormat(h: AlignHoriz.left, v: AlignVert.bottom);
                gfx.RotateTransform(90);
                gfx.DrawString(label, fnt, brush, rect.Y, -rect.X, sf);
                gfx.ResetTransform();
            }


            if (fillToo)
                Fill(color, alpha / 10);
            if (outlineToo)
                Outline(color, alpha);
        }
    }
}
