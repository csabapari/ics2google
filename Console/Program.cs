using Microsoft.Extensions.CommandLineUtils;
using Unity;

namespace Pari.Ics2Google.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (UnityContainer container = new UnityContainer())
            {
                container.RegisterUseCases()
                    .RegisterCommands();

                var app = new CommandLineApplication();
                app.Name = "ics2google";
                app.HelpOption(Command.HelpOptions);

                var listCommand = container.Resolve<ListCommand>();
                app.Command(listCommand.Name, listCommand.Configuration);
                var loadGoogleCommand = container.Resolve<LoadGoogleCalendarCommand>();
                app.Command(loadGoogleCommand.Name, loadGoogleCommand.Configuration);
                var import2GoogleCommand = container.Resolve<Import2GoogleCommand>();
                app.Command(import2GoogleCommand.Name, import2GoogleCommand.Configuration);

                app.Execute(args);
            }

        }
    }
}
