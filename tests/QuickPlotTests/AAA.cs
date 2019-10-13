using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuickPlot.Tests.UnitTests
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
