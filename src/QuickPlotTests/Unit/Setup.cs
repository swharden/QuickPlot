using NUnit.Framework;
using System;

namespace QuickPlotTests.Unit
{
    [SetUpFixture]
    public class Setup
    {
        [OneTimeSetUp]
        public void ClearOutputFolder()
        {
            if (System.IO.Directory.Exists(Tools.outputFolder))
                System.IO.Directory.Delete(Tools.outputFolder, true);

            System.IO.Directory.CreateDirectory(Tools.outputFolder);

            Console.WriteLine($"Cleared output folder: {Tools.outputFolder}");
        }

        [OneTimeTearDown]
        public void WrapUp()
        {
            // todo: generate markdown and HTML reports
        }
    }
}
