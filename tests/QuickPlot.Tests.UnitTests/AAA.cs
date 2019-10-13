using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace QuickPlot.Tests.UnitTestsCore3
{
    [TestClass]
    public class AAA
    {
        [TestMethod]
        public void ClearOldImages()
        {
            foreach (string fileName in System.IO.Directory.GetFiles("./", "*.png"))
            {
                string filePath = System.IO.Path.GetFullPath(fileName);
                Debug.WriteLine($"deleting {filePath}");
                System.IO.File.Delete(filePath);
            }
        }
    }
}
