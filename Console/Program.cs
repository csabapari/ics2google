using System;
using Microsoft.Extensions.CommandLineUtils;

namespace Pari.Ics2Google.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = new CommandLineApplication();
            app.Name = "ics2google";
            app.HelpOption(Command.HelpOptions);

            var listCommand = new ListCommand();
            app.Command(listCommand.Name, listCommand.Configuration);

            app.Execute(args);
        }
    }
}
