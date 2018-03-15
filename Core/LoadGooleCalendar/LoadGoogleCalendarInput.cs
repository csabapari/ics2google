using System;
using System.Collections.Generic;
using System.Text;
using Google.Apis.Auth.OAuth2;

namespace Pari.Ics2Google.Core.LoadGooleCalendar
{
    public class LoadGoogleCalendarInput : IInput
    {
        public LoadGoogleCalendarInput(UserCredential credential)
        {
            this.Credential = credential;
        }

        public UserCredential Credential { get; }
    }
}
