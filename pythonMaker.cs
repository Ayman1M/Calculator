using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Calculator
{
    internal class pythonMaker
    {
        private string path = @"C:\Users\Public\Documents\";
        private string pythonFileName = "calc.py";
        private string resultFileName = "result.txt";
        private string content;

        public string result_
        {
            get { return content; }
        }
        private bool goodresult = true;
        public bool goodresult_()
        {
            return goodresult;
        }
        private void Prossissing(string expression)
        {
            // Format expression safely for Python
            string resultPath = Path.Combine(path, resultFileName).Replace("\\", "/");
            string python_code = $@"
import math
import decimal

def cubic(x):
    return x ** (1 / 3)

try:
    x = decimal.Decimal({expression})
    with open(r'{resultPath}', 'w') as f:
        f.write(str(x))
except Exception as e:
    with open(r'{resultPath}', 'w') as f:
        f.write(str(e))
";

            string pythonFilePath = Path.Combine(path, pythonFileName);

            // Write Python code to file
            File.WriteAllText(pythonFilePath, python_code);

            // Run Python file
            RunPythonScript(pythonFilePath);

            // Wait for script to finish
            Thread.Sleep(60);

            string resultFullPath = Path.Combine(path, resultFileName);
            if (File.Exists(resultFullPath))
            {
                content = File.ReadAllText(resultFullPath);
                var count = 0;
                foreach (char c in content)
                {
                    count++;
                    if (char.IsLetter(c) && count > 5)
                    {
                        this.goodresult = false;

                        break;
                    }
                }
            }
            else
            {
                content = "Error: result file not found.";
            }

            // Cleanup
            if (File.Exists(pythonFilePath)) File.Delete(pythonFilePath);
            if (File.Exists(resultFullPath)) File.Delete(resultFullPath);
        }

        public pythonMaker(string mathExpression)
        {
            Prossissing(mathExpression);
        }

        private void RunPythonScript(string scriptPath)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"\"{scriptPath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process p = Process.Start(psi))
            {
                p.WaitForExit();
            }
        }
    }
}
