using System;
using System.Collections.Generic;
using Pari.Ics2Google.Console.Arguments;
using Pari.Ics2Google.Core;
using Pari.Ics2Google.Core.Import2Google;
using Pari.Ics2Google.Core.ListEvent;
using Pari.Ics2Google.Core.LoadGooleCalendar;
using Unity;
using Unity.Injection;

namespace Pari.Ics2Google.Console
{
    public static class UnityConfiguration
    {
        public static UnityContainer RegisterUseCases(this UnityContainer container)
        {
            container.RegisterType<IUseCase<IList<string>>, ListEventUseCase>("ListEventUseCase");
            container.RegisterType<IUseCase<string>, LoadGoogleCalendarUseCase>("LoadGoogleCalendarUseCase");
            container.RegisterType<IUseCase<object>, Import2GoogleUseCase>("Import2GoogleUseCase");
            return container;
        }

        public static UnityContainer RegisterCommands(this UnityContainer container)
        {
            container.RegisterType<ListCommand>(new InjectionFactory(c =>
                new ListCommand(c.Resolve<IUseCase<IList<string>>>("ListEventUseCase"), c.Resolve<IcsPathArgument>())));
            container.RegisterType<LoadGoogleCalendarCommand>(new InjectionFactory(c =>
                new LoadGoogleCalendarCommand(c.Resolve<IUseCase<string>>("LoadGoogleCalendarUseCase"), c.Resolve<ClientSecretArgument>())));
            container.RegisterType<Import2GoogleCommand>(new InjectionFactory(c =>
                new Import2GoogleCommand(c.Resolve<ClientSecretArgument>(), c.Resolve<IcsPathArgument>(),
                    c.Resolve<GoogleCalendarArgument>(), c.Resolve<ImportFromArgument>(), c.Resolve<IUseCase<object>>("Import2GoogleUseCase"))));

            return container;
        }
    }
}
