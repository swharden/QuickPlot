using System;
using System.Collections.Generic;
using System.Text;

namespace QuickPlotTests.Unit
{
    public static class Tools
    {
        private readonly static string outputFolderName = "output";
        public static string outputFolder { get { return System.IO.Path.GetFullPath(outputFolderName); } }

        public static void SaveFig(QuickPlot.Figure figure, string name, int width = 600, int height = 400)
        {
            string fileName = name + ".png";
            string filePath = System.IO.Path.Join(outputFolder, fileName);
            figure.Save(width, height, filePath);
            Console.WriteLine($"Saved: {filePath}");
        }
    }
}
