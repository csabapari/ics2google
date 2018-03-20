using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Extensions.CommandLineUtils;

namespace Pari.Ics2Google.Console.Arguments
{
    public class ImportFromArgument : Argument
    {
        private const string ImportFromName = "from";

        private const string ImportFromDescription = "Starting from the specified date all the events are imported. Format: " + DateFormat;

        private const string DateFormat = "yyyy-MM-dd";

        public ImportFromArgument() : base(ImportFromName, ImportFromDescription)
        {
        }

        public override bool Validate(IDictionary<string, CommandArgument> arguments)
        {
            if (!arguments.TryGetValue(ImportFromName, out var fromArgument) ||
                fromArgument == null || !fromArgument.HasValue() || !DateTime.TryParseExact(fromArgument.Value, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                System.Console.WriteLine(string.Format("{0} is invalid or missing: '{2}'. It should be a valid date with following format: {1}.", ImportFromName, DateFormat, fromArgument?.Value));
                return false;
            }

            return true;
        }

        public DateTime GetDate(IDictionary<string, CommandArgument> arguments)
        {
            CommandArgument fromArgument;
            if (!arguments.TryGetValue(ImportFromName, out fromArgument))
            {
                throw new ArgumentException(string.Format("The arguments does not contain the {} parameter.", ImportFromName), "arguments");
            }

            return DateTime.ParseExact(fromArgument.Value, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
        }
    }
}
