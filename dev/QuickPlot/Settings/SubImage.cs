using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot.Settings
{

    public class SubImage
    {
        public int left, right, bottom, top;
        public int Width { get { return right - left; } }
        public int Height { get { return bottom - top; } }

        public SubImage(int left, int right, int bottom, int top)
        {
            if (bottom < top)
                throw new ArgumentException("bottom pixel is always > top pixel");

            this.left = left;
            this.right = right;
            this.bottom = bottom;
            this.top = top;
        }

        public void Shrink(int left = 0, int right = 0, int bottom = 0, int top = 0)
        {
            this.left += left;
            this.right -= right;
            this.bottom -= bottom;
            this.top += top;
        }

        public void ShrinkAll(int px)
        {
            Shrink(px, px, px, px);
        }

        public override string ToString()
        {
            return $"Dimensions left={left}, right={right}, bot={bottom}, top={top}, width={Width}, height={Height}";
        }
    }
}
