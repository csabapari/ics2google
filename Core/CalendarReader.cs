using System.IO;
using Ical.Net;
using Ical.Net.CalendarComponents;

namespace Pari.Ics2Google.Core
{
    public class CalendarReader
    {
        private Calendar calendar;

        public void Read(string filePath)
        {
            using (FileStream fs = File.OpenRead(filePath))
            {
                this.calendar = Calendar.Load(fs);
            }
        }

        public void ListAll()
        {
            foreach (CalendarEvent calendarEvent in this.calendar.Events)
            {
                System.Console.WriteLine(string.Format("Event {0} starts at {1}", calendarEvent.Name, calendarEvent.Start.Date));
            }
        }
    }
}
