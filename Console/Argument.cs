
using System.Collections.Generic;
using Microsoft.Extensions.CommandLineUtils;

namespace Pari.Ics2Google.Console
{
    public abstract class Argument
    {
        public Argument(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        public string Name { get; }

        public string Description { get; }

        public abstract bool Validate(IDictionary<string, CommandArgument> arguments);
    }
}
