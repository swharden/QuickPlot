using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Graphics;
using OpenTK.Graphics.ES20;
using SkiaSharp;

namespace QuickPlot.Forms
{
    public partial class InteractivePlot : UserControl
    {
        public Figure fig = new Figure();
        public readonly bool IsUsingOpenGL;

        SkiaSharp.Views.Desktop.SKControl skControl1;
        OpenTK.GLControl glControl1;

        SKColorType colorType = SKColorType.Rgba8888;
        GRBackendRenderTarget renderTarget;
        SKSurface surface;
        GRContext context;

        public InteractivePlot()
        {
            InitializeComponent();
            Disposed += OnDispose;

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                lblMessage.Text = "QuickPlot InteractivePlot\n(inside Visual Studio)";
            }
            else
            {
                lblMessage.Visible = false;
                try
                {
                    CreateControl(true);
                    IsUsingOpenGL = true;
                }
                catch
                {
                    CreateControl(false);
                    IsUsingOpenGL = false;
                }
            }
        }

        private void OnDispose(Object sender, EventArgs e)
        {
            renderTarget?.Dispose();
            surface?.Dispose();
            context?.Dispose();
        }

        private void CreateControl(bool useOpenGL = false)
        {
            if (useOpenGL)
            {
                ColorFormat colorFormat = new ColorFormat(8, 8, 8, 8);
                int depth = 24;
                int stencil = 8;
                int samples = 4;
                GraphicsMode graphicsMode = new GraphicsMode(colorFormat, depth, stencil, samples);
                glControl1 = new OpenTK.GLControl(graphicsMode)
                {
                    BackColor = Color.FromArgb(0, 0, 192),
                    VSync = true,
                    Dock = DockStyle.Fill
                };
                glControl1.Paint += new PaintEventHandler(GlControl1_Paint);
                Controls.Add(glControl1);
                glControl1.BringToFront();
                glControl1.Update();
            }
            else
            {
                skControl1 = new SkiaSharp.Views.Desktop.SKControl
                {
                    BackColor = Color.FromArgb(192, 0, 0),
                    Dock = DockStyle.Fill
                };
                skControl1.PaintSurface += new EventHandler<SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs>(SkControl1_PaintSurface);
                Controls.Add(skControl1);
                skControl1.BringToFront();
                glControl1.Update();
            }
        }

        private void SkControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
        {
            fig.Render(e.Surface.Canvas);
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

            fig.Render(surface.Canvas);

            surface.Canvas.Flush();
            glControl1.SwapBuffers();
        }
    }
}
