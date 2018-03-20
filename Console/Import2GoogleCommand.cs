using System;
using System.Collections.Generic;
using System.Text;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.CommandLineUtils;
using Pari.Ics2Google.Console.Arguments;
using Pari.Ics2Google.Core;
using Pari.Ics2Google.Core.Import2Google;

namespace Pari.Ics2Google.Console
{
    public class Import2GoogleCommand : Command
    {
        private readonly ClientSecretArgument clientSecretArgument;

        private readonly IcsPathArgument icsPathArgument;

        private readonly GoogleCalendarArgument googleCalendarArgument;

        private readonly ImportFromArgument importFromArgument;

        private readonly IUseCase<object> import2GoogleUseCase;

        public Import2GoogleCommand(
            ClientSecretArgument clientSecretArgument, 
            IcsPathArgument icsPathArgument, 
            GoogleCalendarArgument googleCalendarArgument,
            ImportFromArgument importFromArgument,
            IUseCase<object> import2GoogleUseCase)
            : base("import", "Imports the ics calendar to the given google calendar.")
        {
            this.clientSecretArgument = clientSecretArgument;
            this.icsPathArgument = icsPathArgument;
            this.googleCalendarArgument = googleCalendarArgument;
            this.importFromArgument = importFromArgument;
            this.import2GoogleUseCase = import2GoogleUseCase;
        }

        public override IList<Argument> Arguments()
        {
            var arguments = new List<Argument>();
            arguments.Add(this.clientSecretArgument);
            arguments.Add(this.icsPathArgument);
            arguments.Add(this.googleCalendarArgument);
            arguments.Add(this.importFromArgument);
            return arguments;
        }

        public override int Execute(IDictionary<string, CommandArgument> arguments)
        {
            UserCredential credential = this.clientSecretArgument.CreateCredential(arguments);

            CommandArgument icsPath = arguments[this.icsPathArgument.Name];

            CommandArgument googleCalendar = arguments[this.googleCalendarArgument.Name];

            DateTime fromDate = this.importFromArgument.GetDate(arguments);

            IOutput<object> result = this.import2GoogleUseCase.Execute(new Import2GoogleInput(credential, icsPath.Value, googleCalendar.Value, fromDate));

            return result.ExecutionResult ? 0 : 1;
        }

        public override bool Validate(IDictionary<string, CommandArgument> arguments)
        {
            // TOOD logging
            return this.clientSecretArgument.Validate(arguments) && 
                   this.icsPathArgument.Validate(arguments) &&
                   this.googleCalendarArgument.Validate(arguments) &&
                   this.importFromArgument.Validate(arguments);
        }
    }
}
