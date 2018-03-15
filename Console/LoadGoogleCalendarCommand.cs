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
    public class LoadGoogleCalendarCommand : Command
    {
        private const string ClientSecretArgument = "clientSecret";

        private const string ClientSecretDescription = "Client secret required for authentication.";

        private static readonly string[] Scopes = { CalendarService.Scope.CalendarReadonly };

        private readonly IUseCase<string> loadGoogleCalendarUseCase;

        public LoadGoogleCalendarCommand(IUseCase<string> loadGoogleCalendarUseCase) : base("loadgoogle", "Load a google calendar.")
        {
            this.loadGoogleCalendarUseCase = loadGoogleCalendarUseCase;
        }

        public override IList<Argument> Arguments()
        {
            var arguments = new List<Argument>();
            arguments.Add(new Argument(ClientSecretArgument, ClientSecretDescription));
            return arguments;
        }

        public override int Execute(IDictionary<string, CommandArgument> arguments)
        {
            UserCredential credential;

            string clientSecretPath = arguments[ClientSecretArgument].Value;

            using (var stream = File.OpenRead(clientSecretPath))
            {
                string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/ics2google-google-api.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
            IOutput<string> result = this.loadGoogleCalendarUseCase.Execute(new LoadGoogleCalendarInput(credential));

            return 0;
        }

        public override bool Validate(IDictionary<string, CommandArgument> arguments)
        {
            if (!arguments.TryGetValue(ClientSecretArgument, out var clientSecretArgument) ||
                clientSecretArgument == null || !clientSecretArgument.HasValue() || !File.Exists(clientSecretArgument.Value))
            {
                System.Console.WriteLine(string.Format("{0} is invalid or missing. It should be a valid path to an ics file.", ClientSecretArgument));
                return false;
            }

            return true;
        }
    }
}
