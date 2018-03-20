using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Util.Store;
using Microsoft.Extensions.CommandLineUtils;

namespace Pari.Ics2Google.Console.Arguments
{
    /// <summary>
    /// Handles the command line part of google calendar access. This means loading the client secret and creating the <see cref="UserCredential"/>.
    /// </summary>
    public class ClientSecretArgument : Argument
    {
        private const string ClientSecretName = "clientSecret";

        private const string ClientSecretDescription = "Client secret required for google authentication.";

        public ClientSecretArgument() : base((string) ClientSecretName, (string) ClientSecretDescription)
        {
        }

        public override bool Validate(IDictionary<string, CommandArgument> arguments)
        {
            if (!arguments.TryGetValue(ClientSecretName, out var clientSecretArgument) ||
                clientSecretArgument == null || !clientSecretArgument.HasValue() || !File.Exists(clientSecretArgument.Value))
            {
                System.Console.WriteLine(string.Format((string) "{0} is invalid or missing. It should be a valid path to an ics file.", (object) ClientSecretName));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Creates the user credential instance from the client secret json and also stores in the user's Documents directory for later use.
        /// </summary>
        /// <param name="arguments">The arguments of the command which should contiam the path of the secret.</param>
        /// <returns>The created <see cref="UserCredential"/> instance.</returns>
        public UserCredential CreateCredential(IDictionary<string, CommandArgument> arguments)
        {
            UserCredential credential;

            CommandArgument argument;
            if (!arguments.TryGetValue(ClientSecretName, out argument))
            {
                throw new ArgumentException(string.Format((string) "The arguments does not contain the {} parameter.", (object) ClientSecretName), "arguments");
            }

            string clientSecretPath = argument.Value;

            using (var stream = File.OpenRead(clientSecretPath))
            {
                string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/ics2google-google-api.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new [] { CalendarService.Scope.Calendar },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            return credential;
        }
    }
}
