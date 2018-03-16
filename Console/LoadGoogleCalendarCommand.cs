using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Util.Store;
using Microsoft.Extensions.CommandLineUtils;
using Pari.Ics2Google.Console;
using Pari.Ics2Google.Core;
using Pari.Ics2Google.Core.LoadGooleCalendar;

namespace Pari.Ics2Google.Console
{
    // TODO: clean up!
    public class LoadGoogleCalendarCommand : Command
    {
        private readonly IUseCase<string> loadGoogleCalendarUseCase;

        private readonly ClientSecretArgument clientSecretArgument;

        public LoadGoogleCalendarCommand(IUseCase<string> loadGoogleCalendarUseCase, ClientSecretArgument clientSecretArgument) 
            : base("loadgoogle", "Load a google calendar.")
        {
            this.loadGoogleCalendarUseCase = loadGoogleCalendarUseCase;
            this.clientSecretArgument = clientSecretArgument;
        }

        public override IList<Argument> Arguments()
        {
            var arguments = new List<Argument>();
            arguments.Add(this.clientSecretArgument);
            return arguments;
        }

        public override int Execute(IDictionary<string, CommandArgument> arguments)
        {
            UserCredential credential = this.clientSecretArgument.CreateCredential(arguments);

            IOutput<string> result = this.loadGoogleCalendarUseCase.Execute(new LoadGoogleCalendarInput(credential));

            return 0;
        }

        public override bool Validate(IDictionary<string, CommandArgument> arguments)
        {
            return this.clientSecretArgument.Validate(arguments);
        }
    }
}
