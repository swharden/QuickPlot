using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace QuickPlotTests.Unit
{
    /// <summary>
    /// These tests run before other tests
    /// </summary>
    [TestFixture]
    class AAA
    {
        // TODO: find a better way to ensure this is the first test run
        [Test]
        public void ClearOutputFolder()
        {
            if (System.IO.Directory.Exists(Tools.outputFolder))
                System.IO.Directory.Delete(Tools.outputFolder, true);

            System.IO.Directory.CreateDirectory(Tools.outputFolder);

            Console.WriteLine($"Cleared output folder: {Tools.outputFolder}");
        }
    }
}
