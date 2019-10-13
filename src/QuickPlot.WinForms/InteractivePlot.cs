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

        public SKSize figureSize { get { return new SKSize(glControl1.Width, glControl1.Height); } }

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
                Controls.Add(glControl1);
                glControl1.BringToFront();
                glControl1.Paint += new PaintEventHandler(glControl1_Paint);
                glControl1.MouseDown += new MouseEventHandler(glControl1_MouseDown);
                glControl1.MouseMove += new MouseEventHandler(glControl1_MouseMove);
                glControl1.MouseUp += new MouseEventHandler(glControl1_MouseUp);
                Disposed += OnDispose;
            }
        }

        private void OnDispose(Object sender, EventArgs e)
        {
            renderTarget?.Dispose();
            surface?.Dispose();
            context?.Dispose();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
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

            figure.Render(surface.Canvas, figureSize);

            surface.Canvas.Flush();
            glControl1.SwapBuffers();
        }

        #region mouse interaction

        QuickPlot.Plot plotEngagedWithMouse = null;

        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            var mousePoint = new SKPoint(e.Location.X, e.Location.Y);
            plotEngagedWithMouse = figure.PlotUnderMouse(figureSize, mousePoint);

            if (plotEngagedWithMouse != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    glControl1.Cursor = Cursors.SizeAll;
                    plotEngagedWithMouse.MouseDown(mousePoint, left: true);
                }
                else if (e.Button == MouseButtons.Right)
                {
                    glControl1.Cursor = Cursors.NoMove2D;
                    plotEngagedWithMouse.MouseDown(mousePoint, right: true);
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    glControl1.Cursor = Cursors.Cross;
                    plotEngagedWithMouse.MouseDown(mousePoint, middle: true);
                }
            }
        }

        private void glControl1_MouseUp(object sender, MouseEventArgs e)
        {
            var mousePoint = new SKPoint(e.Location.X, e.Location.Y);
            if (plotEngagedWithMouse != null)
            {
                plotEngagedWithMouse.MouseUp(mousePoint);
                if (e.Button == MouseButtons.Middle)
                {
                    plotEngagedWithMouse.AutoAxis();
                    glControl1.Refresh();
                }
                plotEngagedWithMouse = null;
            }
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            var mousePoint = new SKPoint(e.Location.X, e.Location.Y);
            if (plotEngagedWithMouse != null)
            {
                plotEngagedWithMouse.MouseMove(mousePoint);
                glControl1.Refresh();
            }
            else
            {
                var plotUnderMouse = figure.PlotUnderMouse(figureSize, mousePoint);
                glControl1.Cursor = (plotUnderMouse == null) ? Cursors.Arrow : Cursors.Cross;
            }
        }

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            var mousePoint = new SKPoint(e.Location.X, e.Location.Y);
            double zoom = (e.Delta > 0) ? 1.15 : 0.85;
            figure.PlotUnderMouse(figureSize, mousePoint)?.axes.Zoom(zoom, zoom);
            glControl1.Refresh();
        }

        #endregion
    }
}
