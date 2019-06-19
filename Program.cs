using System;
using System.Diagnostics;

namespace ProcessRunner
{

    public class Program
    {
        private static Printer printer = new Printer();

        public static void Main(string[] args)
        {
            var argumentsInfo = new ArgumentsInfo(args);

            if (!argumentsInfo.IsValid())
            {
                printer.PrintInvalidArguments();
                return;
            }

            var processPath = argumentsInfo.GetProcessPath();
            var processArguments = argumentsInfo.GetProcessArguments();

            var startInfo = new ProcessStartInfo(processPath, processArguments);

            var runCount = argumentsInfo.GetProcessRunCount() ?? 1;

            while (runCount > 0)
            {
                using (var process = Process.Start(startInfo))
                {
                    process.EnableRaisingEvents = true;
                    process.Exited += ExitedHandler;
                    process.WaitForExit();
                }

                runCount--;
            }
        }

        public static void ExitedHandler(object sender, EventArgs e)
        {
            printer.PrintExited();
        }
    }
}
