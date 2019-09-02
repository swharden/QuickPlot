using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot
{
    public class Dimensions
    {
        public int left;
        public int right;
        public int top;
        public int bot;

        public int Width { get { return right - left; } }
        public int Height { get { return bot - top; } }

        public Dimensions(int width, int height)
        {
            left = 0;
            right = width;
            top = 0;
            bot = height;
        }

        public void Shrink(int left = 0, int right = 0, int bot = 0, int top = 0)
        {
            this.left += left;
            this.right -= right;
            this.bot -= bot;
            this.top += top;
        }

        public void ShrinkAll(int px)
        {
            Shrink(px, px, px, px);
        }

        public override string ToString()
        {
            return $"Dimensions left={left}, right={right}, bot={bot}, top={top}, width={Width}, height={Height}";
        }
    }
}
