using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SkiaSharp;
using OpenTK.Graphics.ES20;

namespace QuickPlot.FormsGL
{
    public partial class InteractivePlot : UserControl
    {
        SKColorType colorType = SKColorType.Rgba8888;
        GRBackendRenderTarget renderTarget;
        SKSurface surface;
        GRContext context;
        public Figure fig = new Figure();

        public InteractivePlot()
        {
            InitializeComponent();
            Disposed += OnDispose;

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                lblMessage.Text = "QuickPlotGL InteractivePlot\n(inside Visual Studio)";
            }
            else
            {
                lblMessage.Visible = false;
            }
        }

        private void GlControl1_Load(object sender, EventArgs e)
        {

        }

        private void OnDispose(Object sender, EventArgs e)
        {
            Console.WriteLine("disposing");
            context?.Dispose();
            renderTarget?.Dispose();
            surface?.Dispose();
        }

        private void GlControl1_Paint(object sender, PaintEventArgs e)
        {

            Control senderControl = (Control)sender;

            if (context == null)
            {
                var glInterface = GRGlInterface.CreateNativeGlInterface();
                context = GRContext.Create(GRBackend.OpenGL, glInterface);
            }

            if (renderTarget == null || surface == null || renderTarget.Width != senderControl.Width || renderTarget.Height != senderControl.Height)
            {
                renderTarget?.Dispose();

                GL.GetInteger(GetPName.FramebufferBinding, out var framebuffer);
                GL.GetInteger(GetPName.StencilBits, out var stencil);
                var glInfo = new GRGlFramebufferInfo((uint)framebuffer, colorType.ToGlSizedFormat());
                renderTarget = new GRBackendRenderTarget(senderControl.Width, senderControl.Height, context.GetMaxSurfaceSampleCount(colorType), stencil, glInfo);
                surface?.Dispose();
                surface = SKSurface.Create(context, renderTarget, GRSurfaceOrigin.BottomLeft, SKColorType.Rgba8888);
            }

            fig.Render(surface);

            surface.Canvas.Flush();
            glControl1.SwapBuffers();
        }
    }
}
