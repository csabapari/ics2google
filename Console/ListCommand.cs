using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.CommandLineUtils;
using Pari.Ics2Google.Core;
using Pari.Ics2Google.Core.ListEvent;

namespace Pari.Ics2Google.Console
{
    public class ListCommand : Command
    {
        private const string IcsPathArgument = "icsPath";

        private const string IcsPathDescription = "The file path of the ics file exported from a calendar (like zimbra)";

        private readonly IUseCase<IList<string>> listEventUseCase;

        public ListCommand(IUseCase<IList<string>> listEventUseCase) : base("list", "List the calendar events.")
        {
            this.listEventUseCase = listEventUseCase;
        }

        public override IList<Argument> Arguments()
        {
            var arguments = new List<Argument>();
            arguments.Add(new Argument(IcsPathArgument, IcsPathDescription));
            return arguments;
        }

        public override bool Validate(IDictionary<string, CommandArgument> arguments)
        {
            if (!arguments.TryGetValue(IcsPathArgument, out var icsArgument) || 
                icsArgument == null || !icsArgument.HasValue() || !File.Exists(icsArgument.Value))
            {
                System.Console.WriteLine(string.Format("{0} is invalid or missing. It should be a valid path to an ics file.", IcsPathArgument));
                return false;
            }

            return true;
        }

        public override int Execute(IDictionary<string, CommandArgument> arguments)
        {
            CommandArgument icsPath = arguments[IcsPathArgument];
            
            System.Console.WriteLine(string.Format("Ics file path: {0}.", icsPath.Value));

            IOutput<IList<string>> output = this.listEventUseCase.Execute(new ListEventInput(icsPath.Value));

            foreach (string data in output.Data)
            {
                System.Console.WriteLine(data);
            }

            return 0;
        }
    }
}
