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

        public static void ClearSecondaryCalendar(this CalendarService service, string calendarId)
        {

        }
    }
}
