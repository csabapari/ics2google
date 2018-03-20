using System;
using System.Collections.Generic;
using Google;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Calendar = Ical.Net.Calendar;

namespace Pari.Ics2Google.Core.Import2Google
{
    public class Import2GoogleUseCase : UseCase<Import2GoogleInput, object>
    {
        public override IOutput<object> DoExecute(Import2GoogleInput input)
        {
            Calendar calendar = CalendarExtension.Load(input.IcsPath);

            if (calendar.Events == null || calendar.Events.Count == 0)
            {
                Console.WriteLine("There are no events in the input ics file.");
                return new DefaultOutput(true);
            }

            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = input.Credential,
                ApplicationName = "ics2google",
            });

            string workCalendarId = service.GetCalendarIdForName(input.GoogleCalendar);

            Console.WriteLine("Work calendar id: {0}", workCalendarId);

            // clear the calendar
            workCalendarId = service.ClearSecondaryCalendar(workCalendarId, input.GoogleCalendar);

            bool successful = true;
            foreach (CalendarEvent calendarEvent in calendar.Events)
            {
                if (calendarEvent.Start == null || calendarEvent.End == null)
                {
                    Console.WriteLine("Skip event, because start and end dates are missing: {0}", calendarEvent.Summary);
                    continue;
                }

                if (calendarEvent.Start.Value < input.ImportFrom)
                {
                    Console.WriteLine("Skip event, because it is started ({0}) before {1}", calendarEvent.Start.Value, input.ImportFrom);
                    continue;
                }

                Event newEvent = new Event
                {
                    Summary = calendarEvent.Summary,
                    Start = new EventDateTime
                    {
                        DateTime = calendarEvent.Start.Value,
                        TimeZone = calendarEvent.Start.TimeZoneName ?? "Europe/Berlin"
                    },
                    End = new EventDateTime
                    {
                        DateTime = calendarEvent.End.Value,
                        TimeZone = calendarEvent.End.TimeZoneName ?? "Europe/Berlin"
                    }
                };

                if (calendarEvent.RecurrenceRules != null && calendarEvent.RecurrenceRules.Count > 0)
                {
                    if (calendarEvent.Summary == "home office")
                    {
                        Console.WriteLine("TEST");
                        newEvent.Recurrence = new List<string> { "RRULE:FREQ=WEEKLY;UNTIL=20181231;INTERVAL=1;BYDAY=MO,TU,FR" };
                    }
                    else
                    {
                        // at the moment my assumption that always one rule present here (at least in my exported calendar)
                        RecurrencePattern ruleData = calendarEvent.RecurrenceRules[0];
                        string rule = string.Format("{0}:{1}", ruleData.AssociatedObject.Name, ruleData);
                        newEvent.Recurrence = new List<string> { rule };
                    }
                }

                EventsResource.InsertRequest insertRequest = service.Events.Insert(newEvent, workCalendarId);
                try
                {
                    newEvent = insertRequest.Execute();
                    Console.WriteLine("Event inserted: {0}, {1}", newEvent.Summary, newEvent.Start.DateTime);
                }
                catch (GoogleApiException gae)
                {
                    Console.WriteLine("ERROR cannot insert event {0}, {1}", newEvent.Summary, newEvent.Start.DateTime);
                    Console.WriteLine("Exception: {0} {1}", gae.Message, gae.Error);
                    successful = false;
                }
            }

            return new DefaultOutput(successful);
        }
    }
}
