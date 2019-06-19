using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessRunner
{
    public class ArgumentsInfo
    {
        public const string RunCountName = "-runCount=";

        private readonly string[] args;

        public ArgumentsInfo(string[] args)
        {
            this.args = args;
        }

        public bool IsValid()
        {
            return args.Length > 0;
        }

        public string GetProcessPath()
        {
            if (!IsValid())
            {
                throw new InvalidOperationException();
            }

            return args[0];
        }

        public string GetProcessArguments()
        {
            if (!IsValid())
            {
                throw new InvalidOperationException();
            }

            var processArguments = GetArguments().Where(a => !a.StartsWith(RunCountName));

            return string.Join(' ', processArguments);
        }

        private IEnumerable<string> GetArguments()
        {
            return args.Skip(1);
        }

        public int? GetProcessRunCount()
        {
            if (!IsValid())
            {
                throw new InvalidOperationException();
            }

            var result = GetArguments().FirstOrDefault(a => a.StartsWith(RunCountName));

            string countStr;

            if (result != null)
            {
                countStr = result.Substring(RunCountName.Length, result.Length - RunCountName.Length);

                if (int.TryParse(countStr, out int count))
                {
                    return count;
                }
            }

            return null;
        }
    }
}
