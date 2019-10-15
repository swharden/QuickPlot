using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace QuickPlot.Tests.NUnitTests
{
    [TestFixture]
    class AAA
    {
        [Test]
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
