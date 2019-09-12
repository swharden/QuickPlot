using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickPlot.Forms
{
    public partial class InteractivePlot : UserControl
    {
        public Figure fig = new Figure();

        public InteractivePlot()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                lblMessage.Text = "QuickPlot InteractivePlot\n(inside Visual Studio)";
            }
            else
            {
                lblMessage.Visible = false;
            }
        }

        private void SkControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
        {
            fig.Render(e.Surface);
        }
    }
}
