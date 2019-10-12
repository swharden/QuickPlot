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
using System.Diagnostics;

namespace QuickPlotConcept
{
    // WARNING: the user control's project must be built for x86 because Visual Studio is 32-bit.
    //          not doing this will cause Visual Studio to crash when the user control is added.

    public partial class InteractivePlot : UserControl
    {
        SKColorType colorType = SKColorType.Rgba8888;
        GRBackendRenderTarget renderTarget;
        SKSurface surface;
        GRContext context;

        Figure figure = new Figure();

        public InteractivePlot()
        {
            InitializeComponent();

            Version ver = typeof(FormMain).Assembly.GetName().Version;
            lblVersion.Text = $"version {ver}";

            if (Process.GetCurrentProcess().ProcessName == "devenv")
            {
                glControl1.Visible = false;
            }
            else
            {
                lblTitle.Visible = false;
                lblSubtitle.Visible = false;
                lblVersion.Visible = false;
                glControl1.Paint += new PaintEventHandler(GlControl1_Paint);
            }

            Disposed += OnDispose;
        }

        private void OnDispose(Object sender, EventArgs e)
        {
            renderTarget?.Dispose();
            surface?.Dispose();
            context?.Dispose();
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

            figure.Render(surface.Canvas, senderControl.Width, senderControl.Height);

            surface.Canvas.Flush();
            glControl1.SwapBuffers();
        }
    }
}
