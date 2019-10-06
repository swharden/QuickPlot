using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlotTests
{
    [TestClass]
    public class MiscTests
    {
        [TestMethod]
        public void RenderDesignerModeBitmap()
        {
            Bitmap bmp = QuickPlot.Tools.DesignerModeBitmap(new Size(600, 400));
        }
    }
}
