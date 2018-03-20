using System.Linq;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;

namespace Pari.Ics2Google.Core
{
    public static class GoogleCalendarServiceExtension
    {
        public static string GetCalendarIdForName(this CalendarService service, string name)
        {
            CalendarListResource.ListRequest listRequest = service.CalendarList.List();
            CalendarList calendarList = listRequest.Execute();

            CalendarListEntry entry = calendarList.Items?.FirstOrDefault(e => e.Summary == name);

            return entry?.Id;
        }

        /// <summary>
        /// Deletes the secondary calendar with given calendar id. Then creates a new one with the given name
        /// and retruns the id of the new one.
        /// </summary>
        public static string ClearSecondaryCalendar(this CalendarService service, string calendarId, string name)
        {
            if (calendarId != null)
            {
                service.Calendars.Delete(calendarId).Execute();
            }

            Calendar calendar = service.Calendars
                .Insert(new Calendar { Summary = name })
                .Execute();

            return calendar.Id;
        }
    }
}
