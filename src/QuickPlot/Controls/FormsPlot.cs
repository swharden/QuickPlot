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
using System.IO;

namespace QuickPlot.Controls
{
    public partial class FormsPlot : UserControl
    {
        private bool InsideVisualStudio { get { return (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv"); } }

        public FormsPlot()
        {
            InitializeComponent();

            glControl1.Paint += new PaintEventHandler(Render);
        }

        public void Render(object sender, PaintEventArgs e)
        {
            int width = glControl1.Width;
            int height = glControl1.Height;

            // setup the Skia surface using OpenGL
            SKColorType colorType = SKColorType.Rgba8888;
            GRContext contextOpenGL = GRContext.Create(GRBackend.OpenGL, GRGlInterface.CreateNativeGlInterface());
            GL.GetInteger(GetPName.FramebufferBinding, out var framebuffer);
            GRGlFramebufferInfo glInfo = new GRGlFramebufferInfo((uint)framebuffer, colorType.ToGlSizedFormat());
            GL.GetInteger(GetPName.StencilBits, out var stencil);
            GRBackendRenderTarget renderTarget = new GRBackendRenderTarget(width, height, contextOpenGL.GetMaxSurfaceSampleCount(colorType), stencil, glInfo);
            SKSurface surface = SKSurface.Create(contextOpenGL, renderTarget, GRSurfaceOrigin.BottomLeft, colorType);

            // clear the background
            surface.Canvas.Clear(SKColor.Parse("#FFFFFF"));

            // draw a line
            var paint = new SKPaint();
            paint.Color = new SKColor(0, 0, 0);
            surface.Canvas.DrawLine(0, 0, width, height, paint);

            // display using the GLControl
            surface.Canvas.Flush();
            glControl1.SwapBuffers();

            /*
            // display using a picturebox
            using (SKImage image = surface.Snapshot())
            using (SKData data = image.Encode(SKEncodedImageFormat.Png, 100))
            using (MemoryStream mStream = new MemoryStream(data.ToArray()))
            {
                Bitmap bm = new Bitmap(mStream, false);
                pictureBox1.Image = bm;
            }
            */

            // prevent memory access violations by disposing before exiting
            renderTarget?.Dispose();
            contextOpenGL?.Dispose();
            surface?.Dispose();
        }
    }
}
