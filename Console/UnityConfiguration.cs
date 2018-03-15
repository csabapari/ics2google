using System;
using System.Collections.Generic;
using Pari.Ics2Google.Core;
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
            container.RegisterType<IUseCase<string>, LoadGoogleCalendarUseCase>("LoadGoogleCalendarCommand");
            return container;
        }

        public static UnityContainer RegisterCommands(this UnityContainer container)
        {
            container.RegisterType<ListCommand>(new InjectionFactory(c => new ListCommand(c.Resolve<IUseCase<IList<string>>>("ListEventUseCase"))));
            container.RegisterType<LoadGoogleCalendarCommand>(new InjectionFactory(c =>
                new LoadGoogleCalendarCommand(c.Resolve<IUseCase<string>>("LoadGoogleCalendarCommand"))));

            return container;
        }
    }
}
