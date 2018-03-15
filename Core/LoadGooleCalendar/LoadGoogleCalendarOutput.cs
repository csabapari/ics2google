using System;
using System.Collections.Generic;
using System.Text;

namespace Pari.Ics2Google.Core.LoadGooleCalendar
{
    public class LoadGoogleCalendarOutput : Output<string>
    {
        public LoadGoogleCalendarOutput(bool executionResult, string data) : base(executionResult, data)
        {
        }
    }
}
