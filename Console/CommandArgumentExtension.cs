using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.CommandLineUtils;

namespace Pari.Ics2Google.Console
{
    public static class CommandArgumentExtension
    {
        public static bool HasValue(this CommandArgument argument)
        {
            if (argument == null)
            {
                throw new ArgumentNullException("argument");
            }

            return !string.IsNullOrEmpty(argument.Value);
        }
    }
}
