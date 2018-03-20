using System.Collections.Generic;
using Microsoft.Extensions.CommandLineUtils;

namespace Pari.Ics2Google.Console.Arguments
{
    public class GoogleCalendarArgument : Argument
    {
        private const string GoogleCalendarName = "calendar";

        private const string GoogleCalendarDescription = "The name of the secondary google calendar the ics should be imported.";

        public GoogleCalendarArgument() : base((string) GoogleCalendarName, (string) GoogleCalendarDescription)
        {
        }

        public override bool Validate(IDictionary<string, CommandArgument> arguments)
        {
            return arguments.TryGetValue(GoogleCalendarName, out var googleCalendarArgument) &&
                   googleCalendarArgument != null && 
                   googleCalendarArgument.HasValue();
        }
    }
}
