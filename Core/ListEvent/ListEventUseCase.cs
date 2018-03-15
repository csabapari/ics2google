using System.Collections.Generic;
using Ical.Net;
using Ical.Net.CalendarComponents;

namespace Pari.Ics2Google.Core.ListEvent
{
    public class ListEventUseCase : UseCase<ListEventInput, IList<string>>
    {
        public override IOutput<IList<string>> DoExecute(ListEventInput input)
        {
            IList<string> data = new List<string>();

            Calendar calendar = CalendarExtension.Load(input.IscPath);

            foreach (CalendarEvent calendarEvent in calendar.Events)
            {
                data.Add(string.Format("Event Summary: {0}, Start date: {1}", calendarEvent.Summary, calendarEvent.DtStart));
            }

            return new ListEventOutput(true, data);
        }
    }
}
