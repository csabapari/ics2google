using System;
using System.Collections.Generic;
using System.Text;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.CommandLineUtils;

namespace Pari.Ics2Google.Console
{
    public class Import2GoogleCommand : Command
    {
        private readonly ClientSecretArgument clientSecretArgument;

        private readonly IcsPathArgument icsPathArgument;

        private readonly GoogleCalendarArgument googleCalendarArgument;

        public Import2GoogleCommand(ClientSecretArgument clientSecretArgument, IcsPathArgument icsPathArgument, GoogleCalendarArgument googleCalendarArgument)
            : base("import", "Imports the ics calendar to the given google calendar.")
        {
            this.clientSecretArgument = clientSecretArgument;
            this.icsPathArgument = icsPathArgument;
            this.googleCalendarArgument = googleCalendarArgument;
        }

        public override IList<Argument> Arguments()
        {
            var arguments = new List<Argument>();
            arguments.Add(this.clientSecretArgument);
            arguments.Add(this.icsPathArgument);
            return arguments;
        }

        public override int Execute(IDictionary<string, CommandArgument> arguments)
        {
            UserCredential credential = this.clientSecretArgument.CreateCredential(arguments);

            CommandArgument icsPath = arguments[this.icsPathArgument.Name];

            CommandArgument googleCalendar = arguments[this.googleCalendarArgument.Name];

            return 0;
        }

        public override bool Validate(IDictionary<string, CommandArgument> arguments)
        {
            // TOOD logging
            return this.clientSecretArgument.Validate(arguments) && 
                   this.icsPathArgument.Validate(arguments) &&
                   this.googleCalendarArgument.Validate(arguments);
        }
    }
}
