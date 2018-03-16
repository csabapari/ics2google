using System;
using System.Collections.Generic;
using System.Text;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;

namespace Pari.Ics2Google.Core.LoadGooleCalendar
{
    public class LoadGoogleCalendarUseCase : UseCase<LoadGoogleCalendarInput, string>
    {
        public override IOutput<string> DoExecute(LoadGoogleCalendarInput input)
        {
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = input.Credential,
                ApplicationName = "ics2google",
            });

            string workCalendarId = service.GetCalendarIdForName("csaba.pari.work");

            Console.WriteLine("Work calendar id: {0}", workCalendarId);

            // clear the calendar
            service.Calendars.Delete(workCalendarId).Execute();

            Calendar workCalendar = service.Calendars.Insert(new Calendar
            {
                Summary = "csapa.pari.work"
            }).Execute();
            workCalendarId = workCalendar.Id;

            Event newEvent = new Event
            {
                Summary = "xavitest1",
                Start = new EventDateTime
                {
                    DateTime = new DateTime(2018, 3, 17, 12, 0, 0),
                    TimeZone = "Europe/Budapest"
                },
                End = new EventDateTime
                {
                    DateTime = new DateTime(2018, 3, 17, 13, 0, 0),
                    TimeZone = "Europe/Budapest"
                },
                Attendees = new List<EventAttendee>
                {
                    new EventAttendee
                    {
                        Email = "csaba.pari@gmaill.com"
                    }
                }
            };
            EventsResource.InsertRequest insertRequest = service.Events.Insert(newEvent, workCalendarId);
            newEvent = insertRequest.Execute();
            Console.WriteLine("New event: {0}", newEvent.HtmlLink);

            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            Console.WriteLine("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;
                    }
                    Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                }
            }
            else
            {
                Console.WriteLine("No upcoming events found.");
            }

            return new LoadGoogleCalendarOutput(true, string.Empty);
        }
    }
}
