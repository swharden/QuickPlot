using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlot
{
    public static class Tools
    {
        public static SKPoint randomPoint(int width, int height, Random rand)
        {
            float x = (float)(width * rand.NextDouble());
            float y = (float)(height * rand.NextDouble());
            return new SKPoint(x, y);
        }

        public static SKRect RectShrinkBy(SKRect rect, float left = 0, float right = 0, float bottom = 0, float top = 0)
        {
            rect.Left += left;
            rect.Right -= right;
            rect.Top += top;
            rect.Bottom -= bottom;
            return rect;
        }

        public static SKRect RectMatchVert(SKRect rect, SKRect reference)
        {
            rect.Top = reference.Top;
            rect.Bottom = reference.Bottom;
            return rect;
        }

        public static SKRect RectMatchHoriz(SKRect rect, SKRect reference)
        {
            rect.Left = reference.Left;
            rect.Right = reference.Right;
            return rect;
        }

        public static SKPoint RectCenter(SKRect rect)
        {
            return new SKPoint(RectCenterX(rect), RectCenterY(rect));
        }

        public static float RectCenterX(SKRect rect)
        {
            return (rect.Right + rect.Left) / 2;
        }

        public static float RectCenterY(SKRect rect)
        {
            return (rect.Top + rect.Bottom) / 2;
        }
    }
}
