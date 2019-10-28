using NUnit.Framework;
using System;
using System.Text;

namespace QuickPlotTests.Integration
{
    [SetUpFixture]
    public class Setup
    {
        [OneTimeSetUp]
        public void ClearOutputFolder()
        {
            System.Threading.Thread.Sleep(10);

            if (System.IO.Directory.Exists(Tools.outputFolder))
                System.IO.Directory.Delete(Tools.outputFolder, true);

            System.IO.Directory.CreateDirectory(Tools.outputFolder);

            Console.WriteLine($"Cleared output folder: {Tools.outputFolder}");

            System.Threading.Thread.Sleep(10);
        }

        [OneTimeTearDown]
        public void WrapUp()
        {
            // todo: generate markdown and HTML reports
            GenerateHTML();
        }

        public void GenerateHTML()
        {
            ReadAllSourceCode();

            StringBuilder sb = new StringBuilder("<h1>Test Output</h1>\n");

            string[] imagePaths = System.IO.Directory.GetFiles(Tools.outputFolder, "*.png");
            foreach (string path in imagePaths)
            {
                string functionName = System.IO.Path.GetFileNameWithoutExtension(path);
                sb.AppendLine($"<h2><br>{functionName}</h2>");
                sb.AppendLine($"<img src='{functionName}.png'>");
                sb.AppendLine("<pre style='font-family: monospace; background-color: #DDD; padding: 10px;'>");
                sb.AppendLine(GetSource(functionName));
                sb.AppendLine("</pre>");
            }

            sb.Insert(0, "<body style='background-color: #EEE; margin: 30px;'>");
            sb.AppendLine("</body>");

            sb.Insert(0, "<html>");
            sb.AppendLine("</html>");

            string htmlFilePath = System.IO.Path.Join(Tools.outputFolder, "testResults.html");
            System.IO.File.WriteAllText(htmlFilePath, sb.ToString());
        }

        string allSource = "";
        public void ReadAllSourceCode()
        {
            string pathSrc = System.IO.Path.GetFullPath("../../../Integration");
            string[] sourceFilePaths = System.IO.Directory.GetFiles(pathSrc, "Test*.cs");

            allSource = "";
            foreach (var path in sourceFilePaths)
            {
                Console.WriteLine($"including soure code from: {path}");
                allSource += System.IO.File.ReadAllText(path) + "\n";
            }
        }

        public string GetSource(string functionName)
        {
            int posStart = allSource.IndexOf($"public void {functionName}()");

            if (posStart < 0)
                throw new Exception($"function {functionName}() not found in source code");

            // format the code to be a pretty string
            string code = allSource;

            code = code.Substring(posStart);
            code = code.Substring(code.IndexOf("\n        {"));
            code = code.Substring(0, code.IndexOf("\n        }"));
            code = code.Trim();
            code = code.Trim(new char[] { '{', '}' });
            string[] lines = code.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Length > 12)
                    lines[i] = lines[i].Substring(12);
                if (lines[i].Contains("Tools.SaveFig"))
                    lines[i] = $"figure.Save(600, 400, \"{functionName}.png\");";
            }
            code = string.Join("\n", lines).Trim();
            return code;
        }
    }
}
