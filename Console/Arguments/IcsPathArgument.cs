using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.CommandLineUtils;

namespace Pari.Ics2Google.Console.Arguments
{
    public class IcsPathArgument : Argument
    {
        private const string IcsPathName = "icsPath";

        private const string IcsPathDescription = "The file path of the ics file exported from a calendar (like zimbra)";

        public IcsPathArgument() : base((string) IcsPathName, (string) IcsPathDescription)
        {
        }

        public override bool Validate(IDictionary<string, CommandArgument> arguments)
        {
            if (!arguments.TryGetValue(IcsPathName, out var icsArgument) ||
                icsArgument == null || !icsArgument.HasValue() || !File.Exists(icsArgument.Value))
            {
                System.Console.WriteLine(string.Format("{0} is invalid or missing. It should be a valid path to an ics file.", IcsPathName));
                return false;
            }

            return true;
        }
    }
}
