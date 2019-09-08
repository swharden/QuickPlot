using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot
{
    /// <summary>
    /// Rection.rect is the sub-region of an Image (Region.bmp) we are expected to work in.
    /// This object should be GDI-Free.
    /// </summary>
    public class Region
    {
        public Rectangle rect;
        public bool enabled = true;

        public Point Point { get { return new Point(rect.X, rect.Y); } }
        public Point Center { get { return new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2); } }
        public Point CenterRotNeg90 { get { return new Point(-Center.Y, Center.X); } }
        public Point CenterRotPos90 { get { return new Point(Center.Y, -Center.X); } }
        public Size Size { get { return new Size(rect.Width, rect.Height); } set { rect.Width = value.Width; rect.Height = value.Height; } }
        public int X { get { return rect.X; } set { rect.X = value; } }
        public int X2 { get { return rect.X + rect.Width; } }
        public int Y { get { return rect.Y; } set { rect.Y = value; } }
        public int Y2 { get { return rect.Y + rect.Height; } }
        public int Width { get { return rect.Width; } set { { rect.Width = value; } } }
        public int Height { get { return rect.Height; } set { { rect.Height = value; } } }
        public bool IsValid { get { return ((Width > 0) && (Height > 0) && (enabled)); } }
        public Point TopLeft { get { return new Point(rect.Left, rect.Top); } }
        public Point TopRight { get { return new Point(rect.Right, rect.Top); } }
        public Point BottomLeft { get { return new Point(rect.Left, rect.Bottom); } }
        public Point BottomRight { get { return new Point(rect.Right, rect.Bottom); } }

        public override string ToString()
        {
            return $"Region: x1={X}, x2={X2}, y1={Y}, y2={Y2}";
        }

        public Region()
        {
            rect = new Rectangle(0, 0, 0, 0);
        }

        public Region(Region region)
        {
            rect = region.rect;
        }

        public Region(Size size)
        {
            rect = new Rectangle(new Point(0, 0), size);
        }

        public Region(Rectangle rect)
        {
            this.rect = rect;
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

    }
}
