using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.CommandLineUtils;
using Pari.Ics2Google.Console.Arguments;
using Pari.Ics2Google.Core;
using Pari.Ics2Google.Core.ListEvent;

namespace Pari.Ics2Google.Console
{
    public class ListCommand : Command
    {
        private readonly IcsPathArgument icsPathArgument;

        private readonly IUseCase<IList<string>> listEventUseCase;

        public ListCommand(IUseCase<IList<string>> listEventUseCase, IcsPathArgument icsPathArgument) : base("list", "List the calendar events.")
        {
            this.listEventUseCase = listEventUseCase;
            this.icsPathArgument = icsPathArgument;
        }

        public override IList<Argument> Arguments()
        {
            var arguments = new List<Argument>();
            arguments.Add(this.icsPathArgument);
            return arguments;
        }

        public override bool Validate(IDictionary<string, CommandArgument> arguments)
        {
            return this.icsPathArgument.Validate(arguments);
        }

        public override int Execute(IDictionary<string, CommandArgument> arguments)
        {
            CommandArgument icsPath = arguments[this.icsPathArgument.Name];
            
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
