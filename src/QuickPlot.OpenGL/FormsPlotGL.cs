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
using OpenTK.Graphics;
using OpenTK.Graphics.ES20;

namespace QuickPlot.OpenGL
{
    public partial class FormsPlotGL : QuickPlot.Controls.FormsPlot
    {
        SKColorType colorType = SKColorType.Rgba8888;
        GRBackendRenderTarget renderTarget;
        SKSurface surface;
        GRContext context;
        OpenTK.GLControl glControl1;

        public FormsPlotGL()
        {
            InitializeComponent();
            SetupGlControl();
            lblInfo.Text = "this is an OpenGL plot";
            Disposed += OnDispose;
            this.skControl1.Dispose();
        }

        private void SetupGlControl()
        {

            var glColorFormat = new ColorFormat(8, 8, 8, 8);
            var glGraphicsMode = new GraphicsMode(glColorFormat, 24, 8, 4);
            glControl1 = new OpenTK.GLControl(glGraphicsMode)
            {
                BackColor = Color.FromArgb(0, 255, 0),
                Location = new Point(77, 35),
                Name = "glControl1",
                Size = new Size(403, 293),
                TabIndex = 1,
                VSync = false,
                Dock = DockStyle.Fill
            };
            Controls.Add(this.glControl1);
            glControl1.BringToFront();


            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                // dont set-up opengl if we are in Visual Studio
            }
            else
            {
                glControl1.Paint += new PaintEventHandler(GlControl1_Paint);
            }

            // destroy the old skia surface
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

            QuickPlot.Render.RandomLines(surface);

            surface.Canvas.Flush();
            glControl1.SwapBuffers();
            // glControl1.Invalidate();
        }
    }
}