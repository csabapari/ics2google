using System.Collections.Generic;
using Microsoft.Extensions.CommandLineUtils;

namespace Pari.Ics2Google.Console
{
    public abstract class Command
    {
        public const string HelpOptions = "-?|-h|--help";

        private IDictionary<string, CommandArgument> CommandArguments { get; }

        protected Command(string name, string desription)
        {
            this.Name = name;
            this.Description = desription;
            this.CommandArguments = new Dictionary<string, CommandArgument>();
        }

        public string Name { get; }

        public string Description { get; }

        public abstract int Execute(IDictionary<string, CommandArgument> arguments);

        public abstract bool Validate(IDictionary<string, CommandArgument> arguments);

        public virtual IList<Argument> Arguments()
        {
            return new List<Argument>();
        }

        public void Configuration(CommandLineApplication command)
        {
            command.Description = this.Description;
            command.HelpOption(HelpOptions);
            foreach (Argument argument in this.Arguments())
            {
                this.CommandArguments.Add(argument.Name, command.Argument(argument.Name, argument.Description));
            }
            command.OnExecute(() =>
            {
                if (!this.Validate(this.CommandArguments))
                {
                    return 1;
                }
                return this.Execute(this.CommandArguments);
            });
        }
    }
}
