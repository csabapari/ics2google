using System.IO;
using Ical.Net;

namespace Pari.Ics2Google.Core
{
    public static class CalendarExtension
    {
        public static Calendar Load(string filePath)
        {
            using (FileStream fs = File.OpenRead(filePath))
            {
                return Calendar.Load(fs);
            }
        }
    }
}
