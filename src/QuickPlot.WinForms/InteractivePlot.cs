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
        #region setup and rendering 

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
            lblVersionForms.Text = $"WinForms {typeof(InteractivePlot).Assembly.GetName().Version}";

            if (Process.GetCurrentProcess().ProcessName == "devenv" || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                // we are in designer mode
            }
            else
            {
                figure = new Figure();

                var color = SystemColors.Control;
                string colorHex = "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
                figure.backgroundColor = SKColor.Parse(colorHex);

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

            figure.Render(surface.Canvas, figureSize, plotEngagedWithMouse);

            surface.Canvas.Flush();
            glControl1.SwapBuffers();
        }

        public void Render()
        {
            glControl1.Refresh();
        }

        #endregion

        #region mouse interaction

        Plot plotEngagedWithMouse = null; // TODO: move this inside mouse class?
        readonly PlotSettings.MouseTracker mouse = new PlotSettings.MouseTracker();

        private void LockTickDensity(bool locked)
        {
            foreach (Plot subplot in figure.subplots)
            {
                subplot.xTicks.lockTickDensity = locked;
                subplot.yTicks.lockTickDensity = locked;
            }
        }

        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            var mousePoint = new SKPoint(e.Location.X, e.Location.Y);
            plotEngagedWithMouse = figure.PlotAtPoint(figureSize, mousePoint);

            if (plotEngagedWithMouse != null)
            {
                mouse.mouseDownLimits = new PlotSettings.AxisLimits(plotEngagedWithMouse.axes);

                if (e.Button == MouseButtons.Left)
                {
                    LockTickDensity(true);
                    glControl1.Cursor = Cursors.SizeAll;
                    mouse.leftDown = mousePoint;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    glControl1.Cursor = Cursors.NoMove2D;
                    mouse.rightDown = mousePoint;
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    glControl1.Cursor = Cursors.Cross;
                    mouse.middleDown = mousePoint;
                }
            }
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            var mousePoint = new SKPoint(e.Location.X, e.Location.Y);
            if (plotEngagedWithMouse != null)
            {
                mouse.now = mousePoint;
                plotEngagedWithMouse.axes.Set(mouse.mouseDownLimits);

                if (mouse.leftButtonIsDown)
                    plotEngagedWithMouse.axes.PanPixels(mouse.leftDelta.X, mouse.leftDelta.Y);

                if (mouse.rightButtonIsDown)
                    plotEngagedWithMouse.axes.ZoomPixels(mouse.rightDelta.X, mouse.rightDelta.Y);

                Render();
            }
            else
            {
                var plotUnderMouse = figure.PlotAtPoint(figureSize, mousePoint);
                glControl1.Cursor = (plotUnderMouse == null) ? Cursors.Arrow : Cursors.Cross;
            }
        }

        private void glControl1_MouseUp(object sender, MouseEventArgs e)
        {
            var mousePoint = new SKPoint(e.Location.X, e.Location.Y);
            if (plotEngagedWithMouse != null)
            {
                mouse.leftDown = new SKPoint(0, 0);
                mouse.rightDown = new SKPoint(0, 0);
                mouse.middleDown = new SKPoint(0, 0);
                LockTickDensity(false);

                if (e.Button == MouseButtons.Middle)
                {
                    plotEngagedWithMouse.AutoAxis();
                    Render();
                }
                plotEngagedWithMouse = null;
            }
        }

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            var mousePoint = new SKPoint(e.Location.X, e.Location.Y);
            double zoom = (e.Delta > 0) ? 1.15 : 0.85;
            figure.PlotAtPoint(figureSize, mousePoint)?.axes.Zoom(zoom, zoom);
            Render();
        }

        #endregion
    }
}
