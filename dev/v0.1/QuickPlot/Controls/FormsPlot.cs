using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickPlot.Controls
{
    public partial class FormsPlot : UserControl
    {
        public FormsPlot()
        {
            InitializeComponent();
            this.skControl1.PaintSurface += SkControl1_PaintSurface;
        }

        private void SkControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
        {
            QuickPlot.Render.RandomLines(e.Surface);
        }
    }
}
