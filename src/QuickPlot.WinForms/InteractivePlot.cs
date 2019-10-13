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
using System.Diagnostics;
using SkiaSharp;
using OpenTK.Graphics.ES20;

namespace QuickPlot.WinForms
{
    public partial class InteractivePlot : UserControl
    {
        SKColorType colorType = SKColorType.Rgba8888;
        GRBackendRenderTarget renderTarget;
        SKSurface surface;
        GRContext context;

        readonly OpenTK.GLControl glControl1;

        public readonly Figure figure;

        public InteractivePlot()
        {
            InitializeComponent();
            lblVersion.Text = $"QuickPlot {typeof(Figure).Assembly.GetName().Version}";
            lblVersionForms.Text = $"QuickPlot.WinForms {typeof(InteractivePlot).Assembly.GetName().Version}";

            if (Process.GetCurrentProcess().ProcessName == "devenv" || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                // we are in designer mode
            }
            else
            {
                figure = new Figure();
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
                glControl1.Paint += new PaintEventHandler(GlControl1_Paint);
                Disposed += OnDispose;
            }
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

            figure.Render(surface.Canvas, new SKSize(senderControl.Width, senderControl.Height));

            surface.Canvas.Flush();
            glControl1.SwapBuffers();
        }
    }
}
