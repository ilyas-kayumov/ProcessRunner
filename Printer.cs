using System;

namespace ProcessRunner
{
    public class Printer
    {
        public void PrintInvalidArguments()
        {
            Console.WriteLine("Invalid number of arguments");
        }

        public void PrintExited()
        {
            Console.WriteLine("Process exited");
        }
    }
}
