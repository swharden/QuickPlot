using NUnit.Framework;
using System;
using System.Text;

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
            GenerateHTML();
        }

        public void GenerateHTML()
        {
            StringBuilder sb = new StringBuilder("<h1>Test Output</h1>\n");

            string[] imagePaths = System.IO.Directory.GetFiles(Tools.outputFolder, "*.png");
            foreach (string path in imagePaths)
            {
                string basename = System.IO.Path.GetFileName(path);
                sb.AppendLine($"<h2><br><br>{System.IO.Path.GetFileNameWithoutExtension(basename)}</h2>");
                sb.AppendLine($"<img src='{basename}'>");
            }

            sb.Insert(0, "<body style='background-color: #EEE; margin: 30px;'>");
            sb.AppendLine("</body>");

            sb.Insert(0, "<html>");
            sb.AppendLine("</html>");

            string htmlFilePath = System.IO.Path.Join(Tools.outputFolder, "testResults.html");
            System.IO.File.WriteAllText(htmlFilePath, sb.ToString());
        }
    }
}
