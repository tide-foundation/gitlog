using System.Diagnostics;
using System.IO;
using System.Text;

namespace Log
{
    class Program
    {
        static void Main(string[] args) {
            var baseFolder = args[0];
            var overrideArgument = args.Length > 1 ? args[1] : null;

            var repos = File.ReadAllText($"{baseFolder}/config.txt").Split(',');
            var builder = new StringBuilder();
            foreach (var repo in repos) {
                builder.Append($"{repo}\n");

                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        WorkingDirectory = $"{baseFolder}/{repo}",
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = "cmd.exe",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        RedirectStandardInput = true,
                        CreateNoWindow = true,
                        Arguments = overrideArgument ?? "/c git log --date=iso --all --pretty=format:\" % h,% an,% ad,|% s | \" --after=\"2020 - 06 - 30\""
                    }
                };
            
                proc.Start();

                proc.StandardInput.Flush();
                proc.StandardInput.Close();

                var log = proc.StandardOutput.ReadToEnd();

                builder.Append($"{log}\n\n\n");
            }

            File.WriteAllText($"{baseFolder}/output.txt", builder.ToString().Replace('|','"'));
        }
    }
}
